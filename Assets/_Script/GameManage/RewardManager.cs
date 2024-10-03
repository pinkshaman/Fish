using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public QuestDataBase questDataBase;
    public RewardHandle rewardHandles;
    public Transform rootRewardUi;
    public RewardDataBase rewardDataBase;
    public List<RewardBase> rewardListClaim;
    public Button buttonClaim;
    public PlayerData playerData;
    public void Start()
    {
        Debug.Log("List reward Created");
        LoadPlayerData();       
        buttonClaim.onClick.AddListener(OnAccept);
    }
    public void CreateReward(List<RewardBaseUpdate> rewarListUpdate)
    {               
        foreach (var rewardUpdateID in rewarListUpdate)
        {
            RewardBase rewards = rewardDataBase.rewardBases.Find(reward => reward.rewardID == rewardUpdateID.rewardID);
            rewards.rewardQuality = rewardUpdateID.rewardQuality;
            var newReward = Instantiate(rewardHandles, rootRewardUi);
            newReward.SetDataReward(rewards);
            rewardListClaim.Add(rewards);
        }
    }
    public void OnAccept()
    {
        Debug.Log($"RewardListClaim {rewardListClaim.Count}");
        foreach (var rewards in rewardListClaim)
        {
            if (rewards.rewardID == 1)
            {
                playerData.whilePearl += rewards.rewardQuality;
                Debug.Log($"Added WhitePearl :{rewards.rewardName} - {rewards.rewardQuality}");
            }
            if (rewards.rewardID == 3)
            {
                playerData.playerExperience += rewards.rewardQuality;
                Debug.Log($"Added Exp : {rewards.rewardName} - {rewards.rewardQuality}");
            }
            SavePlayersData();
        }
    }
    [ContextMenu("LoadPlayerData")]
    public void LoadPlayerData()
    {
        var defaultValue = JsonUtility.ToJson(playerData);
        var json = PlayerPrefs.GetString(nameof(playerData), defaultValue);
        Debug.Log($"Loaded JSON: {json}");
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log($"PlayerData Loaded - FishMainID: {playerData.fishMainID}");
    }
    [ContextMenu("SavePlayersData")]
    public void SavePlayersData()
    {
        var value = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(nameof(playerData), value);
        PlayerPrefs.Save();
    }



}


