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
    public Button skill1;
    public Button skill2;
    private bool isSkillActivate;
    
    public void Start()
    {
        skill1.onClick.AddListener(Skill1Activate);
        skill2.onClick.AddListener(Skill2Activate);
        StartCoroutine(SetAbilityScore());
    }
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

        SetScalePoint();
    }
    public void SetScalePoint()
    {
        float FillAmout = float.Parse(scalePoint.text) / 100;
        ScaleFillBar.fillAmount = FillAmout;
    }
    IEnumerator SetAbilityScore()
    {
        float scoreCount = 0;

        while (true)
        {
            if (!isSkillActivate)
            {
                skill1.interactable = false;
                scoreCount += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(scoreCount / 10);
                AbilityFillBar.fillAmount = fillAmount;
                if (fillAmount >= 1f)
                {
                    skill1.interactable = true;
                    yield return new WaitUntil(() => isSkillActivate);
                }
            }
            if (isSkillActivate)
            {
                ResetCount();
                scoreCount = 0;
            }
            yield return null;
        }
    }
    public void ResetCount()
    {
        isSkillActivate = false;
        AbilityFillBar.fillAmount = 0;       
    }
    public void Skill1Activate()
    {
        isSkillActivate = true;
    }
    public void Skill2Activate()
    {

    }
    public void ShowReSult()
    {
        

    }




}
