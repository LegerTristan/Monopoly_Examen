using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Panel that displays player informations
/// </summary>
public class MonopolyPlayerInfoPanel : MonoBehaviour
{

    #region F/P
    /// <summary>
    /// Text for displaying player name and player money.
    /// </summary>
    [SerializeField]
    TMP_Text txtPlayerName = null, txtMoney = null;

    /// <summary>
    /// Background of the panel
    /// </summary>
    [SerializeField]
    Image background = null;

    /// <summary>
    /// Check if texts and background exists
    /// </summary>
    public bool IsPlayerInfoPanelValid => txtPlayerName != null && txtMoney != null
        && background != null;
    #endregion

    #region CustomMethods
    /// <summary>
    /// Set background and texts based on player informations and bind
    /// <see cref="txtMoney"/> to player OnMoneyUpdate event.
    /// </summary>
    /// <param name="_player">Player to get informations from</param>
    public void Init(MonopolyCharacter _player)
    {
        if (!IsPlayerInfoPanelValid || !_player)
            return;

        SetBackgroundColor(_player.Color);
        SetTextPlayerName(_player.ToString());
        SetTextMoney(_player.Money.Current);
        _player.Money.OnUpdate += SetTextMoney;
    }

    void SetTextPlayerName(string _playerName)
    {
        txtPlayerName.text = _playerName;
    }

    void SetTextMoney(int _money)
    {
        txtMoney.text = _money.ToString() + MonopolyGameManager.Instance?.Currency;
    }

    void SetBackgroundColor(Color _color)
    { 
        background.color = _color;
    }
    #endregion
}
