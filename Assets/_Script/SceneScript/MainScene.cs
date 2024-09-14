using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public SceneManagers sceneManagers;
    public Button buttonShop;
    public Button buttonFishTank;
    public QuestDataBaseTest questData;
   
    public void Start()
    {
       
    }
    public void OnButtonShopClick()
    {
        sceneManagers.LoadShopScene();
    }
    public void OnButtonFishTankClick()
    {
        sceneManagers.LoadFishTankScene();
    }

    
}
