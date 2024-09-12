using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateID : MonoBehaviour
{
    public Button createButton;
    public InputField inputName;
    public LoadSaveData saveData;
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

    private void CheckIDAndCreatePlayer(string playerName)
    {
        // Kiểm tra xem ID có tồn tại trong PlayerDataBase không
        bool idExists = playerDataBase.playerDatas.Exists(player => player.name == playerName);

        if (idExists)
        {
            Debug.LogWarning("Name is invalid. Please choose orther name");
            Message.text = " Name is invalid.Please choose another name";
        }
        else
        {
            CreatePlayerName(playerName);
        }
    }
    private void CreatePlayerName(string playerName)
    {
        // Tạo đối tượng PlayerData mới
        PlayerData newPlayerData = new PlayerData { name = playerName };

        // Thêm đối tượng PlayerData vào danh sách trong PlayerDataBase
        playerDataBase.playerDatas.Add(newPlayerData);
        Debug.Log($"Successfully! Your name: {playerName}");

        // Lưu dữ liệu      
        saveData.SaveDataJson();
        loadScene.LoadChooseFish();
    }
    
}
