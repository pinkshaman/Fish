using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public GameObject bubble;
    public Vector3 initialPosition;
    public float riseSpeed;
    public float bounceSpeed;
    public float bounceAmout;
    public float MaxHeight;
    public void Start()
    {
       
        initialPosition = transform.position;
    }
    public void BubbleEffect()
    {
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
        float BounceOffsetX = Mathf.Sin(bounceSpeed * Time.deltaTime) * bounceAmout;
        float BounceOffetY = Mathf.Cos(bounceSpeed * Time.deltaTime) * bounceAmout;
        transform.position = new Vector3(initialPosition.x + BounceOffsetX, transform.position.y ,0);
       
        if (transform.position.y > initialPosition.y + MaxHeight)
        {
            GameObject.Destroy(bubble);
        }
    }
    public void Update()
    {
        BubbleEffect();     
        
    }
}
