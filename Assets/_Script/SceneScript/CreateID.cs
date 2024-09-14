using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CreateID : MonoBehaviour
{
    public Button createButton;
    public InputField inputName;
    public LoadSaveData saveLoadData;
    public PlayerDataBase playerDataBase;
    public PlayerDataProgessBase progessBase;
    public Text Message;
    public Button xButton;
    public SceneManagers loadScene;
    
    public void Start()
    {
        createButton.onClick.AddListener(OnCreateButtonClick);
    }
    public void OnXButtonClick()
    {
        Application.Quit();
        Debug.Log("Application is Quitting");
    }

    public void OnCreateButtonClick()
    {
        string playerName = inputName != null ? inputName.text : string.Empty;
        if (!string.IsNullOrEmpty(playerName))
        {
            CheckIDAndCreatePlayer(playerName);
        }
        else
        {
            Debug.LogWarning("input Name is blank!");
        }
    }
   

    public void CheckIDAndCreatePlayer(string playerName)
    {  
        if (playerDataBase != null && playerDataBase.playerDatas != null)
        {
            bool idExists = playerDataBase.playerDatas.Exists(player => player.name == playerName);

            if (idExists)
            {
                Debug.LogWarning("Name is invalid. Please choose another name.");
                Message.text = "Name is invalid. Please choose another name.";
            }
            else
            {
                CreatePlayerName(playerName);
            }
        }
        else
        {
            if (playerDataBase == null)
            {
                Debug.LogWarning("PlayerDataBase is null.");
                // Tạo một instance mới nếu cần
                playerDataBase = ScriptableObject.CreateInstance<PlayerDataBase>();
            }

            // Khởi tạo danh sách nếu null
            if (playerDataBase.playerDatas == null)
            {
                playerDataBase.playerDatas = new List<PlayerData>();
            }

            CreatePlayerName(playerName);
        }
    }


    public void CreatePlayerName(string playerName)
    {
        // Tạo đối tượng PlayerData mới
        PlayerData newPlayerData = new PlayerData
        {
            ID = GetInstanceID(),
            name = playerName,
            level = 1,
            playerExperience = 0f,
            whilePearl = 0,
            blackPearl = 0,
            playerAvatar = null
        };
        // Thêm đối tượng PlayerData vào danh sách trong PlayerDataBase
        playerDataBase.playerDatas.Add(newPlayerData);      
        Debug.Log($"Successfully! Your name: {playerName}");

        // Lưu dữ liệu      
        saveLoadData.SaveDataJson();
        // Tạo progess data 
        CreatePlayerProgess(newPlayerData);
       
    }
    public void CreatePlayerProgess(PlayerData data)
    {
        PlayerDataProgess newPlayerDataProgess = new PlayerDataProgess
        {
            progessID = data.ID,
            progessLevel = data.level,
            progessName = data.name,
            progessBlackPearl = data.blackPearl,
            progessWhilePearl = data.whilePearl,
            progessPlayerAvatar = data.playerAvatar,
            progessplayerExperience = data.playerExperience,
        };
        progessBase.playerDataProgesses.Add(newPlayerDataProgess);
        saveLoadData.SaveProgessDataJson();
        loadScene.LoadChooseFish();
    }
}


