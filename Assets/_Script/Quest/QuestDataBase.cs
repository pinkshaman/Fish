using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Quest/newQuestData")]
public class QuestDataBase : ScriptableObject
{
    public List<QuestData> questDataBases;
}
