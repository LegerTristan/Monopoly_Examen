using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityToolbox.GizmosTools;
using Random = UnityEngine.Random;

#region Money
/// <summary>
/// Contains a current money that can be oincreased or decreased
/// Contains event to call when money is updated or when there is no money left
/// </summary>
public class Money
{
    public event Action OnEmpty = null;
    public event Action<int> OnUpdate = null;

    int current = 0;

    public int Current
    {
        get => current;
        set
        {
            current = value;
            current = current < 0 ? 0 : current;
            OnUpdate?.Invoke(current);

            if (current == 0)
                OnEmpty?.Invoke();
        }
    }
}
#endregion

#region Character
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(FollowComponent))]
public abstract class MonopolyCharacter : MonoBehaviour
{
    #region F/P
    #region Fields

    #region MoveBoard
    /// <summary>
    /// Invoke when character has ended his movement on the board.
    /// </summary>
    public event Action<int> OnBoardMoveEnded = null;

    /// <summary>
    /// Invoke when character complete a turn on the board
    /// </summary>
    public event Action OnBoardTurnCompleted = null;

    /// <summary>
    /// Component used for moving on board.
    /// </summary>
    [SerializeField]
    FollowComponent followComp = null;

    Transform currentCell = null;

    int boardIndex = 0, nbrMove = 0;

    public int DiceResult { get; set; } = 0;

    /// <summary>
    /// Defines if character can have money when he completes a turn on the board
    /// </summary>
    bool canHaveStartMoney = true;
    #endregion

    #region Property
    /// <summary>
    /// Invoke when character has made a choice about a property bought options.
    /// </summary>
    public event Action<bool> OnPropertyBoughtChoiceMade = null;

    /// <summary>
    /// Chance that the character buy a new house on one of his properties (1/3 chances)
    /// </summary>
    const int BUY_HOUSE_CHANCE = 3;

    protected List<Property> properties = new List<Property>();
    #endregion

    [SerializeField]
    MonopolyCharacterData data = null;

    /// <summary>
    /// Sphere above player that represents his color to distinguish him more easily
    /// </summary>
    [SerializeField]
    Transform characterIcon = null;

    /// <summary>
    /// Text that defines current situation of the character (Is in prison, moving...)
    /// </summary>
    string situationText = string.Empty;

    public int TurnLeftInPrison { get; set; } = 0;

    [SerializeField, Header("Debug")]
    bool useDebug = true;

    #endregion

    #region Properties

    public Money Money { get; private set; } = new Money();

    public Color Color => data != null ? data.CharacterColor  : Color.white;

    /// <summary>
    /// Text to print when character is moving
    /// </summary>
    string CharacterTurnText => $"Au tour de {this}";

    /// <summary>
    /// Text to print when character is in prison
    /// </summary>
    string InPrisonText => $"{this} n'a pas réussi à sortir de prison via un double ! " +
        $"Il/Elle doit rester encore {TurnLeftInPrison} tour.s en prison !";

    /// <summary>
    /// Text to print when character leaves prison with a prison card
    /// </summary>
    string LeavePrisonWithCardText => $"{this} sort de prison grâce à sa carte !";

    /// <summary>
    /// Text to print when character leaves prison with a double
    /// </summary>
    string LeavePrisonWithDouble => $"{this} sort de prison grâce à un double !";

    public string CurrentSituationText => situationText;

    public int CurrentBoardIndex => boardIndex;

    public bool CanLeavePrison { get; set; } = false;

    bool IsInPrison => TurnLeftInPrison != 0;

    bool IsMonopolyCharacterValid => followComp != null && followComp.IsLerpMoveComponentValid
                                    && data != null && characterIcon != null;
    #endregion
    #endregion

    #region UnityEvents
    private void Start() => Init();

    private void LateUpdate()
    {
        followComp.LookAtTarget(currentCell);
        followComp.MoveToTarget(currentCell);
    }

