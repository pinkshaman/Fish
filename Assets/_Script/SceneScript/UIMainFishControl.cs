using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainFishControl : MonoBehaviour
{
    public FishDataBase fishDataBase;
    public FishMain fishMain;
    public FishManager fishManager;
    public PlayerData playerData;
    public int Abilityscore;
    public int score;
    public float scalePoint;
    public int lives;
    public Time time;
     
    public UIScore UiScore;
    public void Start()
    {
        fishManager = FindObjectOfType<FishManager>();
        LoadPlayerData();
        var fishID = playerData.fishMainID;
        FishData fish = fishDataBase.fishDatas.Find(fish => fish.id == fishID);
        fishManager.CreateFish(fish);
        Check();
        
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
    public void SetData(FishMain fishmain)
    {
        this. scalePoint = fishmain.scalePoint;
        this. score = fishmain.fishPoint;
        this. lives = fishmain.lives;
        UiScore.SetMenuData(scalePoint,score, lives,Abilityscore);
    }
    public void UpdateData()
    {

        
    }
    public void Check()
    {
        foreach (var ID in fishManager.allFishes)
        {
            var fishData = fishDataBase.fishDatas.Find(fishes => fishes.id == ID);
            UiScore.CreateMenu(fishData);
        }
    }
  
}
