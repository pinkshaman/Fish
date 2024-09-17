using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance { get; private set; }
    public Transform rootFish;
    public FishOtherHandle fishOtherHandle;
    public FishDataBase fishDataBases;
    public GameObject fishPrefabs;
    public List<int> allFishes;

    public FishMain fishMain;
    public GameObject PlayerFishMain;
    public Transform rootMainFish;

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

    public void RegisterFish(int fishID)
    {
        if (!allFishes.Contains(fishID))
        {
            allFishes.Add(fishID);
            Debug.Log($"Add newFish: {fishID}");
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
            RegisterFish(fishOtherHandles.ID);
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
        var newFish = Instantiate(PlayerFishMain, spawnPosition, Quaternion.identity, rootMainFish);
        var fishHandle = newFish.GetComponent<FishMain>();
        fishHandle.gameObject.SetActive(true);
        fishHandle.uniqueID = newFish.GetInstanceID();
        fishHandle.SetData(dataX);
        
        Debug.Log($"Create fish: {newFish.name} at position {spawnPosition}");
    }
  
}
