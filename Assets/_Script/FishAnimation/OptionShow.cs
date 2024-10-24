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
    public PlayerData playerDatas;
    public LoadSaveData loadSaveData;
    public FishTankManager fishTankManager;
    public Text whitePearl;
    public Text blackPearl;
    public void Start()
    {
        loadSaveData.LoadData();
        playerDatas = loadSaveData.playerData;
        UpdateBalance();
        fishShowUIHandle = FindAnyObjectByType<FishShowUIHandle>();
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
        loadSaveData.SetFishMain(playerDatas, Value.id);
    }
    public void Option2()
    {
        var Value = fishShowUIHandle.GetFishDataFromCurrentToggle();
        var keyName = fishShowUIHandle.GetKeyFromCurrentToggle();
        Debug.Log($"Data: {Value.fishName}");
        SellFishOnTank(Value);
        fishShowUIHandle.RemoveFishShow(keyName);
        fishShowUIHandle.UpdateFishShow();
        AudioManager audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.CointEffect();
    }
    public void UpdateBalance()
    {
        whitePearl.text = playerDatas.whilePearl.ToString();
        blackPearl.text = playerDatas.blackPearl.ToString();
    }
    public void SellFishOnTank(FishData fishData)
    {

        FishInTank fishTank =fishTankManager.fishTankBase.fishTankBases.Find(fish => fish.ID == fishData.id);
        fishTankManager.fishTankBase.fishTankBases.Remove(fishTank);
        fishTankManager.RemoveFishFromTank(fishTank);
        Debug.Log($"Sell : {fishTank.ID} = {fishData.Price}");
        playerDatas.whilePearl += fishData.Price;
        UpdateBalance();
        loadSaveData.SavesData(playerDatas);
        fishTankManager.SaveFishTankDataJson();
    }
    public void Option3()
    {
        Debug.Log("Button3");
    }
    public void OnApplicationQuit()
    {

        loadSaveData.SavesData(playerDatas);
    }



}
