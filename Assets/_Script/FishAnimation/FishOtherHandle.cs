using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOtherHandle : FishHandle
{
    public Vector2 moveAreaMin; // Giới hạn dưới của vùng di chuyển
    public Vector2 moveAreaMax; // Giới hạn trên của vùng di chuyển
    private Vector2 randomTagetPosition;// Vị trí mục tiêu ngẫu nhiên
    public QuestDataTest questDataTest;

    public override void Start()
    {
        base.Start();
        SetRandomTagetPosition();
    }
    public void SetData(QuestDataTest data)
    {
        this.questDataTest = data;
        UpdateQuestData(data);
    }
    public override void SetData(FishData dataX)
    {
        
    }
    public void UpdateQuestData(QuestDataTest data)
    {
        Speed = data.speed; 
        ID= data.questID;
        scalePoint = data.scalePoint;
        fishSprite = data.fishSprite;
        anim.runtimeAnimatorController = data.controller;
        uniqueID = GetInstanceID();
        
    }
    public override void Move()
    {
        Vector2 currentPosition = transform.position;

        movement = (randomTagetPosition - currentPosition).normalized;
        rb.MovePosition(Vector2.MoveTowards(currentPosition, randomTagetPosition, Speed * Time.deltaTime));

        Flip(movement);
        //Nếu đã đến gần vị trí mục tiêu, tạo vị trí mục tiêu mới
        if (Vector2.Distance(currentPosition, randomTagetPosition) < 0.1f)
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
    public override void Update()
    {
        base.Update();
    }
}
