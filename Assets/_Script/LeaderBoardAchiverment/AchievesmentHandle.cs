using GooglePlayGames.BasicApi;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class AchievesmentHandle : MonoBehaviour
{
    public Achievesment achievement;
    public AchivesmentProgess achivesmentProgess;
    public TMP_Text achivesmentName;
    public TMP_Text achievesmentDes;
    public RewardDataBase rewardDataBase;
    public Button claimRewward;
    public Image progressBar;
    public Image fillProgess;
    public Image rewardIMG;
    public TMP_Text rewardQuality;
    public Image claimedIMG;
    public PlayerManager playerManager;
    public void Start()
    {
        playerManager = PlayerManager.Instance;
        claimRewward.onClick.AddListener(OnClaim);
    }
    public void SetDataAchievesment(Achievesment data, AchivesmentProgess progess)
    {
        this.achievement = data;
        this.achivesmentProgess = progess;
        foreach (var reward in rewardDataBase.rewardBases)
        {
            if (reward.rewardID == data.AchievesmentRewardID.rewardID)
            {
                rewardIMG.sprite = reward.rewardIMG;
                rewardQuality.text = data.AchievesmentRewardID.rewardQuality.ToString();
            }
        }
        UpdateUiAchievesment();

    }
    public void UpdateAchievesment(AchivesmentProgess progess)
    {
        this.achivesmentProgess = progess;
        UpdateUiAchievesment();

    }
    public void UpdateUiAchievesment()
    {
        achivesmentName.text = achievement.AchievesmentName;
        achievesmentDes.text = achievement.AchivesmentDecription;

        FillProgess();
    }
    public void FillProgess()
    {

        if (achievement.totalTask == 0)
        {
            var percentProgess = 1;
            fillProgess.fillAmount = percentProgess;
        }
        else
        {
            var percentProgess = achivesmentProgess.progessTask / achievement.totalTask;
            fillProgess.fillAmount = percentProgess;
        }
        CheckAchievesment();
    }
    public void CheckAchievesment()
    {
        if (achivesmentProgess.progessTask >= achievement.totalTask)
        {
            if (achivesmentProgess.hasClaimed == false)
            {
                achivesmentProgess.isComplete = true;
                Debug.Log("Mission Complete");
                claimRewward.interactable = true;
                claimRewward.image.color = Color.white;
            }
            else
            {
                claimRewward.image.color = Color.black;
                claimRewward.interactable = false;
                claimedIMG.gameObject.SetActive(true);
            }
        }
        else
        {
            achivesmentProgess.isComplete = false;
            claimRewward.interactable = false;
            claimRewward.image.color = Color.gray;
        }
    }
    public void OnClaim()
    {
        if (achivesmentProgess.isComplete == true)
        {
            claimRewward.image.color = Color.black;
            achivesmentProgess.hasClaimed = true;
            claimRewward.interactable = false;
            claimedIMG.gameObject.SetActive(true);
            AudioManager audio = FindAnyObjectByType<AudioManager>();
            audio.OnButtonClickAudio();
            AchivesmentManager achivesmentManager = AchivesmentManager.Instance;
            achivesmentManager.SaveAchievesment();
        }
    }
    public void ReceivedReward()
    {
        if (achievement.AchievesmentRewardID.rewardID == 1)
        {
            playerManager.playerData.whitePearl += achievement.AchievesmentRewardID.rewardQuality;
        }
        if (achievement.AchievesmentRewardID.rewardID == 3)
        {
            playerManager.playerData.playerExperience += achievement.AchievesmentRewardID.rewardQuality;
        }      
        playerManager.UpdateProgessData(playerManager.playerData);
        playerManager.SavePlayersData();
    }
  
}



