using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupFish : MonoBehaviour
{
    public SwimPathAgent swimPathAgent;

    public void Start()
    {
        
    }
    public void MoveToEnd()
    {
        swimPathAgent.SwimToNextPoint();
    }
    public void SetDataGroup(SwimPath swimPath, float Speed)
    {
        this.swimPathAgent.swimPath = swimPath;
        this.swimPathAgent.Speed =  Speed;

    }
    public void Update()
    {
        MoveToEnd();
    }
}
