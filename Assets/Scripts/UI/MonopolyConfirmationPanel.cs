using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Confirmation panel that display a text and proposer to confirm or cancel.
/// Invoke specific action on confirm and cancel options.
/// </summary>
public class MonopolyConfirmationPanel : MonoBehaviour
{
    #region F/P
    /// <summary>
    /// Confirmation panel to display or hide
    /// </summary>
    [SerializeField]
    GameObject panel = null;

    /// <summary>
    /// Text to edit
    /// </summary>
    [SerializeField]
    TMP_Text txtQuestion = null;

    /// <summary>
    /// Button for confirm and cancel actions
    /// </summary>
    [SerializeField]
    Button btnConfirm = null, btnCancel = null;

    /// <summary>
    /// Check if panel, text and buttons exists
    /// </summary>
    public bool IsConfirmationPanelValid => panel && txtQuestion && btnConfirm && btnCancel;
    #endregion

    #region CustomMethods
    /// <summary>
    /// Display confirmation panel with specific text.
    /// Invoke specific actions passed in params.
    /// </summary>
    /// <param name="_text">Text to display</param>
    /// <param name="_callbackConfirm">Action on confirm callback</param>
    /// <param name="_callbackCancel">Action on cancel callback</param>
    public void PrintConfirmPanel(string _text, Action _callbackConfirm,
        Action _callbackCancel = null)
    {
        if (!IsConfirmationPanelValid)
            return;

        panel.SetActive(true);
        txtQuestion.text = _text;
        btnConfirm.onClick.AddListener(() =>
        {
            _callbackConfirm?.Invoke();
            CloseConfirmPanel();
        });

        btnCancel.onClick.AddListener(() =>
        {
            _callbackCancel?.Invoke();
            CloseConfirmPanel();
        });
    }

    /// <summary>
    /// Hide confirmation panel and remove all listeners on buttons
    /// </summary>
    public void CloseConfirmPanel()
    {
        if (!IsConfirmationPanelValid)
            return;

        btnConfirm.onClick.RemoveAllListeners();
        btnCancel.onClick.RemoveAllListeners();
        panel.SetActive(false);
    }
    #endregion
}
