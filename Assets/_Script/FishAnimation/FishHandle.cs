using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
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
    public int scalePoint;
    public int uniqueID;
    public int ID;
    public Sprite fishSprite;
    public AudioManager audioManager;
    public AudioClip fishEat;
    public AudioClip fishGround;
    
    public virtual void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        ScaleFish();
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
        if (dataX == null)
        {
            Debug.LogError("FishData is null!"); 
            return;
        }
        Speed = dataX.speed;
        scalePoint = dataX.scalePoint;
        ID = dataX.id;
        anim.runtimeAnimatorController = dataX.controller;
        fishSprite = dataX.fishSprite;
        uniqueID = GetInstanceID();
        
    }

    public virtual void Flip(Vector3 direction)
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
       StartCoroutine(StartTurn(movement));

        if (Vector2.Distance(currentPosition, targetPosition) > 0.2f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    public virtual IEnumerator StartTurn(Vector3 currentDirection)
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

    public virtual void Eat()
    {
        audioManager.PlaySoundEffect(fishEat);
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
                    this.scalePoint += otherFishHandle.scalePoint;
                    Eat();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    public virtual void ScaleFish()
    {
        if (this.scalePoint == 15)
        {
            audioManager.PlaySoundEffect(fishGround);
            Debug.Log("Scaling Fish!");
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            Debug.Log("New scale: " + transform.localScale);
        }
        else if (this.scalePoint == 30)
        {
            audioManager.PlaySoundEffect(fishGround);
            Debug.Log("Scaling Fish!");
            transform.localScale = new Vector3(2.0f, 2.0f, 1);
        }
        else if (this.scalePoint == 50)
        {
            audioManager.PlaySoundEffect(fishGround);
            Debug.Log("Scaling Fish!");
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }
        else if (this.scalePoint == 70)
        {
            audioManager.PlaySoundEffect(fishGround);
            Debug.Log("Scaling Fish!");
            transform.localScale = new Vector3(3.0f, 3.0f, 1);
        }
    }
    public virtual void Update()
    {    
        Move();
    }
}
