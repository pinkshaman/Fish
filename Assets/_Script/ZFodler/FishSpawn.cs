using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : FishHandle
{
    public Vector2 moveAreaMin; 
    public Vector2 moveAreaMax; 
    private Vector2 randomTagetPosition;
    public SwimPathAgent swimPathAgent;
    public int fishPoints;
    public override void UpdateData(FishData dataX)
    {
        base.UpdateData(dataX);
        this.fishPoints = dataX.fishPoint;
        
    }
   
   
    public void SetDataAndSwimPath(FishData fishData, SwimPath swimPath)
    {
        this.swimPathAgent.swimPath = swimPath;
        this.fishData = fishData;
        UpdateFishWave(fishData, swimPathAgent);
    }
    public void UpdateFishWave(FishData dataX, SwimPathAgent swimPathAgent)
    {
        swimPathAgent.Speed = dataX.speed;
        Speed = dataX.speed;
        scalePoint = dataX.scalePoint;
        ID = dataX.id;
        anim.runtimeAnimatorController = dataX.controller;
        fishSprite = dataX.fishSprite;
        uniqueID = GetInstanceID();
        this.fishPoints = dataX.fishPoint;
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

    private void SetRandomTagetPosition()
    {
        float randomX = Random.Range(moveAreaMin.x, moveAreaMax.x);
        float randomY = Random.Range(moveAreaMin.y, moveAreaMax.y);
        randomTagetPosition = new Vector2(randomX, randomY);
    }
    public override void Move()
    {
        swimPathAgent.SwimToNextPoint();

    }

}
