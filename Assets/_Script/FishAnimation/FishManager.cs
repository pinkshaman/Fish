using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    public List<Transform> spawnPoints;

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
            Debug.Log($"Add OtherFishTolist: {fishID}");
            allFishes.Sort();
        }
    }

    public void CreateFishQuest(int fishID)
    {
        FishData fishData = fishDataBases.fishDatas.Find(fish => fish.id == fishID);
        int randomIndex = Random.Range(0, spawnPoints.Count); 
        Vector3 randomPosition = spawnPoints[randomIndex].position;
      
        var newFish = Instantiate(fishPrefabs, randomPosition, Quaternion.identity,rootFish);
        FishOtherHandle fishOtherHandles = newFish.GetComponent<FishOtherHandle>();
        fishOtherHandles.uniqueID = newFish.GetInstanceID(); 
        
        fishOtherHandles.SetData(fishData);    
        Debug.Log($"Create fish: {newFish.name} at position {randomPosition}");
        RegisterFish(fishOtherHandles.ID);
    }
    
    public void CreateFish(FishData dataX)
    {
        Vector2 spawnPosition = transform.position;

        // Tạo đối tượng cá mới từ prefab và gán nó vào rootFish
        var newFish = Instantiate(PlayerFishMain, spawnPosition, Quaternion.identity, rootMainFish);
        var fishHandle = newFish.GetComponent<FishMain>();
        fishHandle.gameObject.SetActive(true);
        fishHandle.SetData(dataX);

        Debug.Log($"Create fish: {newFish.name} at position {spawnPosition}");
    }

}
