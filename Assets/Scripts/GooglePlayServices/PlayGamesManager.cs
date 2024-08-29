using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine.UI;

public class PlayGamesManager : MonoBehaviour
{
    private static PlayGamesManager instance;
    [HideInInspector] public string oneHundredAchievement = "CgkIs5ii8MkUEAIQBQ";
    [HideInInspector] public string twoHundredAchievement = "CgkIs5ii8MkUEAIQAw";
    [HideInInspector] public string threeHundredAchievement = "CgkIs5ii8MkUEAIQBg";
    [HideInInspector] public string fiveHundredAchievement = "CgkIs5ii8MkUEAIQBw";
    [HideInInspector] public string perseveranceAchievement = "CgkIs5ii8MkUEAIQCQ";
    [HideInInspector] public string firstTimeAchievement = "CgkIs5ii8MkUEAIQBA";
    public Sprite noConnection;
    public Image leaderboardUI;
    public Image achievementsUI;
    
    public static PlayGamesManager GetInstance()
    {
        return instance;
    }
 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
           Destroy(gameObject);
        }
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SignIn();
    }

    private void ConnectionCheck()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            leaderboardUI.sprite = noConnection;
            achievementsUI.sprite = noConnection;
        }
    }
    void Start()
    {
        ConnectionCheck();
    }
    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void ShowLeaderboard()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.unityLogger.Log("Debugging", "No se mostró el marcador porque no está autenticado.");
            if (leaderboardUI != null)
            {
                leaderboardUI.sprite = noConnection;
            }
            SignIn(); // Llamar a la autenticación nuevamente si es necesario
        }
        else
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
            Debug.unityLogger.Log("Debugging", "Se mostró el marcador.");
        }
    }
    
    public void ReportAchievementProgress(string achievementId, float progress)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.ReportProgress(achievementId, progress, (result) =>
            {
                Debug.unityLogger.Log("Debugging", result ? "Se reportó el logro correctamente." : "Error al reportar el logro.");
            });
        }
        else
        {
            Debug.unityLogger.Log("Debugging", "No está autenticado para reportar el logro.");
        }
    }
    

    public void ShowAchievementUI()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.unityLogger.Log("Debugging", "No se mostraron los logros porque no está autenticado.");
            if (achievementsUI != null)
            {
                achievementsUI.sprite = noConnection;
            }
            SignIn(); // Intentar autenticarse nuevamente
        }
        else
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
            Debug.unityLogger.Log("Debugging", "Se mostraron los logros.");
        }
    }
    
    
    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            Debug.unityLogger.Log("Debugging", "se conecto"); 
        } else
        {
            if (leaderboardUI != null && achievementsUI != null)
            {
                leaderboardUI.sprite = noConnection;
                achievementsUI.sprite = noConnection;
            }
            Debug.unityLogger.Log("Debugging", "No se conecto"); 
        }
    }
   
}
