using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChooseFish : MonoBehaviour
{
    public ChooseFishDataBase chooseFishDataBase;
    public FishDataBase fishDataBase;
    public FishTankBase fishTankBase;
    public UnityEngine.UI.Button ChooseFishButton;
    public Text Message;
    public FishShowUIHandle showUIHandle;
    public FishShowUiPanel fishPanel;
    public SceneManagers loadFishTankScene;
    public void Awake()
    {
        LoadChooseFishJson();

    }
    public void Start()
    {
        ChooseFishButton.onClick.AddListener(OnChooseFish);
        foreach (var datas in chooseFishDataBase.chooseFishDataBases)
        {
            if (datas == null)
            {
                Debug.Log("dataReward is Null");
            }
            var chooseFish = fishDataBase.fishDatas.Find(chooseFish => chooseFish.id == datas.ID);

            if (chooseFish != null)
            {
                //Tạo và hiển thị cá trên UI chỉ với ChooseFishData
                showUIHandle.CreateFishShow(chooseFish);
            }
        }
    }

    public void OnChooseFish()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.OnButtonClickAudio();
        FishData fishData = showUIHandle.GetFishDataFromCurrentToggle();
        if (fishData != null)
        {
            Message.text = $"Selected : {fishData.fishName}";
            Debug.Log($"Fish selected: {fishData.fishName}");
            AddFishToTank(fishData); // Thêm trực tiếp vào FishTankBase
        }
        else
        {
            Debug.Log("No fish is selected or FishData is null.");
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

        loadFishTankScene.LoadFishTankScene();       
    }
    public void OnApplicationQuit()
    {
        SaveFishTankDataJson() ;
        PlayerManager  playerManager = FindObjectOfType<PlayerManager>();
        playerManager.OnApplicationQuit();
    }

    [ContextMenu("SaveChooseFishJson")]
    public void SaveChooseFishJson()
    {
        var value = JsonUtility.ToJson(chooseFishDataBase);
        PlayerPrefs.SetString(nameof(chooseFishDataBase), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadChooseFishJson")]
    public void LoadChooseFishJson()
    {
        var defaultValue = JsonUtility.ToJson(chooseFishDataBase);
        var json = PlayerPrefs.GetString(nameof(chooseFishDataBase), defaultValue);
        chooseFishDataBase = JsonUtility.FromJson<ChooseFishDataBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }

    [ContextMenu("SaveFishTankDataJson")]
    public void SaveFishTankDataJson()
    {
        var value = JsonUtility.ToJson(fishTankBase);
        PlayerPrefs.SetString(nameof(fishTankBase), value);
        PlayerPrefs.Save();
    }
}