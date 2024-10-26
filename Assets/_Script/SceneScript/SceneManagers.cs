using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour
{    
    public int level;  
    public void Start()
    {
      
    }
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
        PlayerPrefs.SetInt("QuestID", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene("PlayScene");
    }
    public void LoadDingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }
    public void LoadStoryScene(int level)
    {
        PlayerPrefs.SetInt("QuestID", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene("StoryScene");

    }
}
