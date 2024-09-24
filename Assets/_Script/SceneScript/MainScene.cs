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
    public QuestDataBase questData;
    public Toggle QuestToggle;
    public GameObject questPanelQuest;
    public void Start()
    {
        QuestToggle.onValueChanged.AddListener(QuestCheck);
    }
    public void OnButtonShopClick()
    {
        sceneManagers.LoadShopScene();
    }
    public void OnButtonFishTankClick()
    {
        sceneManagers.LoadFishTankScene();
    }
    public void QuestCheck(bool isOn)
    {
        questPanelQuest.SetActive(isOn);
    }


}
