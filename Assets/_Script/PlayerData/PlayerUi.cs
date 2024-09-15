using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Tính toán phần trăm kinh nghiệm hiện tại 
        float currenPercent = data.playerExperience / 100;
        levelFill.fillAmount = currenPercent;
        playerExp.text = $"{currenPercent}";
    }
}
