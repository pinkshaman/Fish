using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveData : MonoBehaviour
{
    //public PlayerDataBase playerDataBases;
    public PlayerData playerData;



    //[ContextMenu("SaveDataJson")]
    //public void SaveDataJson()
    //{
    //    var value = JsonUtility.ToJson(playerDataBases.PlayerDataBases);
    //    PlayerPrefs.SetString(nameof(playerDataBases), value);
    //    PlayerPrefs.Save();
    //}

    //[ContextMenu("LoadDataJson")]
    //public void LoadDataJson()
    //{
    //    var defaultValue = JsonUtility.ToJson(playerDataBases.PlayerDataBases);
    //    var json = PlayerPrefs.GetString(nameof(playerDataBases), defaultValue);
    //    playerDataBases = JsonUtility.FromJson<PlayerDataBase>(json);
    //    Debug.Log("LoadDataJson is Loaded");

    //}
    [ContextMenu("LoadData")]
    public void LoadData()
    {
        if (PlayerPrefs.HasKey(nameof(playerData)))
        {
            var defaultValue = JsonUtility.ToJson(playerData);
            var json = PlayerPrefs.GetString(nameof(playerData), defaultValue);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("LoadProgess is Loaded");
        }
        else
        {
            Debug.LogWarning("No saved data found in PlayerPrefs.");
            
        }
    }
    [ContextMenu("SavesData")]
    public void SavesData(PlayerData playerData)
    {
        var value = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(nameof(playerData), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("SetFishMain")]
    public void SetFishMain(PlayerData currentPlayer, int fishMainID)
    {
        var defaultValue = JsonUtility.ToJson(playerData);
        var json = PlayerPrefs.GetString(nameof(playerData), defaultValue);
        currentPlayer = JsonUtility.FromJson<PlayerData>(json);
        currentPlayer.fishMainID = fishMainID;
        string updatedPlayerDataJson = JsonUtility.ToJson(currentPlayer);
        PlayerPrefs.SetString(nameof(playerData), updatedPlayerDataJson);
        PlayerPrefs.Save();

    }
   

}
