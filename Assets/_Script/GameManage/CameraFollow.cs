using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform fishTarget;
    public float speed;
    public Vector3 Offset;

    public void Start()
    {

    }
    public void Update()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, fishTarget.position, speed);
        transform.position = smoothedPosition + Offset;

    }
}
