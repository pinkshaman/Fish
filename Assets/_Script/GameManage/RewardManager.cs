using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public QuestDataBase questDataBase;
    public QuestManager questManager;
    public RewardHandle rewardHandle;
    public Transform rootRewardUi;
    
    
   
    public void Start()
    {
        
    }

    public void UpdateRewards(int questID)
    {
        QuestData selectedQuest = GetQuestDataByID(questID);
        foreach (var reward in selectedQuest.rewardList )
        {
            if (reward == null)
            {
                Debug.LogError("Reward is null.");
                continue; // Bỏ qua reward null
            }

            CreateReward(reward);
            Debug.Log($"RewardCreated:{reward.rewardName}- {reward.rewardQuality} ");
        }      
    }
    public QuestData GetQuestDataByID(int questID)
    {
        foreach (var questData in questDataBase.questDataBases)
        {
            if (questData.questID == questID)
            {
                return questData;
            }
        }
        Debug.LogError("Quest not found!");
        return null;
    }

    public void CreateReward(RewardBase reward)
    {
        var newReward = Instantiate(rewardHandle, rootRewardUi);
        newReward.SetDataReward(reward);
    }

   

}


