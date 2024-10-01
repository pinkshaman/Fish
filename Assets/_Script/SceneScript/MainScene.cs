using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.ChangeBackGroundMusic(1);

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
    public void OnButtonClickEffect()
    {
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.OnButtonClickAudio();
    }


}
