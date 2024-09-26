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
    public List<RewardBase> rewardList;
}

public class QuestManager : MonoBehaviour
{
    public QuestDataBase questDataBase;
    public QuestProgessDataBase processDataBase;
    public QuestHandle questHandle;
    public Transform rootQuestUI;
    public Dictionary<int, QuestHandle> DictionaryQuestHandle;
    

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
        quest.SetQuestData(questdata, progessData);
        DictionaryQuestHandle.Add(questdata.questID, quest);
        Debug.Log ($" RewardList: {questdata.rewardList.Count}");
    }
    public void UpdateQuestProgess(QuestProgessData questProgess)
    {
        var QuestIndex = processDataBase.questProgessDatas.FindIndex(progess => questProgess.id == progess.id);
        processDataBase.questProgessDatas[QuestIndex] = questProgess;
        DictionaryQuestHandle[questProgess.id].UpdateProgess(questProgess);
    }
  
    public void Start()
    {       
        LoadDataJson();

        DictionaryQuestHandle = new Dictionary<int, QuestHandle>();

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
