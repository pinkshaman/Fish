using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySpawn : MonoBehaviour
{
    public EnermyWaveList enermyWavesLists;
    private int currentWave;

    public FishSpawn fishSpawn;
    public FishDataBase fishDataBase;
    public GameObject parentGroup;
    public void Start()
    {
        Debug.Log("Start");
        SpawnEnermyWave();
    }
    public void SpawnEnermyWave()
    {
        foreach (var enermyWave in enermyWavesLists.enermyWavesList)
        {
            FishData newFish = fishDataBase.fishDatas.Find(fish => fish.id == enermyWave.fishDataID);
            var newParentGroup = Instantiate(parentGroup, enermyWave.SwimPath[0], Quaternion.identity);

            for (int i = 0; i < enermyWave.numberOfEnemy; i++)
            {
                var randomindexSpawn = Random.Range(0, enermyWave.spawnPoint.Count);
                Vector3 spawnPosition = enermyWave.spawnPoint[randomindexSpawn].transform.position;
                var enermy = Instantiate(fishSpawn, spawnPosition, Quaternion.identity);
                enermy.SetData(newFish);
                enermy.gameObject.transform.SetParent(newParentGroup.transform);
            }
            SwimPathAgent swimPathAgent = FindObjectOfType<SwimPathAgent>();
            swimPathAgent.swimPath = enermyWave.SwimPath;
            newParentGroup.transform.position = swimPathAgent.Swim();
            currentWave++;
            if (currentWave < enermyWave.numberOfWave)
            {
                Invoke(nameof(SpawnEnermyWave), enermyWave.nextWaveDelay);
            }

        }
    }

}
