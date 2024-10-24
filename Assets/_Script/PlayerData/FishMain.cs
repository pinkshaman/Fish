using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FishMain : FishHandle
{
    public UIScore control;
    public int lives = 3;
    public int score;
    public float dashSpeed = 10;
    public bool isDead;
    public GameObject effectEat;
    public Move move;
    private Vector3 previousDirection; 

    public override void Start()
    {
        control = FindAnyObjectByType<UIScore>();
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
            if (collision.gameObject.GetComponent<FishOtherHandle>())
            {
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
            else if(collision.gameObject.GetComponent<FishSpawn>())
            {
                var fishWave = collision.GetComponent<FishSpawn>();
                if (this.scalePoint>= fishWave.scalePoint)
                {

                    this.scalePoint++;
                    this.score += fishWave.fishPoints;
                    Eat();
                    Destroy(collision.gameObject);
                    ScaleFish();
                    control.SetdataUI(this);
                }
            }

        }
    }
    public override void Eat()
    {
        base.Eat();
        effectEat.SetActive(true);
        StartCoroutine(HandleEffectEat());
    }
    private IEnumerator HandleEffectEat()
    {        
        effectEat.SetActive(true);       
        AnimatorStateInfo eatStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        while (!eatStateInfo.IsName("Eat"))
        {
            yield return null; 
            eatStateInfo = anim.GetCurrentAnimatorStateInfo(0); 
        }     
        yield return new WaitForSeconds(eatStateInfo.length/2);     
        effectEat.SetActive(false);
    }
    public void Dash()
    {
        float dashDirection = transform.localScale.x;
        Vector3 dashForce = new Vector3(dashDirection * dashSpeed, 0, 0);
        rb.MovePosition(dashForce);
    }
    public void OnDisable()
    {
        isDead = true;
        lives -= 1;
        control.SetdataUI(this);

    }
    public override void Move()
    {
        Vector3 currentDirection = move.GetCurrentDirection();
        if (currentDirection.magnitude > 0.2f)
        {           
            anim.SetBool("isMoving", true);
            if (Mathf.Sign(currentDirection.x) != Mathf.Sign(previousDirection.x))
            {
                StartCoroutine(StartTurn(currentDirection));
            }
        }
        else
        {       
            anim.SetBool("isMoving", false);
        }

        previousDirection = currentDirection;
    }
    public override IEnumerator StartTurn(Vector3 currentDirection)
    {      
        anim.SetTrigger("Turn");
        AnimatorStateInfo turnStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        while (!turnStateInfo.IsName("Turn"))
        {
            yield return null;
            turnStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        }
        yield return new WaitForSeconds(turnStateInfo.length);
        Flip(currentDirection);
    }

    public override void Update()
    {
        base.Update();

    }


}
