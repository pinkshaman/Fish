using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnermyWave 
{
    public int waveID;
    public FishData fishData;
    public int numberOfEnemy;
    public int numberOfWave;
    public SwimPath SwimPath;
    public float speed;
    public float nextWaveDelay;
    
}
[Serializable]
public class EnermyWaveList
{
    public List<EnermyWave> enermyWavesList;
}
