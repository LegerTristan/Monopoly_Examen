using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dice panel for displaying dice values.
/// </summary>
public class MonopolyDicePanel : MonoBehaviour
{
    #region F/P
    /// <summary>
    /// Panel to display dice values
    /// </summary>
    [SerializeField]
    GameObject dicePanel = null;

    /// <summary>
    /// Prefab for displaying dice value
    /// </summary>
    [SerializeField]
    Image diceImgPrefab = null;

    /// <summary>
    /// Each sprite of dice result
    /// </summary>
    [SerializeField]
    Sprite[] diceSprites = null;

    /// <summary>
    /// Dice image to display or hide.
    /// Length depends on number of roll we have defined in <see cref="MonopolyGameManager"/>
    /// </summary>
    Image[] diceImgs = null;

    /// <summary>
    /// Current number of display dice images
    /// </summary>
    int currentNbrDisplay = 0;

    /// <summary>
    /// Check if panel, image prefab, and sprites exists
    /// </summary>
    public bool IsDicePanelValid => dicePanel && diceImgPrefab && diceSprites != null 
        && diceSprites.Length > 0;

    #endregion

    #region CustomMethods
    /// <summary>
    /// Bind <see cref="DicePanel"/> to <see cref="MonopolyCharacterManager"/> events
    /// Also, create number of dice image based on number of roll defined in <see cref="MonopolyGameManager"/>
    /// </summary>
    /// <param name="_characterManager">Manager to bind events on</param>
    /// <param name="_nbrRoll">Number of rolls a player have when he wants to move</param>
    public void Init(MonopolyCharacterManager _characterManager, int _nbrRoll)
    {
        if (!IsDicePanelValid || !_characterManager)
            return;

        SpawnDices(_nbrRoll);
        Dice.Instance.OnDiceRolled += PrintDiceValue;
        _characterManager.OnCharacterTurnEnded += (_player) => HideDicesValue();
    }

    /// <summary>
    /// Spawn dice image based on number of rolls
    /// </summary>
    /// <param name="_nbrRoll">Number of rolls</param>
    void SpawnDices(int _nbrRoll)
    {
        diceImgs = new Image[_nbrRoll];
        for(int i = 0; i < _nbrRoll; ++i)
        {
            Image _diceImg = Instantiate(diceImgPrefab);
            _diceImg.transform.SetParent(dicePanel.transform);
            _diceImg.gameObject.SetActive(false);
            diceImgs[i] = _diceImg;
        }
    }

    /// <summary>
    /// Display a dice image at <see cref="currentNbrDisplay"/> index with the value
    /// passed in parameters
    /// </summary>
    /// <param name="_value">Value to display on dice.</param>
    void PrintDiceValue(int _value)
    {
        if (currentNbrDisplay < 0 || currentNbrDisplay >= diceImgs.Length)
            return;

        diceImgs[currentNbrDisplay].sprite = GetDiceSprite(_value - 1);
        diceImgs[currentNbrDisplay].gameObject.SetActive(true);
        currentNbrDisplay++;
    }

    /// <summary>
    /// Get sprite of a dice with specific value based on index in parameters
    /// </summary>
    /// <param name="_index">Index  of the dice sprite to get</param>
    /// <returns></returns>
    Sprite GetDiceSprite(int _index) => _index < 0 || _index >= diceSprites.Length ?
        diceSprites[0] : diceSprites[_index];

    /// <summary>
    /// Hide all dice images
    /// </summary>
    void HideDicesValue()
    {
        for(int i = 0; i < currentNbrDisplay; ++i)
            diceImgs[i].gameObject.SetActive(false);

        currentNbrDisplay = 0;
    }
    #endregion
}
