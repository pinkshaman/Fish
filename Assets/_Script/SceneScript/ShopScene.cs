using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : MonoBehaviour
{
    public FishDataBase fishDataBase;
    public FishTankBase fishTankBase;
    public FishShowUIHandle showUIHandle;
    public UnityEngine.UI.Button ChooseFishButton;
    public Text Message;
    public SceneManagers loadFishTankScene;
    public PlayerData playerData;
    public void Start()
    {     
        ChooseFishButton.onClick.AddListener(OnChooseFish);  
    }

    public void OnChooseFish()
    {
       
        FishData fishData = showUIHandle.GetFishDataFromCurrentToggle();
        if (fishData != null&& playerData.whilePearl>= fishData.Price)
        {
            playerData.whilePearl -= fishData.Price;
            Message.text = $"Selected : {fishData.fishName}";
            Debug.Log($"Fish selected: {fishData.fishName}");
            AddFishToTank(fishData);
            PlayerManager.Instance.SavePlayersData();
        }
        else if (playerData.whilePearl <= fishData.Price)
        {
            Message.text = $"Not Enough WhilePearl ";
        }
        else
        {
            
            Message.text = "No fish is selected";
        }
    }
    private void AddFishToTank(FishData fishData)
    {
        FishInTank newFish = new FishInTank
        {
            ID = fishData.id,
        };

        fishTankBase.fishTankBases.Add(newFish);
        SaveFishTankDataJson();
        Debug.Log("Fish added to tank list in FishTankBase.");
        Message.text = $"{fishData.fishName} has added to tank";
    }
    [ContextMenu("SaveFishTankDataJson")]
    public void SaveFishTankDataJson()
    {
        var value = JsonUtility.ToJson(fishTankBase);
        PlayerPrefs.SetString(nameof(fishTankBase), value);
        PlayerPrefs.Save();
    }

}
