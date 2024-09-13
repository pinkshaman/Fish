using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public FishMain fishMain;
    public PlayerUi playerUI;
    public SceneManagers sceneManagers;
    public PlayerDataBase playerDataBase;
    public PlayerDataProgessBase playerDataProgess;
    public LoadSaveData loadSaveData;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại PlayerManager qua các Scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        loadSaveData.LoadProgessDataJson();
        foreach (var progessData in playerDataProgess.playerDataProgesses)//Duyệt qua danh sách playerDataProgesses trong playerDataProgess.
        {
            //Tìm đối tượng PlayerData tương ứng trong playerDataBase dựa trên ID,
            var playerData = playerDataBase.playerDatas.Find(playerData=> playerData.ID==progessData.progessID);
            //gọi hàm CreatePlayerData() để tạo dữ liệu người chơi tương ứng từ tiến độ đã lưu.
            CreatePlayerData(progessData);
        }
    }
    public void SetMainFish(FishData fishData)
    {
        if (fishMain == null)
        {
            fishMain = FindObjectOfType<FishMain>(); // Lấy FishMain từ Scene hiện tại nếu chưa có
        }

        if (fishMain != null)
        {
            fishMain.SetDataFishMain(fishData); // Gán dữ liệu cho FishMain
        }
    }
    public void CreatePlayerData(PlayerDataProgess playerData)
    {
        var progessData = Instantiate(playerUI); //Tạo một instance của playerUI để cập nhật giao diện người chơi.
        progessData.UpdateDataFormProgess(playerData); //Cập nhật dữ liệu từ dâta 
    }
    public void UpdateProgessData(PlayerDataProgess progessPlayerData)
    {
        //Tìm vị trí của đối tượng PlayerDataProgess cần cập nhật trong danh sách playerDataProgesses dựa trên progessID.
        var dataIndex = playerDataProgess.playerDataProgesses.FindIndex(progess => progessPlayerData.progessID == progess.progessID);
        //Cập nhật lại dữ liệu tiến độ cho vị trí đó.
        playerDataProgess.playerDataProgesses[dataIndex] = progessPlayerData;
        //Cập nhật giao diện người chơi
        playerUI.UpdateProgessPlayerData(progessPlayerData);
    }
}
