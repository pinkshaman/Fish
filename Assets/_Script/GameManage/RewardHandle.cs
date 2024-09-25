using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RewardHandle : MonoBehaviour
{
    public RewardBase rewardBase;  
    public Image rewarImage;
    public TMP_Text rewardQuality;
    public TMP_Text rewardName;

    public void SetDataReward(RewardBase reward)
    {
        this.rewardBase = reward;
        UpDateUi();
    }
    public void UpDateUi()
    {
        rewarImage.sprite = rewardBase.rewardIMG;
        rewardName.text = rewardBase.rewardName;
        rewardQuality.text = $"{rewardBase.rewardQuality}";
    }
   
  
}
