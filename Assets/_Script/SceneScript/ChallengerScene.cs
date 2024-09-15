using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerScene : MonoBehaviour
{
    public FishDataBase fishDataBase;
    public FishHandle fishMain;
    public FishManager fishManager;
    public PlayerData playerData;
    public void Start()
    {

        LoadPlayerData();
        var fishID = playerData.fishMainID;
        FishData fish = fishDataBase.fishDatas.Find(fish => fish.id == fishID);
        fishManager.CreateFish(fish);

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


}
