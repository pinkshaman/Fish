using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Move : MonoBehaviour
{
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    public Vector3 currentDirection;
    public void MoveFish()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentDirection = targetPosition - transform.position;
            currentDirection.z = 0;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        }
        else
        {
            targetPosition = Vector3.zero;
        }

    }
    public Vector3 GetCurrentDirection()
    {
        return currentDirection;
    }
    void Update()
    {
       MoveFish();      
    }
}
