using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Loading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishShowUIHandle : MonoBehaviour
{
    public FishShowUiPanel fishPanel;
    public Transform rootUi;
    public Dictionary<Toggle, FishData> fishTankDict = new Dictionary<Toggle, FishData>();
    public Toggle togglePrefab;
    public Button option1;
    public Button option2;
    public Button option3;

    public SceneManagers sceneManagers;

    public virtual void Start()
    {
        option1.onClick.AddListener(Option1);
        option2.onClick.AddListener(Option2);
        option3.onClick.AddListener(Option3);
    }
    public virtual void CreateFishShow(FishData chooseFish)
    {
        var fish = Instantiate(fishPanel, rootUi);
        Toggle toggle = Instantiate(togglePrefab, fish.transform);
        toggle.gameObject.name = $"{chooseFish.id}"; // Đặt tên cho Toggle 
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        // Truyền Toggle và FishData vào FishPanel
        fish.SetData(chooseFish, toggle);


        // Thêm Toggle và FishData vào Dictionary
        AddToDictionary(toggle, chooseFish);
    }
    public virtual void AddToDictionary(Toggle toggle, FishData fishData)
    {
        if (!fishTankDict.ContainsKey(toggle))
        {
            fishTankDict.Add(toggle, fishData);
            Debug.Log($"Added Toggle for {toggle.name} to Dictionary.");
        }
    }
    public virtual void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            option1.gameObject.SetActive(true);
            option2.gameObject.SetActive(true);
            option3.gameObject.SetActive(true);
            // Lưu trữ Toggle hiện tại (đang được thay đổi)
            Toggle currentToggle = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();

            // Duyệt qua tất cả các Toggle trong dictionary
            foreach (var toggle in fishTankDict.Keys)
            {
                Debug.Log($"Toggle is On: {toggle.isOn}");
                // Nếu Toggle không phải là Toggle hiện tại và đang bật
                if (toggle != currentToggle && toggle.isOn)
                {
                    // Tắt Toggle
                    toggle.isOn = false;
                }
            }
            
        }
        else
        {
            option1.gameObject.SetActive(false);
            option2.gameObject.SetActive(false);
            option3.gameObject.SetActive(false);
        }
    }
    public FishData GetFishDataFromCurrentToggle()
    {
        foreach (var key in fishTankDict)
        {
            if (key.Key.isOn)
            {
                return key.Value;
            }
        }
        return null; // Nếu không có Toggle nào được bật
    }
    public virtual void Option1()
    {
        //FishData chooseFish = GetFishDataFromCurrentToggle();
        //Debug.Log($"Data : {chooseFish.fishName}");
        //sceneManagers.LoadPlayScene();
        //FishManager.Instance.CreateFish(chooseFish);
        StartCoroutine(LoadSceneThenLoadFish());
    }
    public virtual void Option2()
    {

    }
    public virtual void Option3()
    {

    }
    private IEnumerator LoadSceneThenLoadFish()
    {
        sceneManagers.LoadPlayScene();
        yield return SceneLoadingStatus.Complete;
        FishData chooseFish = GetFishDataFromCurrentToggle();
        FishManager.Instance.CreateFish(chooseFish);
        Debug.Log("CreateFish");
    }

}