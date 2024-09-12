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
    public SceneManagers loadFishTankScene;
    public FishShowUIHandle showUIHandle;
    public FishShowUiPanel fishPanel;

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
        //StartCoroutine(LoadFishTankSceneWhenReady());

    }


    private IEnumerator LoadFishTankSceneWhenReady()
    {
        // Giả sử việc khởi tạo Dictionary diễn ra trong quá trình AddFish
        while (!IsDictionaryInitialized())
        {
            // Đợi 1 frame trước khi kiểm tra lại
            yield return null;
        }

        // Khi đã sẵn sàng, load scene FishTankShow
        loadFishTankScene.LoadFishTankScene();
    }

    private bool IsDictionaryInitialized()
    {
        // Kiểm tra nếu Dictionary trong FishTankBase đã được khởi tạo xong
        // (Tùy thuộc vào logic của bạn về cách xác định khi nào Dictionary được khởi tạo)
        return fishTankBase.fishTankBases != null && fishTankBase.fishTankBases.Count > 0;
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