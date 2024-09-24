using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestHandle : MonoBehaviour
{
    public QuestData questData;
    public QuestProgessData questProgessData;
    public TMP_Text QuestName;
    public TMP_Text QuestDescription;
    //public TMP_Text QuestType;
    public TMP_Text QuestRewardQuality;
    public Image RewardIMG;
    public Image ProgessBar;
    public Image ProgessFillBar;
    public Image RewardClaimed;
    public Button Claim;

    public void Start()
    {
        Claim.onClick.AddListener(OnClaim);
    }
    public void UpdateProgess(QuestProgessData progessData)
    {
        this.questProgessData = progessData;
        UpdateUI();
    }
    public void SetQuestData(QuestData data, QuestProgessData progessData)
    {
        this.questData = data;
        this.questProgessData = progessData;
        UpdateUI();

    }
    public void UpdateUI()
    {
        QuestName.text = questData.QuestName;
        QuestDescription.text = questData.QuestDecription;
        QuestRewardQuality.text = questData.rewardQuality.ToString();
        RewardIMG.sprite = questData.QuestReward;
        FillProgess();
    }
    public void OnClaim()
    {
        if(questProgessData.isComplete==true)
        {
            Claim.image.color = Color.black;
            questProgessData.hasClaimed = true;
            Claim.interactable = false;
            RewardClaimed.gameObject.SetActive(true);
        }
    }
    public void FillProgess()
    {
        float currentPecent = (float)questProgessData.currentQuestProgess/ questData.TaskCount;
        ProgessFillBar.fillAmount = currentPecent;
        CheckQuest();
    }
    public void CheckQuest()
    {
        if (questProgessData.currentQuestProgess >= questData.TaskCount)
        {
            if (questProgessData.hasClaimed == false)
            {
                questProgessData.isComplete = true;
                Debug.Log("Mission Complete");
                Claim.interactable = true;
                Claim.image.color = Color.white;
            }
            else
            {
                Claim.image.color = Color.black;
                Claim.interactable = false;
               RewardClaimed.gameObject.SetActive(true);
            }
        }
        else
        {
            questProgessData.isComplete = false;
            Claim.interactable = false;
            Claim.image.color = Color.gray;
        }

    }
    


}
