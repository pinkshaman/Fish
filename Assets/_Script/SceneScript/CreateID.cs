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
        if (playerDataBase != null && playerDataBase.PlayerDataBases != null)
        {
            bool idExists = playerDataBase.PlayerDataBases.Exists(player => player.name == playerName);

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
                playerDataBase = new PlayerDataBase();
            }

            // Khởi tạo danh sách nếu null
            if (playerDataBase.PlayerDataBases == null)
            {
                playerDataBase.PlayerDataBases = new List<PlayerData>();
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
            playerAvatar = null,
            fishMainID = 0,
        };
        // Thêm đối tượng PlayerData vào danh sách trong PlayerDataBase
        //playerDataBase.PlayerDataBases.Add(newPlayerData);
        Debug.Log($"Successfully! Your name: {playerName}");

        // Lưu dữ liệu      
        saveLoadData.SavesData(newPlayerData);
        
        loadScene.LoadChooseFish();
    }
}


