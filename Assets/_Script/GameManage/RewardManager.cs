using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public RewardBase rewardData;
    public RewardDataBase rewardDataBase;
    public QuestDataBase questDataBaseTest;
    public QuestManager controller;
    public RewardHandle rewardHandle;
    public Transform rootRewardUi;

    public Dictionary<int, RewardDataBase> rewards;

    public void Awake()
    {
        rewards = new Dictionary<int, RewardDataBase>();
    }
    public void Start()
    {

    }
    public void AddReward(int questID)
    {
        if (questID == 1)
        {
            Debug.Log(rewards != null ? "rewardData exists" : "rewardData is null");
            rewards = new Dictionary<int, RewardDataBase>();
        
            var newRewards = new RewardDataBase();                             
            var reward = new RewardBase(1, 200, "WhitePearl", null );
            newRewards.rewardBases = new List<RewardBase>();
            newRewards.rewardBases.Add(reward);
           
            
            rewards.Add(questID, newRewards);
        }
    }
    //public void AddRewardByID(RewardBase rewadData)
    //{
    //    var newReward = rewardDataBase.rewardBases.Find(reward => rewardID == reward.rewardID);

    //}
    public void GetRewardByQuestID(int key)
    {
        AddReward(key);

        if (rewards == null)
        {
            Debug.LogError("rewardData is null");
            return;
        }
        if (rewards.ContainsKey(key))
        {
            RewardDataBase selectedRewardBase = rewards[key];

            foreach (var rewards in selectedRewardBase.rewardBases)
            {
                CreateReward(rewards);
            }

        }
    }
    public void CreateReward(RewardBase reward)
    {
        var newReward = Instantiate(rewardHandle, rootRewardUi);
        newReward.SetDataReward(reward);
    }
   

}


