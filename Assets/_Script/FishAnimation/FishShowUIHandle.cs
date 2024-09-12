using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishShowUIHandle : MonoBehaviour
{
    public FishShowUiPanel fishPanel;
    public Transform rootUi;
    public Dictionary<Toggle, FishData> fishTankDict = new Dictionary<Toggle, FishData>();
    public Toggle togglePrefab;
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;
   
    public virtual void Start()
    {       
        option1.SetActive(false);
        option2.SetActive(false);
        option3.SetActive(false);
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
            option1.SetActive(isOn);
            option2.SetActive(isOn);
            option3.SetActive(isOn);
            // Lưu trữ Toggle hiện tại (đang được thay đổi)
            Toggle currentToggle = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();

            // Duyệt qua tất cả các Toggle trong dictionary
            foreach (var toggle in fishTankDict.Keys)
            {
                // Nếu Toggle không phải là Toggle hiện tại và đang bật
                if (toggle != currentToggle && toggle.isOn)
                {
                    // Tắt Toggle
                    toggle.isOn = false;
                }
            }
            Debug.Log($"{currentToggle.name}");
        }
        else
        {
            option1.SetActive(false);
            option2.SetActive(false);
            option3.SetActive(false);
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
       
    }
    public virtual void Option2()
    {

    }
    public virtual void Option3()
    {

    }
}