using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishShopManager : MonoBehaviour
{
    public FishShopDataBase shopDataBase;
    public FishDataBase fishDataBase;
    public FishShowUIHandle shopUiHandle;
    public FishTankBase fishTankBase;
    public FishShopUiHandle fishSellUiHandle;
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
        LoadFishInTankForShop();
    }
    public void LoadFishInTankForShop()
    {
        LoadFishTankDataJson();
        foreach (var fishInTank in fishTankBase.fishTankBases)
        {
            FishData fishData = fishDataBase.fishDatas.Find(fish => fish.id == fishInTank.ID);
            fishSellUiHandle.CreateFishShow(fishData);
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
    [ContextMenu("LoadFishTankDataJson")]
    public void LoadFishTankDataJson()
    {
        var defaultValue = JsonUtility.ToJson(fishTankBase);
        var json = PlayerPrefs.GetString(nameof(fishTankBase), defaultValue);
        fishTankBase = JsonUtility.FromJson<FishTankBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }


}
