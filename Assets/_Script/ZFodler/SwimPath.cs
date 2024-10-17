using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimPath : MonoBehaviour
{
    public List<GameObject> wayPoint;
    public Vector3 this[int index] => wayPoint[index].transform.position;
    private void OnDrawGizmos()
    {
        if (wayPoint == null) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < wayPoint.Count - 1; i++)
        {
            Gizmos.DrawLine(wayPoint[i].transform.position, wayPoint[i + 1].transform.position);
        }
    }
}
