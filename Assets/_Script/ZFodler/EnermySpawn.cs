using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySpawn : MonoBehaviour
{
    public EnermyWave[] enermyWaves;
    private int currentWave;


    private void SpawnEnermyWave()
    {
        var waveInfo= enermyWaves[currentWave];
        var startPostision = waveInfo.SwimPath[0];
        for (int i = 0; i < enermyWaves.Length; i++)
        {
            var enermy = Instantiate(waveInfo.enermPrefab, startPostision, Quaternion.identity);
            var agent = enermy.GetComponent<SwimPathAgent>();
            agent.swimPath = waveInfo.SwimPath;
            agent.Speed = waveInfo.speed;
            startPostision += waveInfo.formationOffset;

        }
        currentWave++;
        if(currentWave<enermyWaves.Length)
        {
            Invoke(nameof(SpawnEnermyWave), waveInfo.nextWaveDelay);
        }
    }
}
