using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
    public void CreateReward(RewardBase reward)
    {
        var newReward = Instantiate(rewardHandles, rootRewardUi);
        newReward.SetDataReward(reward);

    }
    public void SetDataListReward(List<RewardBaseUpdate> rewarListUpdate)
    {
        foreach (var rewardUpdateID in rewarListUpdate)
        {
            RewardBase rewards = rewardDataBase.rewardBases.Find(reward => reward.rewardID == rewardUpdateID.rewardID);
            rewards.rewardQuality = rewardUpdateID.rewardQuality;
            rewardListClaim.Add(rewards);
            StartCoroutine(CalculatorReward(rewards));
        }
    }
    public IEnumerator CalculatorReward(RewardBase reward)
    {
        UIMainFishControl uIMainFish = FindObjectOfType<UIMainFishControl>();
        bool isEnd = uIMainFish.isGameEnd;

        yield return new WaitUntil(() => isEnd);
        UIScore uiScore = FindObjectOfType<UIScore>();
        int score = uiScore.ReturnScore();
        int live = uIMainFish.fishMain.lives;
        if (live <= 0)
        {
            reward.rewardQuality = 0;
            CreateReward(reward);
        }
        else
        {
            if (score > 0 && score <= 300)
            {
                reward.rewardQuality += Mathf.RoundToInt(score * 0.1f);
            }
            else if (score > 300 && score <= 600)
            {
                reward.rewardQuality += Mathf.RoundToInt(score * 0.2f);
            }
            else if (score > 600 && score <= 1000)
            {
                reward.rewardQuality += Mathf.RoundToInt(score * 0.3f);
            }
            else if (score > 1000)
            {
                reward.rewardQuality += Mathf.RoundToInt(score * 0.5f);
            }
            CreateReward(reward);
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


