using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishShowUiPanel : MonoBehaviour
{
    public FishData fishData;
    public Image Image;
    public Toggle showFishButton;
    public Text fishNames;
    public GameObject panelShowFish;
    public Text fishSpeed;
    public Text fishScale;
    public Text ID;
    public Text Price;
    // Start is called before the first frame update
    public virtual void Start()
    {
        showFishButton.onValueChanged.AddListener(ShowFish);
    }
    public virtual void ShowFish(bool isOn)
    {
        panelShowFish.SetActive(isOn);      
    }
    public virtual void SetData(FishData chooseFishX, Toggle toggle)
    {
        this.fishData = chooseFishX;
        this.showFishButton = toggle;
        Debug.Log($"SetData:{chooseFishX.fishName},{toggle.name} , {chooseFishX.id}, {chooseFishX.speed},{chooseFishX.scalePoint}, {chooseFishX.controller}");

        UpdateUi(chooseFishX);
    }

    public virtual void UpdateUi(FishData chooseFishX)
    {
        ID.text = chooseFishX.id.ToString();
        Image.sprite = chooseFishX.fishSprite;
        fishNames.text = chooseFishX.fishName;
        fishSpeed.text = chooseFishX.speed.ToString();
        fishScale.text = chooseFishX.scalePoint.ToString();
        //animator.runtimeAnimatorController = chooseFishX.chooseFishcontroller;
        Price.text = chooseFishX.Price.ToString();
    }
}
