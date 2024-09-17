using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[Serializable]
public class QuestDataTest

{
    public int questID;
    public int quality;
    public Vector2 fishPositions;
    public string fishName;
    public float scalePoint;
    public float speed;
    public Sprite fishSprite;
    public RuntimeAnimatorController controller;
    public int Point;
   
}
[Serializable]
public class QuestDataBaseTest
{
    public List<QuestDataTest> questDatas;
}


public class TestGameController : MonoBehaviour
{
    public FishManager fishManager;
    public QuestDataBaseTest questDataBase;
    
    public Vector2 moveAreaMin;
    public Vector2 moveAreaMax;
    
    // Hàm chọn nhiệm vụ theo questID
    private QuestDataTest GetQuestDataByID(int questID)
    {
        foreach (var questData in questDataBase.questDatas)
        {
            if (questData.questID == questID)
            {
                return questData;
            }
        }
        Debug.LogError("Quest not found!");
        return null;
    }

    // Đặt vị trí cho các phần tử cá trong nhiệm vụ
    public void SetFishPositionForQuest(QuestDataTest questData)
    {
        questData.fishPositions = GenerateRandomPosition();
    }

    // Hàm xử lý tạo cá và vị trí theo nhiệm vụ
    public void LoadFishForQuest(int questID)
    {
        QuestDataTest selectedQuest = GetQuestDataByID(questID);
        if (selectedQuest != null)
        {
            SetFishPositionForQuest(selectedQuest); // Set vị trí cho cá
            List<QuestDataTest> selectedQuestList = new List<QuestDataTest> { selectedQuest };
            fishManager.CreateFishesFromDataQuest(selectedQuestList); // Tạo cá từ dữ liệu
        }
    }

    private Vector2 GenerateRandomPosition()
    {
        float randomX = UnityEngine.Random.Range(moveAreaMin.x, moveAreaMax.x);
        float randomY = UnityEngine.Random.Range(moveAreaMin.y, moveAreaMax.y);
        return new Vector2(randomX, randomY);
    }

    public void Start()
    {
        int currentQuestID = 1; // Giả sử bạn đang ở nhiệm vụ ID 1
        LoadFishForQuest(currentQuestID);
    }
}
