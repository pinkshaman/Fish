using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishShopManager : MonoBehaviour
{
    public FishShopDataBase shopDataBase;
    public FishDataBase fishDataBase;
    public FishShowUIHandle shopUiHandle;
    public FishTankBase fishTankBase;

    public void Awake()
    {
        SaveShopData();
    }
    public void Start()
    {
        LoadShopData();
        foreach (var fishShop in shopDataBase.FisShopDatas)
        {
            FishData fishData = fishDataBase.fishDatas.Find(fish => fish.id == fishShop.FishShopID);
            shopUiHandle.CreateFishShow(fishData);
        }
    }

    [ContextMenu("LoadShopData")]
    public void LoadShopData()
    {
        var defaultValue = JsonUtility.ToJson(shopDataBase);
        var json = PlayerPrefs.GetString(nameof(shopDataBase), defaultValue);
        shopDataBase = JsonUtility.FromJson<FishShopDataBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }

    [ContextMenu("SaveShopData")]
    public void SaveShopData()
    {
        var value = JsonUtility.ToJson(shopDataBase);
        PlayerPrefs.SetString(nameof(shopDataBase), value);
        PlayerPrefs.Save();
    }
}

