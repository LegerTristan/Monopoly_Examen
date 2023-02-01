using System;
using System.Collections;
using UnityEngine;

public class MonopolyGameManager : SingletonTemplate<MonopolyGameManager>
{
    #region F/P
    #region Fields
    /// <summary>
    /// invoked when the game is restarted
    /// </summary>
    public event Action OnGameRestarted = null;

    /// <summary>
    /// Call when the wait time between each turn is ended
    /// </summary>
    public event Action OnWaitEnded = null;

    MonopolyCharacterManager characterManager = null;

    [SerializeField]
    MonopolyUIManager uiManager = null;

    [SerializeField]
    MonopolyHouseManager houseManager = null;

    [SerializeField]
    MonopolyCamera mainCam = null;

    [SerializeField]
    Board board = null;

    /// <summary>
    /// Waiting between each turn
    /// </summary>
    WaitForSeconds waiting = null;

    /// <summary>
    /// Waiting at the start and the end of the game
    /// </summary>
    WaitUntil waitingStart = null;

    /// <summary>
    /// Currency used in the game
    /// </summary>
    [SerializeField]
    string currency = "$";

    /// <summary>
    /// Time to wait between each turn
    /// </summary>
    [SerializeField, Range(0.1f, 10f)]
    float waitTimeBetweenTurn = 1f;

    /// <summary>
    /// Money gained every time a character make a complete turn of the board
    /// </summary>
    [SerializeField, Range(1, 2500)]
    int moneyGainedByTurnComplete = 200;

    /// <summary>
    /// Number of roll every character make to determine the number of cell to move
    /// </summary>
    [SerializeField, Range(1, 4)]
    int nbrRollDice = 2;

    bool inGame = false, waitForReset = false;
    #endregion

    #region Properties
    public Board Board => board;

    public MonopolyCharacterManager CharacterManager => characterManager;

    public MonopolyHouseManager HouseManager => houseManager;

    public MonopolyCamera MainCam => mainCam;

    public string Currency => currency;

    public int MoneyGainedByTurnComplete => moneyGainedByTurnComplete;

    public int NbrRollDice => nbrRollDice;

    bool IsManagerValid => characterManager && mainCam && uiManager && board 
        && houseManager;
    #endregion
    #endregion

    #region CustomMethods

    /// <summary>
    /// Bind it to characterManager OnPlayersReceived event and init wait members
    /// </summary>
    /// <param name="_playerManager">CharacterManager used in the game</param>
    public void Init(MonopolyCharacterManager _playerManager)
    {
        characterManager = _playerManager;

        if (!IsManagerValid)
            return;

        waiting = new WaitForSeconds(waitTimeBetweenTurn);
        waitingStart = new WaitUntil(() => Input.GetMouseButtonDown(0));

        characterManager.OnCharactersReceived += InitLeftElements;
    }

    /// <summary>
    /// Initialize left managers that needs that characterManager already have players.
    /// Do this only during the first game, not after a restart
    /// </summary>
    void InitLeftElements()
    {
        uiManager.Init(characterManager);
        BindToCharacterManager();
        characterManager.OnCharactersReceived -= InitLeftElements;
    }

    /// <summary>
    /// Bind to characterManager events and start the game if there is players.
    /// </summary>
    void BindToCharacterManager()
    {
        for (int i = 0; i < characterManager.Count; ++i)
        {
            MonopolyCharacter _player = characterManager[i];
            _player.OnBoardMoveEnded += (_index) => board.TriggerCellEvent(_player, _index);
            _player.OnBoardTurnCompleted += () => _player.Money.Current += MoneyGainedByTurnComplete;
        }

        characterManager.OnCharacterTurnStarted += (_player) => mainCam.SetTarget(_player.transform);

        characterManager.OnCharacterTurnEnded += (_player) => StartCoroutine(WaitLoop());

        characterManager.OnCharacterWin += (_player) =>
        {
            inGame = false;
            waitForReset = true;
            StartCoroutine(WaitStart());
        };

        OnWaitEnded += characterManager.NextTurn;

        if (characterManager.Count > 0)
            StartCoroutine(WaitStart());
    }

    void StartGame()
    {
        inGame = true;
        mainCam.SetTarget(characterManager[0].transform);
        characterManager.NextTurn();
    }

    void RestartGame()
    {
        waitForReset = false;
        OnGameRestarted?.Invoke();
        StartGame();
    }

    #region Wait
    /// <summary>
    /// Coroutine that waits a left mouse click in order to start or restart the game.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitStart()
    {
        yield return waitingStart;

        if (waitForReset)
            RestartGame();
        else
            StartGame();
    }

    /// <summary>
    /// Coroutine that waits a period before invoke OnWaitEnded
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitLoop()
    {
        yield return waiting;

        if (inGame && IsManagerValid)
            OnWaitEnded?.Invoke();
    }
    #endregion
    #endregion
}
