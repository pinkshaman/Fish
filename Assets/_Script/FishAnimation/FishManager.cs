using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Transform = UnityEngine.Transform;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance;
    public Transform rootFish;
    public FishOtherHandle fishOtherHandle;
    public FishDataBase fishDataBases;
    public GameObject fishPrefabs;
    

    public FishMain fishMain;
    public GameObject PlayerFishMain;
    public Transform rootMainFish;

    public List<Transform> spawnPoints;
    public EnermyWaveList enermyWaveList;
    public GroupFish parentGroup;
    public FishSpawn fishSpawn;

    public void Awake()
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


    public void CreateFishQuest(int fishID)
    {
        FishData fishData = fishDataBases.fishDatas.Find(fish => fish.id == fishID);
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Vector3 randomPosition = spawnPoints[randomIndex].position;

        var newFish = Instantiate(fishPrefabs, randomPosition, Quaternion.identity, rootFish);
        FishOtherHandle fishOtherHandles = newFish.GetComponent<FishOtherHandle>();
        fishOtherHandles.uniqueID = newFish.GetInstanceID();

        fishOtherHandle.gameObject.SetActive(true);
        fishOtherHandles.SetData(fishData);
        
    }
    public void CreateFishGroup(int fishID)
    {
        int randomIndex = Random.Range(0, enermyWaveList.enermyWavesList.Count);
        EnermyWave enermyWave = enermyWaveList.enermyWavesList[randomIndex];

        FishData fishData = fishDataBases.fishDatas.Find(fish => fish.id == fishID);
        var newParentGroup = Instantiate(parentGroup, enermyWave.SwimPath[0], Quaternion.identity,rootFish);
        
        Vector3 randomPosition = spawnPoints[randomIndex].position;
        for (int i = 0; i < enermyWave.numberOfEnemy; i++)
        {
            var enermy = Instantiate(fishSpawn, enermyWave.spawnPoint[i].transform.position, Quaternion.identity, rootFish);
            enermy.gameObject.transform.SetParent(newParentGroup.transform);                    
            enermy.SetData(fishData);          
        }
        newParentGroup.SetDataGroup(enermyWave.SwimPath, fishData.speed);


    }
    public void DestroyFish(bool isEnd)
    {
        foreach (Transform fishObj in rootFish)
        {
            Destroy(fishObj.gameObject);
        }
    }

    public void CreateFish(FishData dataX)
    {
        Vector2 spawnPosition = transform.position;     
        var newFish = Instantiate(PlayerFishMain, spawnPosition, Quaternion.identity, rootMainFish);
        var fishHandle = newFish.GetComponent<FishMain>();
        fishHandle.gameObject.SetActive(true);
        fishHandle.SetData(dataX);

        Debug.Log($"Create fish: {newFish.name} at position {spawnPosition}");
    }


}
