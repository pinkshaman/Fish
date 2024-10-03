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
        StartCoroutine(CreateFishData(QuestID, 1.0f));
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
    public IEnumerator CreateFishData(int QuestID, float delay)
    {
        QuestHandle selectedQuestHandle = GetFishQuestData(QuestID);
        var selectedQuest = selectedQuestHandle.questData.fishList;

        int currentScalePoint = UiFish.fishMain.scalePoint;
        int maxScalePoint = GetMaxScalePoint(currentScalePoint);

        while (!UiFish.isGameEnd)
        {
            currentScalePoint = UiFish.fishMain.scalePoint;
            maxScalePoint = GetMaxScalePoint(currentScalePoint);
            List<FishData> filteredFishList = fishManager.fishDataBases.fishDatas.FindAll(fish => fish.scalePoint <= maxScalePoint && selectedQuest.Contains(fish.id));
            FishData randomFish = filteredFishList[Random.Range(0, filteredFishList.Count)];
            Debug.Log($"Selected fish: {randomFish.fishName}, Scale Point: {randomFish.scalePoint}");
            fishManager.CreateFishQuest(randomFish.id);
            yield return new WaitForSeconds(delay);

        }
    }
    private int GetMaxScalePoint(int scalePoint)
    {
        return scalePoint <= 20 ? 20 :
               scalePoint <= 40 ? 40 :
               scalePoint <= 60 ? 60 : 100;
    }
}
