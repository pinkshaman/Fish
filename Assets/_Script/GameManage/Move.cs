using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public float moveSpeed = 10;
    private Vector3 targetPosition;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            targetPosition = Vector3.zero;
        }
        
    }
}
