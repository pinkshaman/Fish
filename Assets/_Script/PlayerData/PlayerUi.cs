﻿
using UnityEngine;
using UnityEngine.UI;


public class PlayerUi : MonoBehaviour
{
    public Text playerName;
    public Text playerExp;
    public Text playerLevel;
    public Text whilePearl;
    public Text blackPearl;
    public Image progessLevelBG;
    public Image levelFill;
    public PlayerData playerData;
    
    public void SetDataPlayer(PlayerData data)
    {
        
        this.playerData = data;
        UpdateUIplayerData(data);
    }
    public void UpdateUIplayerData(PlayerData data)
    {
        
        playerName.text = data.name;
        playerLevel.text = data.level.ToString();
        whilePearl.text = data.whitePearl.ToString();
        blackPearl.text = data.blackPearl.ToString();
        LevelFill(data);
    }
    public void LevelFill(PlayerData data)
    {
        float ExpMax = 100f * Mathf.Pow(1.2f, data.level);
        float currenPercent = data.playerExperience / ExpMax;
        while (currenPercent >= 1f)
        {          
            float ExpOverflow = data.playerExperience - ExpMax;            
            data.level += 1;            
            ResetFill(data);          
            data.playerExperience = ExpOverflow;          
            ExpMax = 100f * Mathf.Pow(1.2f, data.level);          
            currenPercent = data.playerExperience / ExpMax;
        }
        levelFill.fillAmount = currenPercent;
        playerExp.text = $"{(currenPercent * 100f):F1}%";
    }
    public void ResetFill(PlayerData playerData)
    {
        levelFill.fillAmount = 0;
        playerData.playerExperience = 0;
    }
   
}
