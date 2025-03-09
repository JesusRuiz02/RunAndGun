using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class PlayGamesManager : MonoBehaviour
{
    private static PlayGamesManager instance;
    [HideInInspector] public string oneHundredAchievement = "CgkIs5ii8MkUEAIQBQ";
    [HideInInspector] public string twoHundredAchievement = "CgkIs5ii8MkUEAIQAw";
    [HideInInspector] public string threeHundredAchievement = "CgkIs5ii8MkUEAIQBg";
    [HideInInspector] public string fiveHundredAchievement = "CgkIs5ii8MkUEAIQBw";
    [HideInInspector] public string perseveranceAchievement = "CgkIs5ii8MkUEAIQCQ";
    [HideInInspector] public string firstTimeAchievement = "CgkIs5ii8MkUEAIQBA";
    
    public static PlayGamesManager GetInstance()
    {
        return instance;
    }
    public bool connectedToGamePlay;
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
    }
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SignIn();
      
    }
    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void ShowLeaderboard()
    {
        if (!connectedToGamePlay) SignIn();
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
    
    private void ReportAchievement(string achievementId)
    {
        if (!connectedToGamePlay)
        {
            Debug.LogWarning("User not signed in, cannot report achievement.");
            return;
        }

        PlayGamesPlatform.Instance.ReportProgress(achievementId, 100f, (result) =>
        {
            if (result)
            {
                Debug.Log("Progress reported successfully");
            }
            else
            {
                Debug.LogWarning("Failed to report progress");
            }
        });
    }

    public void FirstTimeAchievement() => ReportAchievement(firstTimeAchievement);
    public void OneHundredAchievement() => ReportAchievement(oneHundredAchievement);
    public void TwoHundredAchievement() => ReportAchievement(twoHundredAchievement);
    public void ThreeHundredAchievement() => ReportAchievement(threeHundredAchievement);
    public void FiveHundredAchievement() => ReportAchievement(fiveHundredAchievement);
    
    public void PerseveranceAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(perseveranceAchievement, 1, (result) =>
        {
            if (result)
            {
                Debug.Log("progressReported");
            }
            else
            {
                Debug.LogWarning("Failed to reported");
            }
        });
    }

    public void ShowAchievementUI()
    {
        if (!connectedToGamePlay) SignIn();
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    
    
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            Debug.Log("Google Play Games login successful.");
            connectedToGamePlay = true;
        }
        else
        {
            Debug.LogWarning("Google Play Games login failed.");
            connectedToGamePlay = false;
        }
    }
    
   
}
