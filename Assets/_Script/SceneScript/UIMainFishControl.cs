using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Interstitial ads;
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
        var sortedFishList = fishListID.Select(fishID => fishDataBase.fishDatas.Find(fishes => fishes.id == fishID)) 
        .OrderBy(fishData => fishData.scalePoint) 
        .ToList();
        foreach (var fishID in sortedFishList)
        {
            UiScore.CreateMenu(fishID);
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
        isGameEnd = isEnd;

        Debug.Log($"IsGame: {isEnd}");
        resultPanel.SetActive(isEnd);
        UiScore.ShowReSult();
        ads.ShowAd();
        int fishLives = UiScore.ReturnLives();
        int scores = UiScore.ReturnScore();
        RewardManager rewardManager = FindObjectOfType<RewardManager>();
        StartCoroutine(rewardManager.CalculatorReward(isEnd, fishLives, scores));
        Debug.Log($"Waiting for Reward Intilizing: {isEnd}-{fishLives}-{scores}");

    }
}
