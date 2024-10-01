using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOnOff : MonoBehaviour
{
    public Toggle toggle;
    public GameObject panel;   
    public Button button;
    public void Start()
    {
        toggle.onValueChanged.AddListener(OnClick);
        
    }
    public void OnClick(bool isOn)
    {
        //AudioManager audio = FindObjectOfType<AudioManager>();
        //audio.OnButtonClickAudio();
        if (panel != null)
        {
            panel.SetActive(isOn);
            button.gameObject.SetActive(isOn);          
        }
    }    
}
