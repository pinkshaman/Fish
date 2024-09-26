using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;



[Serializable]
public class QuestData
{
    public int questID;
    public string QuestName;
    public string QuestDecription;
    public int TaskCount;
    public List<int> fishList;
}

public class QuestManager : MonoBehaviour
{
    public FishManager fishManager;
    public QuestDataBase questDataBase;
    public RewardDataBase rewardDataBase;
    public QuestProgessDataBase processDataBase;
    public QuestHandle questHandle;
    public Transform rootQuestUI;
    public Dictionary<int, QuestHandle> DictionaryQuestHandle;
    public List<RewardBase> RewardList;
    public GameObject questTogglePanel;
    public Toggle toggle;


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
        while (isComplete==false)
        {
            int randomIndex = Random.Range(0, selectedQuest.fishList.Count);
            Debug.Log($"fishLish : {selectedQuest.fishList.Count}");
            fishManager.CreateFishQuest(randomIndex);
            if(isComplete==true ) { break; }
        }
    }

    [ContextMenu("SaveDataJson")]
    public void SaveDataJson()
    {
        var value = JsonUtility.ToJson(processDataBase);
        PlayerPrefs.SetString(nameof(processDataBase), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadDataJson")]
    public void LoadDataJson()
    {
        var defaultValue = JsonUtility.ToJson(processDataBase);
        var json = PlayerPrefs.GetString(nameof(processDataBase), defaultValue);
        processDataBase = JsonUtility.FromJson<QuestProgessDataBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }

    public void CreateQuest(QuestData questdata, QuestProgessData progessData)
    {
        var quest = Instantiate(questHandle, rootQuestUI);

        quest.SetQuestData(questdata, progessData, RewardList);
        DictionaryQuestHandle.Add(questdata.questID, quest);
    }
    public void UpdateQuestProgess(QuestProgessData questProgess)
    {
        var QuestIndex = processDataBase.questProgessDatas.FindIndex(progess => questProgess.id == progess.id);
        processDataBase.questProgessDatas[QuestIndex] = questProgess;
        DictionaryQuestHandle[questProgess.id].UpdateProgess(questProgess);
    }
    public void UpdateRewardQuality(int rewardID, int newQuality)
    {
        foreach (var rewardData in RewardList)
        {
            RewardBase reward = rewardDataBase.rewardBases.Find(r => r.rewardID == rewardID);
            reward.rewardQuality = newQuality;

        }
    }
    public void Start()
    {
        toggle.onValueChanged.AddListener(OpenQuestCheck);
        LoadDataJson();

        DictionaryQuestHandle = new Dictionary<int, QuestHandle>();
        RewardList = new List<RewardBase>();

        Debug.Log("IdQuestHanle is Created");
        foreach (var datas in questDataBase.questDataBases)
        {
            QuestProgessData processData = processDataBase.questProgessDatas.Find(processData => processData.id == datas.questID);
            if (processData == null)
            {
                processData = new QuestProgessData(datas.questID, false, false, 0);
                processDataBase.questProgessDatas.Add(processData);
                SaveDataJson();               
            }
            CreateQuest(datas, processData);
        }
    }
}
