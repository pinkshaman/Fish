using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public RewardDataBase rewardBase;
    public QuestDataBaseTest questDataBaseTest;
    public TestGameController controller;
    public RewardHandle rewardHandle;
    public Transform rootRewardUi;
    public void Start()
    {
        
    }
    public void GetRewardByQuestID(int questID)
    {
       
        var RewardbyID = rewardBase.rewardBases.Find(reward => reward.rewardID == questID);
        CreateReward(RewardbyID);
    }
    public void CreateReward(RewardBase reward)
    {
        var newReward = Instantiate(rewardHandle, rootRewardUi);
        newReward.SetDataReward(reward);
    }



}