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
}
[Serializable]
public class QuestProgessDataBase
{
    public List<QuestProgessData> questProgessDatas;
}

