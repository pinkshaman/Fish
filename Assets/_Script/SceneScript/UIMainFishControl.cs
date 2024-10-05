using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
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
        LoadPlayerData();
        var fish = fishDataBase.fishDatas.Find(fish => fish.id == playerData.fishMainID);
        fishManager.CreateFish(fish);


    }
    [ContextMenu("LoadPlayerData")]
    public void LoadPlayerData()
    {
        var defaultValue = JsonUtility.ToJson(playerData);
        var json = PlayerPrefs.GetString(nameof(playerData), defaultValue);
        Debug.Log($"Loaded JSON: {json}");
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log($"PlayerData Loaded - FishMainID: {playerData.fishMainID}");
    }

    public void Check(List<int> fishListID)
    {
        foreach (var fishID in fishListID)
        {
            var fishData = fishDataBase.fishDatas.Find(fishes => fishes.id == fishID);
            UiScore.CreateMenu(fishData);
        }
    }
    public void CheckGame(int live)
    {
        Debug.Log($"live: {live}");
        if (live == 0 || coutdownTime.isEnd == true)
        {
            isGameEnd = true;
            LoadResutl(isGameEnd);
        }
        else
        {
            isGameEnd = false;
        }
    }

    public IEnumerator RespawnFish()
    {
        yield return new WaitForSeconds(3.0f);
        foreach (Transform child in fishManager.rootMainFish)
        {
            if (child.CompareTag("FishMain"))
            {
                child.gameObject.SetActive(true);
                child.transform.position = new Vector2(0, 0);
            }
        }
    }
    public void LoadResutl(bool isEnd)
    {
        Debug.Log($"IsGame: {isEnd}");
        resultPanel.SetActive(isEnd);
        UiScore.ShowReSult();
        int fishLives = fishMain.lives;
        int scores = UiScore.ReturnScore();
        RewardManager rewardManager = FindObjectOfType<RewardManager>();
        StartCoroutine(rewardManager.CalculatorReward(isEnd,fishLives,scores));
        Debug.Log($"Waiting for Reward Intilizing: {isEnd}-{fishLives}-{scores}");

    }
}
