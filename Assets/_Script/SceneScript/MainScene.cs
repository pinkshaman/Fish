using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public SceneManagers sceneManagers;
    public Button buttonShop;
    public Button buttonFishTank;
    public PlayerDataBase playerDataBase;
    public PlayerUi playerUIPanel;
    public QuestDataBaseTest questData;
   
    public void Start()
    {
        foreach (var playerID in playerDataBase.playerDatas)
        {
            CreateData(playerID);
        }
    }
    public void OnButtonShopClick()
    {
        sceneManagers.LoadShopScene();
    }
    public void OnButtonFishTankClick()
    {
        sceneManagers.LoadFishTankScene();
    }

    public void CreateData(PlayerData dataX)
    {
        var data = Instantiate(playerUIPanel);
        data.SetDataPlayer(dataX);

    }
    [ContextMenu("SaveDataJson")]
    public void SaveDataJson()
    {
        var value = JsonUtility.ToJson(playerDataBase);
        PlayerPrefs.SetString(nameof(playerDataBase), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadDataJson")]
    public void LoadDataJson()
    {
        var defaultValue = JsonUtility.ToJson(playerDataBase);
        var json = PlayerPrefs.GetString(nameof(playerDataBase), defaultValue);
        playerDataBase = JsonUtility.FromJson<PlayerDataBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }
    public void OnApplicationQuit()
    {
        SaveDataJson();
    }
    public void UpdatePlayerData(PlayerData playerData)
    {
        var data = playerDataBase.playerDatas.FindIndex(playerdata =>playerdata.ID == playerData.ID);
        playerDataBase.playerDatas[data] = playerData;
        playerUIPanel.UpdateUIplayerData(playerData);
    }
}
