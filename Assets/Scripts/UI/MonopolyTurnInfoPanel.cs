using UnityEngine;
using TMPro;

/// <summary>
/// UI panel that displays text with a specific color
/// based on value passed in parameters
/// </summary>
public class MonopolyTurnInfoPanel : MonoBehaviour
{
    #region F/P
    const string PLAYER_WIN_TEXT = " a gagné la partie ! " +
        "<color=white>Clic gauche pour relancer la partie</color>";
    const string PLAYER_LOSE_TEXT = " a perdu !";

    /// <summary>
    /// UI text to edit
    /// </summary>
    [SerializeField]
    TMP_Text txtTurn = null;

    /// <summary>
    /// Check if txtTurn exists
    /// </summary>
    public bool IsMonopolyTurnPanelValid => txtTurn != null;
    #endregion

    #region CustomMethods
    /// <summary>
    /// Bind <see cref="TurnPanel"/> to <see cref="MonopolyCharacterManager" events/>
    /// </summary>
    /// <param name="_playerManager">manager to bind events from</param>
    public void Init(MonopolyCharacterManager _playerManager)
    {
        if (!IsMonopolyTurnPanelValid || !_playerManager)
            return;

        _playerManager.OnCharacterTurnStarted += (_player) 
            => SetTxtTurn(_player.CurrentSituationText, _player.Color);

        _playerManager.OnCharacterLose += (_player)
            => SetTxtTurn(_player + PLAYER_LOSE_TEXT, _player.Color);

        _playerManager.OnCharacterWin += (_player)
            => SetTxtTurn(_player + PLAYER_WIN_TEXT, _player.Color);
    }

    /// <summary>
    /// Set text with a specific color
    /// </summary>
    /// <param name="_text">Text to display</param>
    /// <param name="_color">Color of the text</param>
    public void SetTxtTurn(string _text, Color _color)
    {
        txtTurn.text = _text;
        txtTurn.color = _color;
    }
    #endregion
}
