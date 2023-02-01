using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage all UI elements, accessible trough singleton instance.
/// </summary>
public class MonopolyUIManager : SingletonTemplate<MonopolyUIManager>
{
    #region F/P
    [SerializeField]
    MonopolyTurnInfoPanel turnPanel = null;

    /// <summary>
    /// Prefab for creating panels with players informations
    /// </summary>
    [SerializeField]
    MonopolyPlayerInfoPanel playerInfoPrefab = null;

    [SerializeField]
    MonopolyDicePanel dicePanel = null;

    [SerializeField]
    MonopolyEventPanel eventPanel = null;

    [SerializeField]
    MonopolyConfirmationPanel confirmationPanel = null;
    
    /// <summary>
    /// Container for displaying player informations
    /// </summary>
    [SerializeField]
    HorizontalLayoutGroup playerInfoContainer = null;

    public MonopolyTurnInfoPanel TurnInfoPanel => turnPanel;
    public MonopolyEventPanel EventPanel => eventPanel;
    public MonopolyConfirmationPanel ConfirmPanel => confirmationPanel;

    /// <summary>
    /// Check that every UI elements exists and is valid.
    /// </summary>
    bool IsUIValid => turnPanel && turnPanel.IsMonopolyTurnPanelValid && playerInfoContainer &&
        playerInfoPrefab && playerInfoPrefab.IsPlayerInfoPanelValid && confirmationPanel &&
        confirmationPanel.IsConfirmationPanelValid && dicePanel && dicePanel.IsDicePanelValid
        && eventPanel && eventPanel.IsEventPanelValid;

    #endregion

    #region CustomMethods
    /// <summary>
    /// Init all UI elements
    /// </summary>
    /// <param name="_characterManager">Character manager to give to UI elements</param>
    public void Init(MonopolyCharacterManager _characterManager)
    {
        if (!IsUIValid)
            return;

        turnPanel.Init(_characterManager);
        dicePanel.Init(_characterManager, MonopolyGameManager.Instance.NbrRollDice);
        confirmationPanel.CloseConfirmPanel();
        AddPlayerInfoPanels(_characterManager);
    }

    /// <summary>
    /// Create a new PlayerInfoPanel and set it as child of <see cref="playerInfoContainer"/>
    /// </summary>
    /// <param name="_playerManager">Character manager used to get players informations</param>
    void AddPlayerInfoPanels(MonopolyCharacterManager _playerManager)
    {
        for(int i = 0; i < _playerManager.Count; ++i)
        {
            MonopolyCharacter _player = _playerManager[i];

            if (!_player)
                return;

            MonopolyPlayerInfoPanel _panel = Instantiate(playerInfoPrefab);
            _panel.Init(_player);
            _panel.transform.SetParent(playerInfoContainer.transform);
        }
    }
    #endregion
}
