using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnermyWave 
{
    public int waveID;
    public int fishDataID;
    public int numberOfEnemy;
    public int numberOfWave;
    public SwimPath SwimPath;
    public float speed;
    public float nextWaveDelay;
    public List<GameObject> spawnPoint;
    
}
[Serializable]
public class EnermyWaveList
{
    public List<EnermyWave> enermyWavesList;
}
