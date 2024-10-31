using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
    public int QuestID;
    public QuestProgessDataBase processDataBase;    
    public GameObject questTogglePanel;
    public Toggle toggle;
    public FishManager fishManager;
    public RewardManager rewardManager;
    public UIMainFishControl UiFish;
    public QuestByID questByID;
    public MapDataBase mapDataBase;
    public MapChallenger mapPrefabs;
    public Transform rootMap;
    public List<FishData> filteredFishList;

    public void Start()
    {
        QuestID = PlayerPrefs.GetInt("QuestID");
        toggle.onValueChanged.AddListener(OpenQuestCheck);
        Debug.Log($"input QuestID : {QuestID}");
        ChangeBGMusic();
        GetQuestDataByID(QuestID);
    }

    public void ChangeBGMusic()
    {
        AudioManager audioManager = AudioManager.Instance;
        audioManager.ChangeBackGroundMusic(2);
    }

    public void OpenQuestCheck(bool isOn)
    {
        questTogglePanel.SetActive(isOn);
    }


    public IEnumerator CreateFishData(List<int> fishList, float delay,QuestProgessData progessData)
    {
        while (!UiFish.isGameEnd)
        {
            int currentScalePoint = UiFish.UiScore.fishMain.scalePoint;
            int maxScalePoint = GetMaxScalePoint(currentScalePoint);
            filteredFishList = fishManager.fishDataBases.fishDatas.FindAll(fish => fish.scalePoint <= maxScalePoint && fishList.Contains(fish.id));
            FishData randomFish = filteredFishList[Random.Range(0, filteredFishList.Count)];
            fishManager.CreateFishQuest(randomFish.id);
            if (randomFish.id < 30)
            {
                fishManager.CreateFishGroup(randomFish.id);
            }
            yield return new WaitForSeconds(delay);

        }
        if (UiFish.isGameEnd)
        {
            fishManager.DestroyFish(UiFish.isGameEnd);
            int fishLives = UiFish.UiScore.ReturnLives();
           if(fishLives > 0 && progessData.id !=1)
            {
                progessData.currentQuestProgess = 1;
                QuestManager questManager = QuestManager.Instance;
                questManager.UpdateQuestProgess(progessData);
                SaveDataJson();
            }
        }
    }
    private int GetMaxScalePoint(int scalePoint)
    {
        return scalePoint < 20 ? 20 :
               scalePoint < 40 ? 40 :
               scalePoint < 60 ? 60 : 100;
    }
    public void LoadDataJson()
    {
        var defaultValue = JsonUtility.ToJson(processDataBase);
        var json = PlayerPrefs.GetString(nameof(processDataBase), defaultValue);
        processDataBase = JsonUtility.FromJson<QuestProgessDataBase>(json);
        Debug.Log("LoadDataJson is Loaded");
    }
    public void SaveDataJson()
    {
        var value = JsonUtility.ToJson(processDataBase);
        PlayerPrefs.SetString(nameof(processDataBase), value);
        PlayerPrefs.Save();
    }
    public void LoadMap(int mapID)
    {
        MapData mapdata = mapDataBase.mapDataBases.Find(map => map.mapID == mapID);
        var newMap = Instantiate(mapPrefabs, rootMap);
        newMap.SetMap(mapdata);
    }
    public void GetQuestDataByID(int questID)
    {
        LoadDataJson();

        QuestManager questManager = QuestManager.Instance;
        foreach (var key in questManager.DictionaryQuestHandle.Keys)
        {
            if (key == questID)
            {
                QuestData quest = questManager.DictionaryQuestHandle[key].questData;
                QuestProgessData progess = questManager.DictionaryQuestHandle[key].questProgessData;
                if (progess == null || progess.id != questID)
                {
                    progess = new QuestProgessData(quest.questID, false, false, 0);
                    processDataBase.questProgessDatas.Add(progess);
                    SaveDataJson();
                }
                    questByID.SetDataHandle(quest, progess);
                    UiFish.Check(quest.fishList);
                StartCoroutine(CreateFishData(quest.fishList, 3.0f, progess));
                    rewardManager.SetDataListReward(quest.rewardListUpdate);
                    LoadMap(quest.mapData);

                
            }
        }
    }
}
