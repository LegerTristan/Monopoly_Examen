using System.Collections.Generic;
using UnityEngine;
//using UnityToolbox.GizmosTools;

/// <summary>
/// Base class of the projet that manages players and the spawn point for them.
/// 
/// (Cette classe n'a pas été utilisé comme je voulais par manque de temps, 
/// l'objectif de cette classe à la base était de faire spawn les managers dans la scène.
/// De ce fait, le gamemode aurait été la seule classe "manager" nécessaire dans la scène, pendant que le GameManager
/// assurait la gestion du jeu au runtime. J'aurais potentiellement pu aussi rajouter un shortcut 
/// dans le menu d'Unity.)
/// </summary>
public class GameMode : MonoBehaviour
{
    #region F/P
    /// <summary>
    /// Minimum player required in order to launch the game
    /// </summary>
    const int MINIMUM_PLAYER_REQUIRED = 2;

    /// <summary>
    /// List of character prefabs, it is edit by the user at runtime.
    /// </summary>
    [SerializeField, Header("PlayerManager")]
    List<MonopolyCharacter> characterPrefabs = new List<MonopolyCharacter>();

    List<MonopolyCharacter> characters = new List<MonopolyCharacter>();

    [SerializeField]
    MonopolyGameManager gameManager = null;

    [SerializeField]
    MonopolyCharacterManager characterManager = null;

    /// <summary>
    /// Spawn point of players.
    /// </summary>
    [SerializeField]
    Transform spawnPoint = null;

    /// <summary>
    /// Money at the start of the game for every player
    /// </summary>
    [SerializeField, Range(1, 5000)]
    int startMoney = 1500;

    #region Debug
    /// <summary>
    /// Debug color for spawn point
    /// </summary>
    [SerializeField, Header("Debug")]
    Color debugColor = Color.yellow;

    /// <summary>
    /// Debug radius for spawn point
    /// </summary>
    [SerializeField, Range(0.01f, 1f)]
    float debugRadius = 0.1f;
    #endregion

    bool IsGameValid => characterPrefabs.Count >= MINIMUM_PLAYER_REQUIRED 
        && characterManager && spawnPoint && gameManager;

    #endregion

    #region UnityEvents
    void Start() => InitGame();
    #endregion

    #region CustomMethods
    /// <summary>
    /// Binds gameManager to characterManager and init gameManager and call <see cref="SpawnPlayers"/>
    /// </summary>
    private void InitGame()
    {
        if (!IsGameValid)
            return;

        gameManager.OnGameRestarted += () =>
        {
            for (int i = 0; i < characters.Count; i++)
                characters[i].Money.Current = startMoney;

            characterManager.InitCharacters(characters, false);
        };
        gameManager.Init(characterManager);
        SpawnPlayers();
    }

    /// <summary>
    /// Spawn every players with the start money and passed them to characterManager.
    /// </summary>
    void SpawnPlayers()
    {
        for (int i = 0; i < characterPrefabs.Count; i++)
        {
            if (characterPrefabs[i] == null)
                continue;

            MonopolyCharacter _newPlayer = Instantiate(characterPrefabs[i]);
            if (_newPlayer == null)
                continue;

            _newPlayer.Money.Current = startMoney;
            characters.Add(_newPlayer);
        }

        characterManager.InitCharacters(characters);
    }

    private void OnDrawGizmos()
    {
        if (!spawnPoint)
            return;

        Gizmos.color = debugColor;
        Gizmos.DrawWireSphere(spawnPoint.position, debugRadius);
    }
    #endregion
}
