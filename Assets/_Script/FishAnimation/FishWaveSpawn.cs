using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWaveSpawn :FishHandle
{
    SwimPathAgent swimPathAgent;


    public override void Move()
    {
       swimPathAgent.SwimToNextPoint();
    }
}
