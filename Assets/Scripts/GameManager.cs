using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple GameManager Instances!");
        }
        else
        {
            instance = this;
        }
    }
    #region PlayerTracking
    private const string PLAYER_ID_PREFIX = "Player ";
    private static Dictionary<string, Player> playerDictionary = new Dictionary<string, Player>();
    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        _player.transform.name = _playerID;
        _player.playerID = _playerID;
        playerDictionary.Add(_playerID, _player);
    }
    public static void UnRegisterPlayer(string _playerID)
    {
        playerDictionary.Remove(_playerID);
    }
    public static Player GetPlayer(string _playerID)
    {
        return playerDictionary[_playerID];
    }
    #endregion
}
