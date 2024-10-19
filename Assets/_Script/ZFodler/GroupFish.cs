using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupFish : MonoBehaviour
{
    public SwimPathAgent swimPathAgent;
    protected Vector2 movement;

    public void Start()
    {

    }
    public void MoveToEnd()
    {
        swimPathAgent.SwimToNextPoint();
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = swimPathAgent.swimPath[6];
        if(currentPosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void SetDataGroup(SwimPath swimPath, float Speed)
    {
        this.swimPathAgent.swimPath = swimPath;
        this.swimPathAgent.Speed = Speed;

    }
   
    public void Update()
    {
        MoveToEnd();
    }
}
