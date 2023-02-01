using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage characters and invoke events when a character turn has started, 
/// ended or when he loses or win.
/// </summary>
public class MonopolyCharacterManager : MonoBehaviour
{
    #region F/P
    public event Action OnCharactersReceived = null;

    public event Action<MonopolyCharacter>  OnCharacterTurnStarted = null,
                                            OnCharacterTurnEnded = null,
                                            OnCharacterLose = null,
                                            OnCharacterWin = null;

    List<MonopolyCharacter> characters = new List<MonopolyCharacter>();

    int characterIndex = -1;

    MonopolyCharacter CurrentCharacter => characterIndex < 0 || characterIndex >= characters.Count ?
        characters[0] : characters[characterIndex];

    public MonopolyCharacter this[int _index] => _index < 0 || _index >= characters.Count ?
        null : characters[_index];

    public int Count => characters.Count;

    #endregion

    #region CustomMethods
    /// <summary>
    /// Copy characters from a list passed in parameters.
    /// If it is the first initialization, call InitBindings
    /// </summary>
    /// <param name="_characters">List of characters</param>
    /// <param name="_firstInit">Dfines whether it is the first initialization (start game) or not (restart game)</param>
    public void InitCharacters(List<MonopolyCharacter> _characters, bool _firstInit = true)
    {
        CopyCharacters(_characters);

        if (characters.Count == 0)
            return;

        for(int i = 0; i < characters.Count; ++i)
        {
            if (characters[i])
                characters[i].ResetParams();
        }

        if(_firstInit)
            InitBindings();
    }

    public void CopyCharacters(List<MonopolyCharacter> _characters)
    {
        characters.Clear();

        for (int i = 0; i < _characters.Count; ++i)
        {
            characters.Add(_characters[i]);
        }
    }

    void InitBindings()
    {
        OnCharacterTurnStarted += (_character) =>
        {
            _character.PlayTurn();
        };

        MonopolyGameManager.Instance.Board.OnCellTriggered += (_cell) => EndCharacterTurn();

        OnCharactersReceived?.Invoke();
    }

    void StartCharacterTurn()
    {
        OnCharacterTurnStarted?.Invoke(CurrentCharacter);
    }

    public void NextTurn()
    {
        characterIndex++;
        characterIndex = characterIndex >= characters.Count ? 0 : characterIndex;
        StartCharacterTurn();
    }

    void EndCharacterTurn()
    {
        if (CurrentCharacter.Money.Current == 0)
            RemoveCurrentCharacter();
        else
            OnCharacterTurnEnded?.Invoke(CurrentCharacter);
    }

    /// <summary>
    /// Remove current character and call OnCharacterTurnEnded or OnCharacterWin
    /// depending on number of characters left.
    /// </summary>
    void RemoveCurrentCharacter()
    {
        MonopolyCharacter _loser = CurrentCharacter;

        characters.Remove(CurrentCharacter);
        _loser.Lose();
        characterIndex--;
        OnCharacterLose?.Invoke(_loser);

        if (characters.Count == 1)
            OnCharacterWin?.Invoke(characters[0]);
        else
            OnCharacterTurnEnded?.Invoke(_loser);
    }
    #endregion
}
