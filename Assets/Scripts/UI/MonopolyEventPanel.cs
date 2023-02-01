using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Event panel that display card with a specific event text
/// </summary>
public class MonopolyEventPanel : MonoBehaviour
{
    #region F/P
    /// <summary>
    /// Event panel that contains event card sprite
    /// </summary>
    [SerializeField]
    Image eventPanel = null;

    /// <summary>
    /// Text for luck event
    /// </summary>
    [SerializeField]
    TMP_Text txtEventLuck = null;

    /// <summary>
    /// Text for community event
    /// </summary>
    [SerializeField]
    TMP_Text txtEventCommunity = null;

    /// <summary>
    ///Check if eventPanel and texts exists.
    /// </summary>
    public bool IsEventPanelValid => eventPanel != null && txtEventLuck != null && txtEventCommunity;
    #endregion

    #region CustomMethods
    /// <summary>
    /// Display event card with specific sprite and text passed
    /// in parameters
    /// The text displayed depends on if the card is a community event or not
    /// </summary>
    /// <param name="_cardSprite">Sprite to display</param>
    /// <param name="_text">Text to display</param>
    /// <param name="_isEventCommunity">Display luck text or community text depending on this value</param>
    public void PrintEventCard(Sprite _cardSprite, string _text, bool _isEventCommunity)
    {
        if (!IsEventPanelValid)
            return;

        eventPanel.sprite = _cardSprite;

        if (_isEventCommunity)
        {
            txtEventCommunity.text = _text;
            txtEventCommunity.gameObject.SetActive(true);
        }
        else
        {
            txtEventLuck.text = _text;
            txtEventLuck.gameObject.SetActive(true);
        }

        eventPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide event card
    /// </summary>
    public void HideEventCard()
    {
        if (!IsEventPanelValid)
            return;

        eventPanel.gameObject.SetActive(false);
        txtEventCommunity.gameObject.SetActive(false);
        txtEventLuck.gameObject.SetActive(false);
    }
    #endregion
}
