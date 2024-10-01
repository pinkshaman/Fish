using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FishMain : FishHandle
{
    public UIScore control;
    public int lives = 3;
    public int score;
    public float dashSpeed=10;
    public override void Start()
    {
        base.Start();
        
        control.SetdataUI(this);
    }
    public override void SetData(FishData dataX)
    {
        this.fishData = dataX;      
        UpdateData(dataX);
    }
    public override void UpdateData(FishData dataX)
    {
        base.UpdateData(dataX);
        Speed = dataX.speed;
        scalePoint = 2;
        ID = dataX.id;
        anim.runtimeAnimatorController = dataX.controller;
        fishSprite = dataX.fishSprite;
        uniqueID = GetInstanceID();

    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // Lấy đối tượng FishHandle từ đối tượng va chạm
            FishOtherHandle otherFishHandle = collision.gameObject.GetComponent<FishOtherHandle>();
            if (this.scalePoint >= otherFishHandle.scalePoint)
            {
                this.scalePoint++;
                this.score += otherFishHandle.fishPoints;
                Eat();
                Destroy(collision.gameObject);
                ScaleFish();   
                control.SetdataUI(this);
            }
        }
    }
    public void Dash()
    {
        float dashDirection = transform.localScale.x;
        Vector3 dashForce = new Vector3(dashDirection * dashSpeed, 0, 0);        
        rb.MovePosition(dashForce);
    }
    public void OnDestroy()
    {
        if(lives>0)
        {
            FishManager.Instance.CreateFish(this.fishData);
            lives -= 1;            
        }
    }

    public override void Update()
    {
        base.Update();

    }

}
