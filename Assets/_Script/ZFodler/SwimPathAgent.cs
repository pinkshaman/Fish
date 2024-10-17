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
            if (nextIndex >= swimPath.wayPoint.Count)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SwimToNextPoint()
    => transform.position = Vector3.MoveTowards(transform.position, swimPath[nextIndex], Speed * Time.deltaTime);
    public Vector3 Swim()
        => transform.position = Vector3.MoveTowards(transform.position, swimPath[nextIndex], Speed * Time.deltaTime);


}
