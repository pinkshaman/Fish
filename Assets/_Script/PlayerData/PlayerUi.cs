using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

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
       // Gán dữ liệu data cho playerData.
        this.playerData = data;
        UpdateUIplayerData(data);
    }
    public void UpdateUIplayerData(PlayerData data)
    {
        //Cập nhật các thành phần giao diện
        playerName.text = data.name;
        playerLevel.text = data.level.ToString();
        whilePearl.text = data.whilePearl.ToString();
        blackPearl.text = data.blackPearl.ToString();
        LevelFill(data);
    }
    public void LevelFill(PlayerData data)
    {
        float ExpMax = 100f * Mathf.Pow(1.2f, data.level);
        float currenPercent = data.playerExperience / ExpMax;
        levelFill.fillAmount = currenPercent;
        playerExp.text = $"{Mathf.Floor(currenPercent * 1000) / 10:F1}%";
        if (currenPercent >=1f)
        {
            float ExpOverflow = data.playerExperience - ExpMax;
            data.level += 1;
            ResetFill();
            data.playerExperience = ExpOverflow;
            ExpMax *= 1.2f;
            currenPercent = data.playerExperience / ExpMax;
            levelFill.fillAmount = currenPercent;
            playerExp.text = $"{Mathf.Floor(currenPercent * 1000) / 10:F1}%";
        }
    }
    public void ResetFill()
    {
        levelFill.fillAmount = 0;
    }
}
