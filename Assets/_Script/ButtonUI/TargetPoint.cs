using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    [SerializeField] private MoveButtonUi dr;
    private Vector2 movement;
    public float Speed;

    public void Start()
    {
        dr = FindObjectOfType<MoveButtonUi>();
    }
    public void Move(Vector2 movement)
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + Speed * Time.deltaTime * (Vector3)movement;
        transform.position = newPosition;

    }
    public void Update()
    {
        if (dr != null)
        {
            movement = dr.GetDirection();
            Move(movement);
        }
        else
        {
            Debug.LogError("MoveButtonUi instance not found!");
        }
    }
}
