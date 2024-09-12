using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour
{
    public void LoadChooseFish()
    {
        SceneManager.LoadScene("ChooseFish");
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadFishTankScene()
    {
        SceneManager.LoadScene("FishTank");
    }
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void LoadDingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
