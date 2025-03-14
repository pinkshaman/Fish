﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public FishTankManager fishTankManager;
    public OptionShow choosefishmanager;
    public void Start()
    {
        fishTankManager= FindAnyObjectByType<FishTankManager>();
        choosefishmanager = FindAnyObjectByType<OptionShow>();

        // Kiểm tra nếu FishTankManager không tồn tại, tải scene chứa nó
        if (FindAnyObjectByType<FishTankManager>() == null)
        {
            Debug.Log("FishTankManager not found. Loading FishTank scene.");
            SceneManager.LoadScene("FishTank", LoadSceneMode.Additive);
            StartCoroutine(WaitForFishTankManager());
        }
    }

    private IEnumerator WaitForFishTankManager()
    {
        // Đợi đến khi FishTankManager xuất hiện
        while (FindAnyObjectByType<FishTankManager>() == null)
        {
            yield return null; // Đợi frame tiếp theo
        }

        fishTankManager = FindAnyObjectByType<FishTankManager>();
        Debug.Log("FishTankManager found and assigned.");
    }
}
