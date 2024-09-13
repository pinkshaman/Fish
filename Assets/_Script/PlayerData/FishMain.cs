using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMain : MonoBehaviour
{
    public FishData fishData;
    public FishHandle fishHandle;
    public GameObject fishPrefabs;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);

    }
    public void SetDataFishMain(FishData dataX)
    {
        this.fishData = dataX;
        FishManager.Instance.CreateFish(fishData);
    }

}
