using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMainFishControl : MonoBehaviour
{
    public FishDataBase fishDataBase;
    public FishMain fishMain;
    public FishManager fishManager;
    public PlayerData playerData;
    public UIScore UiScore;
    public CoutdownTime coutdownTime;
    public GameObject resultPanel;
    public bool isGameEnd;
    public void Start()
    {
        fishManager = FindObjectOfType<FishManager>();
        LoadPlayerData();
        var fish = fishDataBase.fishDatas.Find(fish => fish.id == playerData.fishMainID);
        fishManager.CreateFish(fish);
        Check();
        coutdownTime = FindObjectOfType<CoutdownTime>();
    }
    [ContextMenu("LoadPlayerData")]
    public void LoadPlayerData()
    {
        var defaultValue = JsonUtility.ToJson(playerData);
        var json = PlayerPrefs.GetString(nameof(playerData), defaultValue);

        // Debug để kiểm tra xem giá trị của json có chính xác không
        Debug.Log($"Loaded JSON: {json}");

        playerData = JsonUtility.FromJson<PlayerData>(json);

        // Debug để kiểm tra xem playerData có được nạp đúng không
        Debug.Log($"PlayerData Loaded - FishMainID: {playerData.fishMainID}");
    }

    public void Check()
    {
        foreach (var ID in fishManager.allFishes)
        {
            var fishData = fishDataBase.fishDatas.Find(fishes => fishes.id == ID);
            UiScore.CreateMenu(fishData);
        }
    }
    public void LoadResutl(bool isEnd)
    {  
        Debug.Log($"IsGame: {isEnd}");
        resultPanel.SetActive(isEnd);
        UiScore.ShowReSult();
    }
}
