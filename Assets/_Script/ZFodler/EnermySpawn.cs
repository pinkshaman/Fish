using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySpawn : MonoBehaviour
{
    public EnermyWaveList enermyWavesLists;
    private int currentWave;
    public FishSpawn fishSpawn;
    public FishDataBase fishDataBase;
    public GroupFish parentGroup;


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
            var newParentGroup = Instantiate(parentGroup, enermyWave.SwimPath[0], Quaternion.identity, enermyWave.SwimPath.transform);

            for (int i = 0; i < enermyWave.numberOfEnemy; i++)
            {
                var enermy = Instantiate(fishSpawn, enermyWave.spawnPoint[i].transform.position, Quaternion.identity);
                enermy.SetData(newFish);
                enermy.gameObject.transform.SetParent(newParentGroup.transform);

            }
            newParentGroup.SetDataGroup(enermyWave.SwimPath, newFish.speed);

            currentWave++;
            if (currentWave < enermyWave.numberOfWave)
            {
                Invoke(nameof(SpawnEnermyWave), enermyWave.nextWaveDelay);
            }

        }
    }

}
