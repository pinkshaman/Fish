using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Toggle = UnityEngine.UI.Toggle;
public class OptionShow : MonoBehaviour
{
    public Button option1;
    public Button option2;
    //public Button option3;
    public FishShowUIHandle fishShowUIHandle;
    public SceneManagers sceneManagers;
    public PlayerData playerData;
    public LoadSaveData loadSaveData;
    public void Start()
    {
        fishShowUIHandle = FindObjectOfType<FishShowUIHandle>();
        option1.onClick.AddListener(Option1);
        option2.onClick.AddListener(Option2);
        //option3.onClick.AddListener(Option3);
    }
    public void OnShow()
    {
        foreach (var key in fishShowUIHandle.fishTankDict)
        {
            if (key.Key.isOn)
            {
                option1.gameObject.SetActive(true);
                option2.gameObject.SetActive(true);
                //option3.gameObject.SetActive(true);
            }
            else
            {
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
                //option3.gameObject.SetActive(false);
            }
        }
    }
 
    public void Option1()
    {
        var Value = fishShowUIHandle.GetFishDataFromCurrentToggle();
        Debug.Log($"Data :{Value.fishName}");
        
        loadSaveData.SetFishMain(playerData,Value.id);
        
    }
    public void Option2()
    {
        Debug.Log("Button2");
        sceneManagers.LoadShopScene();
    }
    public void Option3()
    {
        Debug.Log("Button3");
    }
    

}
