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
    public UIMainFishControl UiFish;
    public void Start()
    {
        toggle.onValueChanged.AddListener(OpenQuestCheck);
        Debug.Log($"input QuestID : {QuestID}");
        StartCoroutine(GetFishFromList(QuestID));
        rewardManager.CreateReward(QuestID);
        UiFish.Check(QuestID);
        ChangeBGMusic();
    }
    public void ChangeBGMusic()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeBackGroundMusic(2);
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
    public IEnumerator GetFishFromList(int QuestID)
    {
        QuestHandle selectedQuestHandle = GetFishQuestData(QuestID);
        var selectedQuest = selectedQuestHandle.questData.fishList;
        while (!UiFish.isGameEnd)
        {
            int randomIndex = Random.Range(0, selectedQuest.Count);
            Debug.Log($"RandomIndex: {randomIndex}");
            int fishID = selectedQuest[randomIndex];
            Debug.Log($"fishList for Challenger: {fishID}");

            fishManager.CreateFishQuest(fishID);

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void CreateFishData(int QuestID)
    {
        QuestHandle selectedQuestHandle = GetFishQuestData(QuestID);
        var selectedQuest = selectedQuestHandle.questData.fishList;
        if (UiFish.fishMain.scalePoint < 20)
        {
            foreach (var fishID in selectedQuest)
            {
                FishData fishData = fishManager.fishDataBases.fishDatas.Find(fish => fish.scalePoint <= 20 && fish.id == fishID);
               

            }
        }
    }
}
