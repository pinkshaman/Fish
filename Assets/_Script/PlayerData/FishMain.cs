using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMain : FishHandle
{
    public UIMainFishControl control;
    public int lives = 3;

    public override void Start()
    {
        base.Start();
        control.SetData(this);
    }
    public override void UpdateData(FishData dataX)
    {
        base.UpdateData(dataX);
        
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // Lấy đối tượng FishHandle từ đối tượng va chạm
            FishOtherHandle otherFishHandle = collision.gameObject.GetComponent<FishOtherHandle>();
            if (this.scalePoint > otherFishHandle.scalePoint)
            {
                this.scalePoint++;
                this.fishPoint += otherFishHandle.fishPoints;
                Eat();
                Destroy(collision.gameObject);
                ScaleFish();   
                control.SetData(this);
            }
        }
    }
    public override void Update()
    {
        base.Update();

    }







}
