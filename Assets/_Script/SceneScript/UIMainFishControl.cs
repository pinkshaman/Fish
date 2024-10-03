﻿using System.Collections;
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
    public void CheckGame()
    {
        if (fishMain.lives == 0 || coutdownTime.isEnd == true)
        {
            isGameEnd = true;
            LoadResutl(isGameEnd);
        }
    }
    public void LoadResutl(bool isEnd)
    {
        Debug.Log($"IsGame: {isEnd}");
        resultPanel.SetActive(isEnd);
        UiScore.ShowReSult();
    }


}
