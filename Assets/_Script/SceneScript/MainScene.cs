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
        AudioManager audio = FindFirstObjectByType<AudioManager>();
        if (audio != null)
        {
            audio.ChangeBackGroundMusic(1);
        }
        else
        {
            Debug.LogError("AudioManager not found  ");
        }

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
        AudioManager audio = FindAnyObjectByType<AudioManager>();
        audio.OnButtonClickAudio();
    }


}
