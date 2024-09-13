using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CreateID : MonoBehaviour
{
    public Button createButton; // Nút tạo người chơi mới 
    public InputField inputName;  //InputField cho người dùng nhập tên.
    public LoadSaveData saveLoadData; //Tham chiếu script  lưu và tải dữ liệu người chơi.
    public PlayerDataBase playerDataBase; //Đối tượng chứa danh sách các PlayerData.
    public PlayerDataProgessBase progessBase;// Đối tượng chứa danh sách các PlayerDataProgess.
    public Text Message; //Text để hiển thị thông báo cho người dùng.
    public Button xButton; //Nút thoát ứng dụng.
    public SceneManagers loadScene; // Quản lý chuyển cảnh khi tạo người chơi thành công.
    public void Start()
    {
        createButton.onClick.AddListener(OnCreateButtonClick); // lắng nghe sự kiện 
    }
    public void OnXButtonClick()
    {
        Application.Quit();   //Thoát ứng dụng khi nhấn nút xButton
        Debug.Log("Application is Quitting");
    }

    public void OnCreateButtonClick()
    {
        //Lấy tên người chơi từ inputName
       
        string playerName = inputName != null ? inputName.text : string.Empty;
        if (!string.IsNullOrEmpty(playerName))
        {
            CheckIDAndCreatePlayer(playerName); // Nếu tên không rỗng, kiểm tra tính hợp lệ của tên bằng cách gọi hàm 
        }
        else
        {
            Debug.LogWarning("input Name is blank!"); //Nếu tên rỗng, cảnh báo bằng Debug.LogWarning()
        }
    }

    public void CheckIDAndCreatePlayer(string playerName)
    {      
        saveLoadData.LoadDataJson(); //Tải dữ liệu từ saveLoadData.
        if (playerDataBase != null && playerDataBase.playerDatas != null) 
        {
            //Nếu playerDataBase và danh sách playerDatas không null, kiểm tra xem tên đã tồn tại chưa.
            bool idExists = playerDataBase.playerDatas.Exists(player => player.name == playerName);

            if (idExists) //Nếu tên đã tồn tại, cảnh báo và yêu cầu nhập tên khác.
            {
                Debug.LogWarning("Name is invalid. Please choose another name.");
                Message.text = "Name is invalid. Please choose another name.";
            }
            else//Nếu tên chưa tồn tại, gọi CreatePlayerName() để tạo người chơi.
            {
                CreatePlayerName(playerName);
            }
        }
        else //Nếu playerDataBase hoặc playerDatas null, khởi tạo danh sách và tạo người chơi
        {
            playerDataBase.playerDatas = new List<PlayerData>();
            Debug.LogWarning("PlayerDataBase is null.");
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
        loadScene.LoadChooseFish();
    }
    public void CreatePlayerProgess(PlayerData data)
    {
        // Tạo đối tượng ProgessPlayerData mới
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
        // Thêm đối tượng PlayerDataProgess vào danh sách trong PlayerDataBaseProgess
        progessBase.playerDataProgesses.Add(newPlayerDataProgess);
        saveLoadData.SaveProgessDataJson();   //Lưu dữ liệu 
    }


}