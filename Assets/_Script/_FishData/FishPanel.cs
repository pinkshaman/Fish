using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FishPanel : MonoBehaviour
{  
    public FishData fishData;
    public Image Image;
    public Toggle showFishButton;
    public Text fishNames;
    //public Text fishDescription;
    public GameObject panelShowFish;
    public Text fishSpeed;
    public Text fishScale;
    public Text ID;
    //private Animator animator;
    
    public void Start()
    {
        showFishButton.onValueChanged.AddListener(ShowFish);      
    }
    public void ShowFish(bool isOn)
    {
        panelShowFish.SetActive(isOn);
        Debug.Log(isOn ? "Show Fish Panel" : "Hide Fish Panel");

    }
    public void SetData(FishData chooseFishX, Toggle toggle)
    {
        this.fishData = chooseFishX;
        this.showFishButton = toggle;
        Debug.Log($"SetData:{chooseFishX.fishName}");
        
        UpdateUi(chooseFishX);
    }

    public void UpdateUi(FishData chooseFishX)
    {
        ID.text = chooseFishX.id.ToString();
        Image.sprite = chooseFishX.fishSprite;
        fishNames.text = chooseFishX.fishName;
        fishSpeed.text = chooseFishX.speed.ToString();
        fishScale.text = chooseFishX.scalePoint.ToString();
        //animator.runtimeAnimatorController = chooseFishX.chooseFishcontroller;
    }
}
