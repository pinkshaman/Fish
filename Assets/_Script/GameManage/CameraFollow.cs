using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform fishTarget;
    public float speed;
    public Vector3 Offset;
    public Vector2 minBounds; 
    public Vector2 maxBounds;


    public void Start()
    {

    }

    public void Update()
    {
        var fishMain = FindAnyObjectByType<FishMain>();     
       
        Vector3 desiredPosition = fishMain.gameObject.transform.position + Offset;
        if(!fishMain.gameObject.activeSelf)
        {
            Debug.Log("FishMain Respawn");
            return;
        }

        // Giới hạn vị trí của camera trong vùng cho phép
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, speed);
        transform.position = smoothedPosition ;

    }
}
