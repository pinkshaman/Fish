using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public FishData fishData;
    public GameObject fishDisplay;
    public Transform RootFishMenu;
    public FishMain fishMain;
    public Image AbilityBar;
    public Image AbilityFillBar;
    public Image ScaleBar;
    public Image ScaleFillBar;
    public Text scores;
    public Text scalePoint;
    public Text lives;
    public void CreateMenu(FishData fishDataX)
    {
        var fishMenu = Instantiate(fishDisplay, RootFishMenu);
        var fishImage = fishMenu.GetComponentInChildren<Image>();
        fishImage.sprite = fishDataX.fishSprite;
    }
    public void SetdataUI(FishMain fish)
    {
        this.fishMain = fish;
        SetMenuData(fish);
    }
   

    public void SetMenuData(FishMain fishMain)
    {
        scalePoint.text = $"{fishMain.scalePoint}";
        scores.text = $"{fishMain.score}";
        lives.text = $"{fishMain.lives}";
        SetAbilityScore();
        SetScalePoint();
    }
    public void SetAbilityScore()
    {
        float FillAmout =float.Parse(scalePoint.text)/ 100;
        ScaleFillBar.fillAmount = FillAmout;
    }
    public void SetScalePoint()
    {
        
    }
}
