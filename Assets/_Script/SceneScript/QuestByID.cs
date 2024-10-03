using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestByID : MonoBehaviour
{
    public Transform RootUiQuest;
    public QuestHandle questHandle;
    public void Start()
    {

    }
    public QuestHandle GetQuestDataByID(int questID)
    {      
        QuestManager questManager = FindObjectOfType<QuestManager>();
        Dictionary<int, QuestHandle> questDictionary = questManager.GetQuests();
        Debug.Log($"Dictionary: {questDictionary.Keys} - {questID}");
        foreach (var quest in questDictionary)
        {
            if (quest.Key == questID)
            {
                return quest.Value;
            }
        }
        Debug.LogError("Dictionary not found!");
        return null;
    }
    public void SetDataHandle(int questID)
    {
        questHandle = GetQuestDataByID(questID);
        QuestData questData = questHandle.questData;
        QuestProgessData progessData = questHandle.questProgessData;
        var quest = Instantiate(questHandle,RootUiQuest);
        quest.SetQuestData(questData, progessData);
    }
}
