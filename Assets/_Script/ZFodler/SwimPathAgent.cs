using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwimPathAgent : MonoBehaviour
{
    public SwimPath swimPath;
    public float Speed;
    private int nextIndex = 1;



    public void Update()
    {
        if (swimPath == null) return;
        if (nextIndex >= swimPath.wayPoint.Count) { return; }
        if (transform.position != swimPath[nextIndex])
        {
            SwimToNextPoint();
        }
        else
        {
            nextIndex++;
        }
    }
    public void SwimToNextPoint()
    => transform.position = Vector3.MoveTowards(transform.position, swimPath[nextIndex], Speed * Time.deltaTime);



}
