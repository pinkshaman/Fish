using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySpawn : MonoBehaviour
{
    public EnermyWaveList enermyWavesLists;
    private int currentWave;
    public int fishId;
    public int waveID;
    public FishOtherHandle fishOtherHandle;
    public FishDataBase fishDataBase;

    public void Start()
    {
        Debug.Log("Start");
        SpawnEnermyWave();
    }
    private void SpawnEnermyWave()
    {
        foreach (var enermyWave in enermyWavesLists.enermyWavesList)
        {
            if (enermyWave.waveID == waveID)
            {
                FishData newFish = fishDataBase.fishDatas.Find(fish => fish.id == fishId);             
                var enermy = Instantiate(fishOtherHandle, enermyWave.SwimPath[0], Quaternion.identity);
               
                enermy.SetData(newFish);
                
            }
            currentWave++;
            if (currentWave < enermyWave.numberOfWave)
            {
                Invoke(nameof(SpawnEnermyWave), enermyWave.nextWaveDelay);
            }
        }
    }
}
