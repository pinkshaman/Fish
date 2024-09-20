using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public RewardDataBase rewardBase;
    public QuestDataBaseTest questDataBaseTest;
    public TestGameController controller;
    public RewardHandle rewardHandle;
    public Transform rootRewardUi;

    public Dictionary<int, RewardDataBase> rewardData;

    public void Awake()
    {
        rewardData = new Dictionary<int, RewardDataBase>();
    }
    public void Start()
    {
        AddReward();
    }
    public void AddReward()
    {
        if (rewardData == null)
        {
            Debug.LogWarning("rewardData is null. Initializing now...");
            rewardData = new Dictionary<int, RewardDataBase>();
        }
        Debug.Log(rewardData != null ? "rewardData exists" : "rewardData is null");
        rewardData.Add(1, new RewardDataBase
        {
            rewardBases = new List<RewardBase>
            {
                new() { rewardID = 1, rewardName = "While Pearl", rewardQuality = 200},
                new() { rewardID = 3, rewardName = "Exp", rewardQuality = 1000 }

            }
        });

    }
    public void GetRewardByQuestID(int key)
    {

        if (rewardData == null)
        {
            Debug.LogError("rewardData is null");
            return;
        }
        if (rewardData.ContainsKey(key))
        {
            RewardDataBase selectedRewardBase = rewardData[key];

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


