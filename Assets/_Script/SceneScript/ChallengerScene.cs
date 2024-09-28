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
    public FishManager fishManager;
    public RewardManager rewardManager;
    public void Start()
    {
        Debug.Log($"input QuestID : {QuestID}");
        GetFishFromList(QuestID);
        rewardManager.CreateReward(QuestID);
        toggle.onValueChanged.AddListener(OpenQuestCheck);
    
    }

    public void OpenQuestCheck(bool isOn)
    {
        questTogglePanel.SetActive(isOn);
    }

    public QuestHandle GetFishQuestData(int questID)
    {
        QuestManager questManager = QuestManager.Instance;
        Dictionary<int, QuestHandle> questDictionary = questManager.GetQuests();
        Debug.Log($"Dictionary: {questDictionary.Keys} - {questDictionary.Values}");
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
    public void GetFishFromList(int QuestID)
    {
        QuestHandle selectedQuestHandle = GetFishQuestData(QuestID); 
        var selectedQuest = selectedQuestHandle.questData.fishList;
        int randomIndex = Random.Range(0, selectedQuest.Count);
        int fishID = selectedQuest[randomIndex];

        Debug.Log($"fishList for Challenger : {selectedQuest.Count}");
        fishManager.CreateFishQuest(fishID);


    }


}
