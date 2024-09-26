using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengerScene : MonoBehaviour
{
    public int QuestID;
    public QuestDataBase questDataBase;
    public GameObject questTogglePanel;
    public Toggle toggle;
    public QuestHandle questHandle;
    public FishManager fishManager;
    public RewardManager rewardManager;
    public void Start()
    {
        Debug.Log($"input QuestID : {QuestID}");
        toggle.onValueChanged.AddListener(OpenQuestCheck);    
        GetFishFromList(QuestID);
        rewardManager.UpdateRewards(QuestID);
    }
   
    public void OpenQuestCheck(bool isOn)
    {
        questTogglePanel.SetActive(isOn);
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
    public void GetFishFromList(int questid)
    {

        QuestData selectedQuest = GetQuestDataByID(questid);
        bool isComplete = questHandle.IsCompleteGame();
        Debug.Log($"isComplete : {isComplete}");
        while (isComplete == false)
        {
            int randomIndex = Random.Range(0, selectedQuest.fishList.Count);
            int fishID = selectedQuest.fishList[randomIndex];
            Debug.Log($"fishList for Challenger : {selectedQuest.fishList.Count}");        
            fishManager.CreateFishQuest(fishID);
            if (isComplete == true) { break; }
        }
    }


}
