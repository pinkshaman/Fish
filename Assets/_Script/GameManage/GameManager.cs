using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;

[Serializable]
public class DataSetting
{
    public PlayerData playerData;
    public QuestProgessDataBase questProgessDataBase;
    public AchivesmentProgessList achivesmentProgessList;
    public FishTankBase fishTankBase;

}
public class GameManager : MonoBehaviour
{
    public DataSetting dataSetting = new DataSetting();
    private string fileName = "file.dat";
    public void SaveData()
    {

        LoadFromPlayerPrefs();
        OpenSavedGame();
    }

    void OpenSavedGame()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            Debug.Log("Saved Successful");
            var update = new SavedGameMetadataUpdate.Builder().Build();
            var json = JsonUtility.ToJson(dataSetting);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            savedGameClient.CommitUpdate(game, update,bytes,OnSavedGameWritten);
        }
        else
        {
            // handle error
            Debug.Log("Saved Failed");
        }
    }
    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }
    public void LoadData()
    {

        OpenLoadGame();
        SaveToPlayerPrefs();
    }
    public void OpenLoadGame()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, LoadGameData);
    }
    public void LoadGameData(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Load Game Data Success");
            savedGameClient.ReadBinaryData(data, OnSavedGameDataRead);
        }
        else
        {
            Debug.Log("Save Game Data Failed");
        }
    }
    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] loadedData)
    {
        string data = System.Text.Encoding.UTF8.GetString(loadedData);
        if(data == "")
        {
            SaveData();
        }
        else
        {
            dataSetting = JsonUtility.FromJson<DataSetting>(data);
        }
    }
   public void DeleteGameData(string filename)
    {
        // Open the file to get the metadata.
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, DeleteSavedGame);
    }

    public void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
        }
        else
        {
            // handle error
        }
    }
    public void SaveToPlayerPrefs()
    {
        if (dataSetting.playerData != null)
        {
            var value = JsonUtility.ToJson(dataSetting.playerData);
            PlayerPrefs.SetString("playerData", value);
        }       
        if (dataSetting.questProgessDataBase != null)
        {
            var value = JsonUtility.ToJson(dataSetting.questProgessDataBase);
            PlayerPrefs.SetString("processDataBase", value);
        }      
        if (dataSetting.achivesmentProgessList != null)
        {
            var value = JsonUtility.ToJson(dataSetting.achivesmentProgessList);
            PlayerPrefs.SetString("achievesmentProgessList", value);
        }    
        if (dataSetting.fishTankBase != null)
        {
            var value = JsonUtility.ToJson(dataSetting.fishTankBase);
            PlayerPrefs.SetString("fishTankBase", value);
        }
        PlayerPrefs.Save();

    }

    public void LoadFromPlayerPrefs()
    {

        string playerData = PlayerPrefs.GetString("playerData", "");
        string processDataBase = PlayerPrefs.GetString("processDataBase", "");
        string achivesmentProgessList = PlayerPrefs.GetString("achievesmentProgessList", "");
        string fishTankBase = PlayerPrefs.GetString("fishTankBase", "");

        if (!string.IsNullOrEmpty(playerData))
            dataSetting.playerData = JsonUtility.FromJson<PlayerData>(playerData);

        if (!string.IsNullOrEmpty(processDataBase))
            dataSetting.questProgessDataBase = JsonUtility.FromJson<QuestProgessDataBase>(processDataBase);

        if (!string.IsNullOrEmpty(achivesmentProgessList))
            dataSetting.achivesmentProgessList = JsonUtility.FromJson<AchivesmentProgessList>(achivesmentProgessList);

        if (!string.IsNullOrEmpty(fishTankBase))
            dataSetting.fishTankBase = JsonUtility.FromJson<FishTankBase>(fishTankBase);
    }


}
