using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerScript : MonoBehaviour
{
    public Transform RootUiQuest;
    public QuestHandle questHandle;
    public void Start()
    {

    }
    public QuestHandle GetQuestDataByID(int questID)
    {
        QuestManager questManager = QuestManager.Instance;
        Dictionary<int, QuestHandle> questDictionary = questManager.GetQuests();
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
    public void SetDataHandle(int questID)
    {
        questHandle = GetQuestDataByID(questID);
        QuestData questData = questHandle.questData;
        QuestProgessData progessData = questHandle.questProgessData;
        var quest = Instantiate(questHandle,RootUiQuest);
        quest.SetQuestData(questData, progessData);
    }
}
