using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : MonoBehaviour
{
    public FishDataBase fishDataBase;
    public FishTankBase fishTankBase;
    public FishShowUIHandle showUIHandle;
    public Button BuyFishButton;
    public Text Message;
    public SceneManagers loadScene;
    public PlayerData playerDatas;
    public Text whitePearl;
    public Text BlackPearl;
    public LoadSaveData loadSaveData;
    public void Start()
    {
        BuyFishButton.onClick.AddListener(OnBuyFish);      
        LoadFishTankDataJson();
        loadSaveData.LoadData();
        playerDatas = loadSaveData.playerData;
        UpdateBalance();
    }

    public void UpdateBalance()
    {
        whitePearl.text = playerDatas.whilePearl.ToString();
        BlackPearl.text = playerDatas.blackPearl.ToString();
    }   
    public void OnBuyFish()
    {

        FishData fishData = showUIHandle.GetFishDataFromCurrentToggle();
        if (fishData != null && playerDatas.whilePearl >= fishData.Price)
        {
            playerDatas.whilePearl -= fishData.Price;
            Message.text = $"Selected : {fishData.fishName}";
            Debug.Log($"Fish selected: {fishData.fishName}");
            AddFishToTank(fishData);
            UpdateBalance();
            loadSaveData.playerData = playerDatas;
            loadSaveData.SavesData(loadSaveData.playerData);
        }
        else if (playerDatas.whilePearl <= fishData.Price)
        {
            Message.text = $"Not Enough WhilePearl ";
        }
        else
        {

            Message.text = "No fish is selected";
        }
    }
    private void AddFishToTank(FishData fishData)
    {
        FishInTank newFish = new FishInTank
        {
            ID = fishData.id,
        };

        fishTankBase.fishTankBases.Add(newFish);
        SaveFishTankDataJson();
        Debug.Log("Fish added to tank list in FishTankBase.");
        Message.text = $"{fishData.fishName} has added to tank";
    }
  
   
    [ContextMenu("SaveFishTankDataJson")]
    public void SaveFishTankDataJson()
    {
        var value = JsonUtility.ToJson(fishTankBase);
        PlayerPrefs.SetString(nameof(fishTankBase), value);
        PlayerPrefs.Save();
    }
    public void LoadFishTankDataJson()
    {
        var defaultValue = JsonUtility.ToJson(fishTankBase);
        var json = PlayerPrefs.GetString(nameof(fishTankBase), defaultValue);
        fishTankBase = JsonUtility.FromJson<FishTankBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }

    public void OnclickEffect()
    {
        AudioManager audio = FindAnyObjectByType<AudioManager>();
        audio.OnButtonClickAudio();
    }
    private void OnApplicationQuit()
    {
        SaveFishTankDataJson();
        loadSaveData.playerData = playerDatas;
        loadSaveData.SavesData(loadSaveData.playerData);
    }
}
