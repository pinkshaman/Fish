﻿using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishOtherHandle : FishHandle
{
    public Vector2 moveAreaMin; // Giới hạn dưới của vùng di chuyển
    public Vector2 moveAreaMax; // Giới hạn trên của vùng di chuyển
    private Vector2 randomTagetPosition;// Vị trí mục tiêu ngẫu nhiên
    public FishData fishOtherData;
    public int fishPoints;

    public override void Start()
    {
        base.Start();
        SetRandomTagetPosition();

    }
    public override void SetData(FishData data)
    {
        this.fishOtherData = data;
        UpdateData(data);
    }
    public override void UpdateData(FishData dataX)
    {
        if (dataX == null)
        {
            Debug.LogWarning("fishData is null");
        }
        Speed = dataX.speed;
        scalePoint = dataX.scalePoint;
        ID = dataX.id;
        anim.runtimeAnimatorController = dataX.controller;
        fishSprite = dataX.fishSprite;
        uniqueID = GetInstanceID();
        this.fishPoints = dataX.fishPoint;
    }

    public void Move1()
    {
        
       
    }
    public void Move2()
    {
        Vector2 currentPosition = transform.position;
        movement = (randomTagetPosition - currentPosition).normalized;
        rb.MovePosition(Vector2.MoveTowards(currentPosition, randomTagetPosition, Speed * Time.deltaTime));
        Flip(movement);

        if (Vector2.Distance(currentPosition, randomTagetPosition) < 0.1f)
        {
            SetRandomTagetPosition();
        }
    }
    public int RandomMove()
    {
        int RandomMove = Random.Range(1, 2);
        return RandomMove;
    }

    public override void Move()
    {
        Move2();
    }
    private void SetRandomTagetPosition()
    {
        float randomX = Random.Range(moveAreaMin.x, moveAreaMax.x);
        float randomY = Random.Range(moveAreaMin.y, moveAreaMax.y);
        randomTagetPosition = new Vector2(randomX, randomY);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FishMain"))
        {
            FishMain fishMain = collision.gameObject.GetComponent<FishMain>();
            if (this.scalePoint > fishMain.scalePoint)
            {
                this.scalePoint += fishMain.scalePoint;
                Eat();
                fishMain.gameObject.SetActive(false);
                ScaleFish();
            }
            else
            {
                SetRandomTagetPosition();
            }
        }
    }
    public override void ScaleFish()
    {
        if (this.scalePoint == 15)
        {                
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        else if (this.scalePoint == 30)
        {
           
            transform.localScale = new Vector3(2.0f, 2.0f, 1);
        }
        else if (this.scalePoint == 50)
        {          
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }
    }
    public override void Update()
    {
        base.Update();
    }
}
