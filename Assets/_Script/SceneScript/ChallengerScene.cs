using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengerScene : MonoBehaviour
{
    public int QuestID;
    public QuestProgessDataBase processDataBase;
    public QuestDataBase questDataBase;
    public GameObject questTogglePanel;
    public Toggle toggle;
    public FishManager fishManager;
    public RewardManager rewardManager;
    public UIMainFishControl UiFish;
    public QuestByID questByID;
    public void Start()
    {
        toggle.onValueChanged.AddListener(OpenQuestCheck);
        Debug.Log($"input QuestID : {QuestID}");
        ChangeBGMusic();
        GetQuestDataByID(QuestID);
    }

    public void ChangeBGMusic()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeBackGroundMusic(2);
    }

    public void OpenQuestCheck(bool isOn)
    {
        questTogglePanel.SetActive(isOn);
    }


    public IEnumerator CreateFishData(List<int> fishList, float delay)
    {
        int currentScalePoint = UiFish.fishMain.scalePoint;
        int maxScalePoint = GetMaxScalePoint(currentScalePoint);

        while (!UiFish.isGameEnd)
        {
            currentScalePoint = UiFish.fishMain.scalePoint;
            maxScalePoint = GetMaxScalePoint(currentScalePoint);
            List<FishData> filteredFishList = fishManager.fishDataBases.fishDatas.FindAll(fish => fish.scalePoint <= maxScalePoint && fishList.Contains(fish.id));
            FishData randomFish = filteredFishList[Random.Range(0, filteredFishList.Count)];
            fishManager.CreateFishQuest(randomFish.id);
            yield return new WaitForSeconds(delay);

        }
        if (UiFish.isGameEnd)
        {
            fishManager.DestroyFish(UiFish.isGameEnd);
        }
    }
    private int GetMaxScalePoint(int scalePoint)
    {
        return scalePoint <= 20 ? 20 :
               scalePoint <= 40 ? 40 :
               scalePoint <= 60 ? 60 : 100;
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
    public void GetQuestDataByID(int questID)
    {
        LoadDataJson();
        foreach (var datas in questDataBase.questDataBases)
        {
            if (datas.questID == questID)
            {
                QuestProgessData processData = processDataBase.questProgessDatas.Find(processData => processData.id == datas.questID && datas.questID == questID);
                questByID.SetDataHandle(datas, processData);
                UiFish.Check(datas.fishList);
                StartCoroutine(CreateFishData(datas.fishList, 1.0f));
                rewardManager.SetDataListReward(datas.rewardListUpdate);
            }
        }
    }
}
