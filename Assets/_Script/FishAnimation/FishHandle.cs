﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FishHandle : MonoBehaviour
{
    public FishData fishData;
    public Animator anim;
    public Rigidbody2D rb;
    public Transform targetPoint;
    public GameObject EatPoint;
    protected Vector2 originalScale;
    protected Vector2 movement;
    public float Speed;
    public float scalePoint;
    public int uniqueID;
    public int ID;
    public Sprite fishSprite;
    
   
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
    }
    public virtual void SetData(FishData dataX)
    {
        this.fishData = dataX;
        UpdateData(dataX);
    }
    public virtual void UpdateData(FishData dataX)
    {
        Speed = dataX.speed;
        scalePoint = dataX.scalePoint;
        ID = dataX .id;
        anim.runtimeAnimatorController = dataX.controller;
        fishSprite = dataX.fishSprite;
        uniqueID = GetInstanceID();
        
    }
  
    public virtual void Flip(Vector2 direction)
    {

        if (direction.x < 0) // Di chuyển sang trái
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Xoay 180 độ quanh trục Y
        }
        else if (direction.x > 0) // Di chuyển sang phải
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Xoay về góc 0 độ
        }
    }
    public virtual void Move()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = targetPoint.position;
        movement = (targetPosition - currentPosition).normalized;           
        rb.MovePosition(Vector2.Lerp(currentPosition, targetPosition, Speed * Time.deltaTime));       
        Flip(movement);

        if(Vector2.Distance(currentPosition,targetPosition)> 0.2f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
   
    public virtual void Eat()
    {
        anim.SetTrigger("Eat");
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            FishData otherFishHandle = collision.gameObject.GetComponent<FishData>();
            if (otherFishHandle != null)
            {               
                if (this.scalePoint > otherFishHandle.scalePoint)
                {                  
                    this.scalePoint+= otherFishHandle.scalePoint ;                   
                    Eat();
                    Destroy(collision.gameObject);                   
                }
            }
        }
    }
    public virtual void ScaleFish()
    {
        if (scalePoint >= 15)
        {
            Debug.Log("Scaling Fish!"); 
            transform.localScale = new Vector3(1.5f,1.5f, 1);
            Debug.Log("New scale: " + transform.localScale);
        }
        else if(scalePoint >= 30)
        {
            Debug.Log("Scaling Fish!");
            transform.localScale = new Vector3(2.0f, 2.0f, 1);
        }
        else if(scalePoint >= 50)
        {

            Debug.Log("Scaling Fish!");
            transform.localScale = new Vector3(3.0f, 3.0f, 1);
        }
    }
    public virtual void Update()
    {
       
        Move();

    }
}
