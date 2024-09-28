using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public QuestDataBase questDataBase;
    public RewardHandle rewardHandles;
    public Transform rootRewardUi;
    
    
   
    public void Start()
    {
        
    }

    public QuestHandle GetQuestDataByID(int questID)
    {
        QuestManager questManager = QuestManager.Instance;
        Dictionary<int, QuestHandle> questDictionary = questManager.GetQuests();
        foreach (var key in questDictionary)
        {
            if (key.Key == questID)
            {
                return key.Value;
            }
        }
        Debug.LogError("Dictionary not found!");
        return null;
    }
  

    public void CreateReward(int QuestID)
    {
        QuestHandle questHandle = GetQuestDataByID(QuestID);
        rewardHandles = questHandle.rewardHandle;
        var reward = rewardHandles.rewardBase;

        var newReward = Instantiate(rewardHandles, rootRewardUi);
        newReward.SetDataReward(reward);
    }

   

}


