using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class RewardBase
{
    public int rewardID;
    public Sprite rewardIMG;
    public int rewardQuality;
    public string rewardName;
}
[Serializable]
public class RewardDataBase
{
    public List<RewardBase> rewardBases;
}
