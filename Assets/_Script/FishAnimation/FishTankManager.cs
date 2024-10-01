using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishTankManager : MonoBehaviour
{
    public FishDataBase fishDataBase;
    public FishTankBase fishTankBase;
    public FishShowUIHandle showUi;
    public FishinTankHandle fishinTankHandle;

    public Transform rootTank;
    public GameObject fishTankPrefabs;
    public Vector2 moveAreaMin;
    public Vector2 moveAreaMax;


    public void Awake()
    {
        LoadFishTankDataJson();

    }
    public void Start()
    {      
        foreach (var fishTanks in fishTankBase.fishTankBases)
        {
            var chooseFish = fishDataBase.fishDatas.Find(chooseFish => chooseFish.id == fishTanks.ID);
            Debug.Log($"Fish in fishTank{chooseFish}");
            CreateFish(chooseFish);
        }
    }
    public void CreateFish(FishData dataX)
    {
        Debug.Log("Creating fish for data: " + dataX.id);
        // Tạo đối tượng cá mới từ prefab và gán nó vào rootFish
        Vector2 randomPos = GenerateRandomPosition();
        var newFish = Instantiate(fishTankPrefabs, randomPos, Quaternion.identity, rootTank);
        var fishComponent = newFish.GetComponent<FishinTankHandle>();
        fishComponent.uniqueID = newFish.GetInstanceID();

        fishComponent.SetData(dataX);
        showUi.CreateFishShow(dataX);
    }

    private Vector2 GenerateRandomPosition()
    {
        float randomX = UnityEngine.Random.Range(moveAreaMin.x, moveAreaMax.x);
        float randomY = UnityEngine.Random.Range(moveAreaMin.y, moveAreaMax.y);
        return new Vector2(randomX, randomY);
    }

    public void AddFishToTank(FishData data)
    {
        FishInTank newFishTank = new FishInTank
        {
            ID = data.id
        };
        fishTankBase.fishTankBases.Add(newFishTank);
        SaveFishTankDataJson();
        Debug.Log("Fish added to tank");
    }
    public void RemoveFishFromTank(FishInTank data)
    {
        fishTankBase.fishTankBases.Remove(data);
    }
 
    [ContextMenu("SaveFishTankDataJson")]
    public void SaveFishTankDataJson()
    {
        var value = JsonUtility.ToJson(fishTankBase);
        PlayerPrefs.SetString(nameof(fishTankBase), value);
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
    public void OnApplicationQuit()
    {
        SaveFishTankDataJson();      
    }
}
