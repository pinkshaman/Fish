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
    

    public virtual void Start()
    {
        
    }
    public virtual void CreateFishShow(FishData chooseFish)
    {
        var fish = Instantiate(fishPanel, rootUi);
        Toggle toggle = Instantiate(togglePrefab, fish.transform); 
        var toggleName = fish.GetInstanceID();
        toggle.gameObject.name = $"{toggleName}"; // Đặt tên cho Toggle 
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
      
        // Truyền Toggle và FishData vào FishPanel
        fish.SetData(chooseFish, toggle);
        // Thêm Toggle và FishData vào Dictionary
        AddToDictionary(toggle, chooseFish);
    }
    public void RemoveFishShow(string keyName)
    {
        foreach(var key in fishTankDict.Keys)
        {
            if(key.name.ToString() == keyName)
            {
                fishTankDict.Remove(key);
            }
        }
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
            // Lưu trữ Toggle hiện tại (đang được thay đổi)
            Toggle currentToggle = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();

            // Duyệt qua tất cả các Toggle trong dictionary
            foreach (var toggle in fishTankDict.Keys)
            {
                Debug.Log($"Toggle is On:{toggle.name} {toggle.isOn}");
                // Nếu Toggle không phải là Toggle hiện tại và đang bật
                if (toggle != currentToggle && toggle.isOn)
                {
                    // Tắt Toggle
                    toggle.isOn = false;
                }
            }
        }
    }
    public virtual FishData GetFishDataFromCurrentToggle()
    {
        foreach (var key in fishTankDict)
        {
            if (key.Key.isOn)
            {
                return key.Value;
            }
        }
        return null;
    }
    public virtual string GetKeyFromCurrentToggle()
    {
        foreach (var key in fishTankDict)
        {
            if (key.Key.isOn)
            {
                string keyName = key.Key.ToString();
                return keyName;
            }
        }
        return null;
    }

}