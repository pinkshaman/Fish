using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestByID : MonoBehaviour
{
    public Transform RootUiQuest;
    public QuestHandle questHandle;
   
    public void SetDataHandle(QuestData questData, QuestProgessData progessData)
    {        
        var quest = Instantiate(questHandle, RootUiQuest);
        quest.SetQuestData(questData, progessData);
    }
   
}
