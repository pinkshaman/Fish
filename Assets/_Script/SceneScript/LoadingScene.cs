using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public FishTankManager fishTankManager;
    public FishShowUi choosefishmanager;
    public void Start()
    {
        fishTankManager= FindObjectOfType<FishTankManager>();
        choosefishmanager = FindObjectOfType<FishShowUi>();

        // Kiểm tra nếu FishTankManager không tồn tại, tải scene chứa nó
        if (FindObjectOfType<FishTankManager>() == null)
        {
            Debug.Log("FishTankManager not found. Loading FishTank scene.");
            SceneManager.LoadScene("FishTank", LoadSceneMode.Additive);
            StartCoroutine(WaitForFishTankManager());
        }
    }

    private IEnumerator WaitForFishTankManager()
    {
        // Đợi đến khi FishTankManager xuất hiện
        while (FindObjectOfType<FishTankManager>() == null)
        {
            yield return null; // Đợi frame tiếp theo
        }

        fishTankManager = FindObjectOfType<FishTankManager>();
        Debug.Log("FishTankManager found and assigned.");
    }
}
