using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMain : MonoBehaviour
{
    public FishData fishData;
    public FishHandle fishHandle;
    public GameObject fishPrefabs;
    public Rigidbody rb;
    public Collider2D col;
    public Animator anim;

    public int ID;
    public int intanceID;
    public float Speed;
    public float scalePoint;
    public Sprite fishSpirte;
    public RuntimeAnimatorController controller;
    
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }
    public void SetDataFishMain(FishData dataX)
    {
        this.fishData = dataX;
    }
    public void UpdateFish()
    {
        ID = fishData.id;
        Speed = fishData.speed;

    }
}
