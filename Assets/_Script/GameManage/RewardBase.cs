using System;
using System.Collections;
using UnityEngine;


[Serializable]
public class RewardBase
{
    public int rewardID;
    public Sprite rewardIMG;
    public int rewardQuality;
    public string rewardName;

    public RewardBase(int rewardID, int rewardQuality, string rewardName, Sprite rewardIMG)
    {
        this.rewardID = rewardID;
        this.rewardQuality = rewardQuality;
        this.rewardName = rewardName;   
        this.rewardIMG = rewardIMG;
    }
}
