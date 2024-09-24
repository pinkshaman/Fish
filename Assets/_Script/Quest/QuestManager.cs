using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public class QuestData
{
    public int questID;
    public string QuestName;
    public string QuestDecription;
    public Sprite QuestReward;
    public int TaskCount;
    public int rewardQuality;
    

    public int fishQuality;
    public Vector2 fishPositions;
    public string fishName;
    public float scalePoint;
    public float speed;
    public Sprite fishSprite;
    public RuntimeAnimatorController controller;
    public int questScore;
}

public class QuestManager : MonoBehaviour
{
    public FishManager fishManager;
    public QuestDataBase questDataBase;
    public RewardManager rewardManager;
    public QuestProgessDataBase processDataBases;
    public QuestHandle questHandle;
    public Transform rootQuestUI;
    public Dictionary<int,QuestHandle> DictionaryQuestHandle;



    public Vector2 moveAreaMin;
    public Vector2 moveAreaMax;
    public GameObject questTogglePanel;
    public Toggle toggle;


    public void OpenQuestCheck(bool isOn)
    {
        questTogglePanel.SetActive(isOn);
    }
    // Hàm chọn nhiệm vụ theo questID
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

    // Đặt vị trí cho các phần tử cá trong nhiệm vụ
    public void SetFishPositionForQuest(QuestData questData)
    {
        questData.fishPositions = GenerateRandomPosition();
    }

    // Hàm xử lý tạo cá và vị trí theo nhiệm vụ
    public void LoadFishForQuest(int questID)
    {
        QuestData selectedQuest = GetQuestDataByID(questID);
        if (selectedQuest != null)
        {
            SetFishPositionForQuest(selectedQuest); // Set vị trí cho cá
            List<QuestData> selectedQuestList = new List<QuestData> { selectedQuest };
            fishManager.CreateFishesFromDataQuest(selectedQuestList); // Tạo cá từ dữ liệu
            rewardManager.GetRewardByQuestID(questID);
        }
    }

    private Vector2 GenerateRandomPosition()
    {
        float randomX = UnityEngine.Random.Range(moveAreaMin.x, moveAreaMax.x);
        float randomY = UnityEngine.Random.Range(moveAreaMin.y, moveAreaMax.y);
        return new Vector2(randomX, randomY);
    }

    [ContextMenu("SaveDataJson")]
    public void SaveDataJson()
    {
        var value = JsonUtility.ToJson(processDataBases);
        PlayerPrefs.SetString(nameof(processDataBases), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadDataJson")]
    public void LoadDataJson()
    {
        var defaultValue = JsonUtility.ToJson(processDataBases);
        var json = PlayerPrefs.GetString(nameof(processDataBases), defaultValue);
        processDataBases = JsonUtility.FromJson<QuestProgessDataBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }

    public void CreateQuest(QuestData questdata, QuestProgessData progessData)
    {
        var quest = Instantiate(questHandle, rootQuestUI);
        quest.SetQuestData(questdata, progessData);
        DictionaryQuestHandle.Add(questdata.questID, quest);
    }
    public void UpdateQuestProgess(QuestProgessData  questProgess)
    {
        var QuestIndex = processDataBases.questProgessDatas.FindIndex(progess => questProgess.id == progess.id);
        processDataBases.questProgessDatas[QuestIndex] = questProgess;
        DictionaryQuestHandle[questProgess.id].UpdateProgess(questProgess);
    }
    public void Start()
    {             
        toggle.onValueChanged.AddListener(OpenQuestCheck);
        LoadDataJson();
        DictionaryQuestHandle = new Dictionary<int, QuestHandle>();
        Debug.Log("IdQuestHanle is Created");
        foreach (var datas in questDataBase.questDataBases)
        {
            QuestProgessData processData = processDataBases.questProgessDatas.Find(processData => processData.id == datas.questID);
            CreateQuest(datas, processData);
        }
    }
}
