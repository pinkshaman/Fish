using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerUi playerUI;
    public SceneManagers sceneManagers;
    public PlayerData playerData;
    public LoadSaveData loadSaveData;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        LoadPlayerData();

        Debug.Log("Data Loaded: " + playerData.name);
        CreatePlayerData(playerData);
    }
    public void CreatePlayerData(PlayerData playerdata)
    {
        playerUI.SetDataPlayer(playerdata);
    }
    public int GetFishMainID()
    {
        
        return playerData.fishMainID;
    }


    public void UpdateProgessData(PlayerData changedData)
    {
        this.playerData = changedData;
        playerUI.UpdateUIplayerData(changedData);
    }


    [ContextMenu("LoadPlayerData")]
    public void LoadPlayerData()
    {
        var defaultValue = JsonUtility.ToJson(playerData);
        var json = PlayerPrefs.GetString(nameof(playerData), defaultValue);
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("LoadProgess is Loaded");
    }
    [ContextMenu("SavePlayersData")]
    public void SavePlayersData()
    {
        var value = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(nameof(playerData), value);
        PlayerPrefs.Save();
    }
}
