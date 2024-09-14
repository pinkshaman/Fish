using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveData : MonoBehaviour
{
    public PlayerDataBase playerDataBases;
    public PlayerDataProgessBase playerDataProgesses;

    [ ContextMenu("SaveDataJson")]
    public void SaveDataJson()
    {
        var value = JsonUtility.ToJson(playerDataBases);
        PlayerPrefs.SetString(nameof(playerDataBases), value);
        PlayerPrefs.Save();
    }
   
    [ContextMenu("LoadDataJson")]     
        public void LoadDataJson()
        {
            if (PlayerPrefs.HasKey(nameof(playerDataBases)))
            {
                string json = PlayerPrefs.GetString(nameof(playerDataBases));
                if (!string.IsNullOrEmpty(json))
                {
                    playerDataBases = JsonUtility.FromJson<PlayerDataBase>(json);
                    Debug.Log("Dữ liệu đã được tải.");
                }
                else
                {
                    Debug.LogWarning("Chuỗi JSON tải về là rỗng.");
                }
            }
            else
            {
                Debug.LogWarning("Dữ liệu không tồn tại trong PlayerPrefs.");
            }
        }
    [ContextMenu("SaveProgessDataJson")]
    public void SaveProgessDataJson()
    {
        var value = JsonUtility.ToJson(playerDataProgesses);
        PlayerPrefs.SetString(nameof(playerDataProgesses), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadProgessDataJson")]
    public void LoadProgessDataJson()
    {
        var defaultValue = JsonUtility.ToJson(playerDataProgesses);
        var json = PlayerPrefs.GetString(nameof(playerDataProgesses), defaultValue);
        playerDataProgesses = JsonUtility.FromJson<PlayerDataProgessBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }
    public void OnApplicationQuit()
    {
        SaveProgessDataJson();
    }
  
}
