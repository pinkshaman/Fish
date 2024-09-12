using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

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
        // Lấy vị trí hiện tại của Fish và TargetPoint
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = targetPoint.position;

        // Tính hướng di chuyển và cập nhật hướng sprite
        movement = (targetPosition - currentPosition).normalized;
        // Di chuyển Fish bằng Lerp        
        rb.MovePosition(Vector2.Lerp(currentPosition, targetPosition, Speed * Time.deltaTime));


        // Đổi hướng sprite dựa trên hướng di chuyển
        Flip(movement);
    }
    public virtual void CheckAnim()
    {
        // Chơi animation Move nếu đang di chuyển
        bool isMoving = !movement.Equals(Vector2.zero);
        anim.SetBool("isMoving", isMoving);
    }
    public virtual void Eat()
    {
        anim.SetTrigger("Eat");
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // Lấy đối tượng FishHandle từ đối tượng va chạm
            FishHandle otherFishHandle = collision.gameObject.GetComponent<FishHandle>();
            // Tìm FishHandle tương ứng từ danh sách allFishes
            FishHandle otherFishInList = FishManager.Instance.allFishes.Find(otherFishInList => otherFishInList.uniqueID == otherFishHandle.uniqueID);

            if (otherFishInList != null)
            {
                // So sánh scalePoint của cá hiện tại và cá va chạm
                if (this.scalePoint > otherFishInList.scalePoint)
                {
                    // Tăng scalePoint của cá lớn hơn
                    this.scalePoint += otherFishInList.scalePoint;
                    // Gọi Eat animation và phá hủy cá nhỏ hơn
                    Eat();
                    Destroy(collision.gameObject);
                    ScaleFish();
                }
            }
        }
    }
    public virtual void ScaleFish()
    {
        if (scalePoint >= 10)
        {
            Debug.Log("Scaling Fish!"); // Thông báo trong Console
            transform.localScale = new Vector3(2, 2, 1);
            Debug.Log("New scale: " + transform.localScale);
        }
    }
    public virtual void Update()
    {
        CheckAnim();
        Move();

    }
}
