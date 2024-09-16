using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishinTankHandle : FishHandle
{

    public Vector2 moveAreaMin; // Giới hạn dưới của vùng di chuyển
    public Vector2 moveAreaMax; // Giới hạn trên của vùng di chuyển
    private Vector2 randomTagetPosition;// Vị trí mục tiêu ngẫu nhiên


    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetRandomTagetPosition();
    }
   
    public override void Move()
    {
        Vector2 currentPosition = transform.position;

        movement = (randomTagetPosition - currentPosition).normalized;
        rb.MovePosition(Vector2.MoveTowards(currentPosition, randomTagetPosition, Speed * Time.deltaTime));

        Flip(movement);
        //Nếu đã đến gần vị trí mục tiêu, tạo vị trí mục tiêu mới
        if (Vector2.Distance(currentPosition, randomTagetPosition) < 0.3f)
        {
            SetRandomTagetPosition();
        }
    }
    private void SetRandomTagetPosition()
    {
        float randomX = Random.Range(moveAreaMin.x, moveAreaMax.x);
        float randomY = Random.Range(moveAreaMin.y, moveAreaMax.y);
        randomTagetPosition = new Vector2(randomX, randomY);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            SetRandomTagetPosition();
            Move();
        }
    }
}
