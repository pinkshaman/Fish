using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishShopUiHandle : MonoBehaviour
{
    public FishShowUiPanel fishPanel;
    public Dictionary<Toggle, FishData> fishList = new Dictionary<Toggle, FishData>();
    public Toggle togglePrefab;
    public Transform rootSellFishUI;
    public  void CreateFishShow(FishData chooseFish)
    {
        var fish = Instantiate(fishPanel, rootSellFishUI);
        Toggle toggle = Instantiate(togglePrefab, fish.transform);
        toggle.gameObject.name = $"{chooseFish.id}"; 
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        fish.SetData(chooseFish, toggle);
       
        AddToDictionary(toggle, chooseFish);
    }
    public void AddToDictionary(Toggle toggle, FishData fishData)
    {
        if (!fishList.ContainsKey(toggle))
        {
            fishList.Add(toggle, fishData);
            Debug.Log($"Added Toggle for {toggle.name} to Dictionary.");
        }
    }
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {           
            Toggle currentToggle = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
           
            foreach (var toggle in fishList.Keys)
            {
                Debug.Log($"Toggle is On:{toggle.name} - {toggle.isOn}");              
                if (toggle != currentToggle && toggle.isOn)
                {
                   
                    toggle.isOn = false;
                }
            }

        }
    }
    public FishData GetFishDataFromCurrentToggleSell()
    {
        foreach (var key in fishList)
        {
            if (key.Key.isOn)
            {
                return key.Value;
            }
        }
        return null; 
    }
}
