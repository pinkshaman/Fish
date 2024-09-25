using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestProgessData
{
    public int id;
    public bool isComplete;
    public bool hasClaimed;
    public int currentQuestProgess;

    public QuestProgessData(int id, bool isComplete, bool hasClaimed, int currenProgess)
    {
        this.id = id;
        this.isComplete = isComplete;
        this.hasClaimed = hasClaimed;
        this.currentQuestProgess = currenProgess;
    }
}
[Serializable]
public class QuestProgessDataBase
{
    public List<QuestProgessData> questProgessDatas;
}

