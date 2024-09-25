using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerScene : MonoBehaviour
{
    public int QuestID;
    public QuestManager questManager;
  
    public void Start()
    {
        SetQuestForScene();  
    }
    public void SetQuestForScene()
    {
        questManager.GetFishFromList(QuestID);
    }
   

}
