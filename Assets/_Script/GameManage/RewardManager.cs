using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public QuestDataBase questDataBase;
    public RewardHandle rewardHandles;
    public Transform rootRewardUi;
    public RewardDataBase rewardDataBase; 
    public void Start()
    {
        
    }
    public void CreateReward(List<RewardBaseUpdate> rewarListUpdate)
    {               
        foreach (var rewardUpdateID in rewarListUpdate)
        {
            RewardBase rewards = rewardDataBase.rewardBases.Find(reward => reward.rewardID == rewardUpdateID.rewardID);
            rewards.rewardQuality = rewardUpdateID.rewardQuality;
            var newReward = Instantiate(rewardHandles, rootRewardUi);
            newReward.SetDataReward(rewards);
        }
    }
}


