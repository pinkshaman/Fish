using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchivesmentProgess
{
    public int progessID;
    public bool isComplete;
    public int progessTask;
    public bool hasClaimed;
    public AchivesmentProgess(int id , bool isComplete, int progesstask,bool hasClaimed)
    {
        this.progessID = id;
        this.isComplete = isComplete;
        this.progessTask = progesstask;
        this.hasClaimed = hasClaimed;
    }
}
[Serializable]
public class AchivesmentProgessList
{
    public List<AchivesmentProgess> achivesmentProgesses;
}
public class AchivesmentManager : MonoBehaviour
{
    public static AchivesmentManager Instance { get; private set; }

    public AchievesmentList achievesmentList;
    public AchivesmentProgessList achievesmentProgessList;
    public AchievesmentHandle AchievesmentHandle;
    public Transform rootAchivesment;
    public Dictionary<int, AchievesmentHandle> DictionaryAchievesment;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        LoadAchievesment();
        DictionaryAchievesment = new Dictionary<int, AchievesmentHandle>();
        foreach ( var achivesment in achievesmentList.achievesmentLists)
        {
            var progess = achievesmentProgessList.achivesmentProgesses.Find(progess => progess.progessID == achivesment.achivesmentID);
            if (progess == null)
            {
                progess = new AchivesmentProgess(achivesment.achivesmentID, false, 0, false);
                achievesmentProgessList.achivesmentProgesses.Add(progess);
                SaveAchievesment();
            }

            CreateAchievesment(achivesment, progess);
        }
    }
    public void CreateAchievesment(Achievesment data, AchivesmentProgess progess)
    {
        var achivesHandle = Instantiate(AchievesmentHandle, rootAchivesment);
        achivesHandle.SetDataAchievesment(data, progess);
        DictionaryAchievesment.Add(data.achivesmentID, achivesHandle);
    }

    public void UpdateQuestProgess(AchivesmentProgess achievesProgess)
    {
        var QuestIndex = achievesmentProgessList.achivesmentProgesses.FindIndex(progess => achievesProgess.progessID == progess.progessID);
        achievesmentProgessList.achivesmentProgesses[QuestIndex] = achievesProgess;
        DictionaryAchievesment[achievesProgess.progessID].UpdateAchievesment(achievesProgess);
    }
    public void OnApplicationQuit()
    {
        SaveAchievesment();
    }


    [ContextMenu("SaveAchivesment")]
    public void SaveAchievesment()
    {
        var value = JsonUtility.ToJson(achievesmentProgessList);
        PlayerPrefs.SetString(nameof(achievesmentProgessList), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadAchivesment")]
    public void LoadAchievesment()
    {
        var defaultValue = JsonUtility.ToJson(achievesmentProgessList);
        var json = PlayerPrefs.GetString(nameof(achievesmentProgessList), defaultValue);
        achievesmentProgessList = JsonUtility.FromJson<AchivesmentProgessList>(json);
        Debug.Log("ProgessAchievesment is Loaded");
    }

}
