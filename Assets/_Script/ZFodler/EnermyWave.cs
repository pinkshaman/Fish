using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnermyWave 
{   
    public int numberOfEnemy;  
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
