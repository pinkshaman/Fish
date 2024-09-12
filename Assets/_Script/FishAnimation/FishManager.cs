using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance { get; private set; }
    public Transform rootFish;
    public FishOtherHandle fishOtherHandle;
    public FishDataBase fishDataBases;
    public GameObject fishPrefabs;
    public List<FishHandle> allFishes;
    //public FishShowUIHandle fishShowUIHandle;
    public FishHandle fishHandle;
    public GameObject fishPlayerPrefabs;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {

    }
    public void RegisterFish(FishHandle fish)
    {
        if (!allFishes.Contains(fish))
        {
            allFishes.Add(fish);
            Debug.Log($"Add newFish: {fish.uniqueID}");
        }
    }
    public void CreateFishQuest(QuestDataTest questData)
    {
        for (int i = 0; i < questData.quality; i++)
        {
            Vector2 spawnPosition = questData.fishPositions;

            // Tạo đối tượng cá mới từ prefab và gán nó vào rootFish
            var newFish = Instantiate(fishPrefabs, spawnPosition, Quaternion.identity, rootFish);
            FishOtherHandle fishOtherHandles = newFish.GetComponent<FishOtherHandle>();         
            fishOtherHandles.uniqueID = newFish.GetInstanceID();
            fishOtherHandles.SetData(questData);
            Debug.Log($"Create fish: {newFish.name}:{questData.quality} at position {spawnPosition}");
            RegisterFish(fishOtherHandles);
        }
    }
    // Phương thức để tạo nhiều cá dựa trên thông tin cung cấp
    public void CreateFishesFromDataQuest(List<QuestDataTest> questDataTests)
    {
        foreach (var questData in questDataTests)
        {
            CreateFishQuest(questData);
        }
    }
    public void CreateFishesFromData(List<FishData> data)
    {
        foreach (var fishdata in data)
        {
            CreateFish(fishdata);
        }
    }
    public void CreateFish(FishData dataX)
    {
        Vector2 spawnPosition = transform.position;

        // Tạo đối tượng cá mới từ prefab và gán nó vào rootFish
        var newFish = Instantiate(fishPrefabs, spawnPosition, Quaternion.identity, rootFish);
        var fishHandle = newFish.GetComponent<FishHandle>();
        fishHandle.SetData(dataX);
        fishHandle.uniqueID = newFish.GetInstanceID();
        //fishShowUIHandle.CreateFishShow(dataX);
        Debug.Log($"Create fish: {newFish.name} at position {spawnPosition}");
    }   
}