    private void OnDrawGizmos()
    {
        if (!useDebug)
            return;

        if (!IsMonopolyCharacterValid)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, Vector3.one);
            return;
        }

        Gizmos.color = data.CharacterColor;
        Gizmos.DrawSphere(transform.position + transform.up * 0.01f, 0.1f);
    }
    #endregion

    #region CustomMethods
    /// <summary>
    /// Initialize player icon and currentCell
    /// </summary>
    public void Init()
    {
        if (!IsMonopolyCharacterValid)
            return;

        boardIndex = 0;
        characterIcon.GetComponent<MeshRenderer>().material.color = data.CharacterColor;
        characterIcon.localScale = Vector3.one * data.SphereRadius;
        currentCell = MonopolyGameManager.Instance.Board[boardIndex].transform;
        transform.position = currentCell.position;
    }

    public override string ToString() => data ? $"{data.CharacterName}" : "Unknown";

    public void PlayTurn()
    {
        canHaveStartMoney = true;

        if (!IsInPrison)
        {
            situationText = CharacterTurnText;
            MoveOnBoard();
        }
        else
            PassTurnInPrison();
    }

    /// <summary>
    /// Reset all parameters of the character such as prison card, properties, board index...
    /// </summary>
    public void ResetParams()
    {
        canHaveStartMoney = true;
        CanLeavePrison = false;
        ResetAllProperties();
        TeleportTo(0);
    }

    #region Prison
    void ReduceTurnInPrison() => TurnLeftInPrison--;

    /// <summary>
    /// Set situationText according to situation.
    /// If player has a card, he leaves the prison, else he try a double, if it fails,
    /// then <see cref="TurnLeftInPrison"/> is decremented and character do nothing.
    /// </summary>
    void PassTurnInPrison()
    {
        if(CanLeavePrison)
        {
            situationText = LeavePrisonWithCardText;
            LeavePrison();
            CanLeavePrison = false;
            return;
        }

        if(TryLeavePrison())
        {
            situationText = LeavePrisonWithDouble;
            LeavePrison();
            return;
        }
        
        situationText = InPrisonText;
        ReduceTurnInPrison();
        EndMove();
    }

    /// <summary>
    /// Roll two dices and check if they are identical
    /// </summary>
    /// <returns>Returns true if it is a double, else false</returns>
    bool TryLeavePrison()
    {
        int _diceA = Dice.Instance.RollDice(), _diceB = Dice.Instance.RollDice();

        return _diceA == _diceB;
    }

    void LeavePrison()
    {
        MoveOnBoard();
        TurnLeftInPrison = 0;
    }
    #endregion

    #region BoardMove

    /// <summary>
    /// Move character on board.
    /// Number of move is determine by dice roll, number of roll depending on user define
    /// </summary>
    void MoveOnBoard()
    {
        int _nbrMove = 0;
        for (int i = 0; i < MonopolyGameManager.Instance?.NbrRollDice; ++i)
            _nbrMove += Dice.Instance.RollDice();

        MoveOnBoard(_nbrMove);
    }

    /// <summary>
    /// Move on board from number of cells passed in parameters.
    /// Set also if character can have start money.
    /// </summary>
    /// <param name="_nbrMove">Move range</param>
    /// <param name="_canHaveStartMoney">Defines if character can gain money when he compketes a turn on board</param>
    public void MoveOnBoard(int _nbrMove, bool _canHaveStartMoney = true)
    {
        canHaveStartMoney = _canHaveStartMoney;
        DiceResult = _nbrMove;
        nbrMove = _nbrMove;
        followComp.OnMoveEnded += MoveToNextCell;
        MoveToNextCell();
    }

    void MoveToNextCell()
    {
        if(nbrMove == 0)
        {
            EndMove();
            return;
        }

        SetNextCell();
    }

    void SetNextCell()
    {
        boardIndex++;
        if(boardIndex >= Board.BOARD_SIZE)
        {
            boardIndex = 0;

            if(canHaveStartMoney)
                OnBoardTurnCompleted?.Invoke();
        }

        nbrMove--;
        nbrMove = nbrMove < 0 ? 0 : nbrMove;
        currentCell = MonopolyGameManager.Instance.Board[boardIndex].transform;
    }

    void EndMove()
    {
        followComp.OnMoveEnded -= MoveToNextCell;
        OnBoardMoveEnded?.Invoke(boardIndex);
    }

    /// <summary>
    /// Set character position based on board index in parameters.
    /// </summary>
    /// <param name="_boardIndex">New position of the character</param>
    public void TeleportTo(int _boardIndex)
    {
        if (_boardIndex < 0 || _boardIndex >= Board.BOARD_SIZE)
            return;

        boardIndex = _boardIndex;
        currentCell = MonopolyGameManager.Instance?.Board[_boardIndex].transform;
        transform.position = currentCell.position;
    }

    #endregion

    #region Property
    public abstract void ChooseIfBuy(PropertyData _buyData);

    protected void ReturnChoiceIfBuy(bool _choice)
    {
        OnPropertyBoughtChoiceMade?.Invoke(_choice);
    }

    public bool CanAffordCost(int _cost) => _cost < Money.Current;

    public void AddProperty(Property _property)
    {
        properties.Add(_property);
    }

    /// <summary>
    /// Returns all occurences in properties based on a predicate passed in parameters
    /// </summary>
    /// <param name="_occurenceCondition">Conditions to validate in order to increment occurences</param>
    /// <returns></returns>
    public int PropertyOccurences(Func<Property, bool> _occurenceCondition)
    {
        if (_occurenceCondition == null)
            return 0;

        int _occurences = 0;

        for(int i = 0; i < properties.Count; ++i)
        {
            if (_occurenceCondition.Invoke(properties[i]))
                _occurences++;
        }

        return _occurences;
    }

    /// <summary>
    /// returns number of houses and hotels on all properties.
    /// </summary>
    /// <param name="_nbrHouses">Number of houses</param>
    /// <param name="_nbrHotels">Number of hotels</param>
    public void GetNbrHousesAndHotels(ref int _nbrHouses, ref int _nbrHotels)
    {
        for(int i = 0; i < properties.Count; ++i)
        {
            Plot _plot = properties[i] as Plot;
            if (_plot == null)
                continue;

            _nbrHouses += _plot.NbrHouse;
            _nbrHotels += _plot.NbrHotel;
        }
    }

    public bool BuyHouse(int _totalHouseCost) => CanAffordCost(_totalHouseCost)
         && Random.Range(0, BUY_HOUSE_CHANCE) == 0;

    void ResetAllProperties()
    {
        for(int i = 0; i < properties.Count; ++i)
        {
            if (properties[i] != null)
                properties[i].Reset();
        }

        properties.Clear();
    }
    #endregion

    public void Lose()
    {
        TeleportToCenterBoard();
        ResetAllProperties();
    }

    void TeleportToCenterBoard()
    {
        currentCell = null;
        transform.position = MonopolyGameManager.Instance.Board.transform.position + GetRandomPosition();
    }

    /// <summary>
    /// Returns a random position based on tansform forward and right vector.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomPosition()
    {
        return (transform.forward * Random.Range(-0.1f, 0.1f)) + (transform.right * Random.Range(0f, 0.1f));
    }
    #endregion
}
#endregion