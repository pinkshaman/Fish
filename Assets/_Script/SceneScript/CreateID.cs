
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.U2D;
public class CreateID : MonoBehaviour
{
    public Button createButton;
    public InputField inputName;
    public LoadSaveData saveLoadData;

    public PlayerData playerData;
    public Text Message;
    public Button xButton;
    public SceneManagers loadScene;

    public void Awake()
    {
        SignIn();
    }
    public void Start()
    {
        createButton.onClick.AddListener(OnCreateButtonClick);
        LoadplayerData();
    }
    public void LoadplayerData()
    {
        saveLoadData.LoadData();
        playerData = saveLoadData.playerData;

        if (playerData.name == string.Empty)
        {
            Message.text = "Input Player Name";
        }
        else
        {
            loadScene.LoadMainScene();
            Message.text = $"Login Successfull{playerData.name} ";
        }
    }

    public void OnXButtonClick()
    {
        Application.Quit();
        Debug.Log("Application is Quitting");
    }

    public void OnCreateButtonClick()
    {
        string playerName = inputName != null ? inputName.text : string.Empty;
        if (!string.IsNullOrEmpty(playerName))
        {
            CreatePlayerName(playerName);
        }
        else
        {
            Debug.LogWarning("input Name is blank!");
            Message.text = " Input Name is blank!";
        }
    }
    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            //string id = PlayGamesPlatform.Instance.GetUserId();
            //string name = PlayGamesPlatform.Instance.GetUserDisplayName();          
            //PlayerData newPlayerData = new PlayerData(id,name,1,0,null,0,0,0);
            GameManager.Instance.LoadData();

            Message.text = $"Login with Google Succesful";          
        }
        else
        {
            Message.text = "Login Fail!";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
   

    public void CreatePlayerName(string playerName)
    {
        string id = PlayGamesPlatform.Instance.GetUserId();
        

        PlayerData newPlayerData = new PlayerData(id, playerName, 1, 0, null, 0, 0, 0);
        saveLoadData.SavesData(newPlayerData);
        Debug.Log($"Successfully! Your name: {playerName}");
        loadScene.LoadChooseFish();

    }
  
   
}


