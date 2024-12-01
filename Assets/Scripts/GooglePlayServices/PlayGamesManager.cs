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
    
    
    public void ReportAchievement(string achievementId, float progress)
    {
        PlayGamesPlatform.Instance.ReportProgress(achievementId, progress, (result) =>
        {
            if (result)
            {
                Debug.Log($"Achievement {achievementId} progress reported");
            }
            else
            {
                Debug.LogWarning($"Failed to report progress for {achievementId}");
            }
        });
    }
    
    public void ShowAchievementUI()
    {
        if (connectedToGamePlay)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
           
        }
    }
    
    
    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            connectedToGamePlay = true;
        } else
        {
            connectedToGamePlay = false;
        }
    }
   
}
