using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class FishShowUi : FishShowUIHandle
{
    public override void Start()
    {
        
    }
    public override void OnToggleValueChanged(bool isOn)
    {
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
}
