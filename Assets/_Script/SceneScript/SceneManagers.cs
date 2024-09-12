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
    public void LoadPlayScene(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void LoadDingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
