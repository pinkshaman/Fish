using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class RewardUpdateData
{
    public int rewardID;       
    public int rewardQuality;  
}
public class RewardManager : MonoBehaviour
{
    public RewardDataBase rewardDataBase;
    public QuestDataBase questDataBaseTest;
    public QuestManager questManager;
    public RewardHandle rewardHandle;
    public Transform rootRewardUi;
    public List<RewardUpdateData> rewardUpdateList;
    
   
    public void Start()
    {
        UpdateRewards();
    }

    public void UpdateRewards()
    {     
        foreach (var rewardData in rewardUpdateList)
        {           
            UpdateRewardQualityByID(rewardData.rewardID, rewardData.rewardQuality);
            questManager.UpdateRewardQuality(rewardData.rewardID, rewardData.rewardQuality);
        }      
    }
    public void UpdateRewardQualityByID(int rewardID, int newQuality)
    {
        RewardBase reward = rewardDataBase.rewardBases.Find(r => r.rewardID == rewardID);
        reward.rewardQuality = newQuality;
        CreateReward(reward);
    }

    public void CreateReward(RewardBase reward)
    {
        var newReward = Instantiate(rewardHandle, rootRewardUi);
        newReward.SetDataReward(reward);
    }
   

}


