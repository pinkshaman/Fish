using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public FloatUI floatUI;
   
    public float Speed;
    public Vector3 direction;

    public void Start()
    {
        floatUI = FindObjectOfType<FloatUI>();
        direction = floatUI.GetDirection();
    }
    public void Moving()
    {
        transform.Translate(Speed * Time.deltaTime * direction);
    }

    public void Update()
    {
        Moving();
    }

}
