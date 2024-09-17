using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public FishData fishData;
    public GameObject fishDisplay;
    public Transform RootFishMenu;

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
        fishImage.sprite = fishData.fishSprite;
    }
    
    public void SetMenuData(float scalepoint, int score, int live,int Ability)
    {
        scalePoint.text = $"{scalepoint}";
        scores.text = $"{score}";
        lives.text = $"{live}";
        
    }
    public void SetAbilityScore()
    {

    }
    public void SetScalePoint()
    {

    }
}
