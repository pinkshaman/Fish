using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveData : MonoBehaviour
{
    public PlayerDataBase playerDataBases;


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
}
