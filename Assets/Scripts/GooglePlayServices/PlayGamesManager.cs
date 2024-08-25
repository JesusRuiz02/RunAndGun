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
        SignIn();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void ShowLeaderboard()
    {
        if (!connectedToGamePlay) SignIn();
        Social.ShowLeaderboardUI();
    }

    public void FirstTimeAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(firstTimeAchievement, 100f, (result) =>
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
    
    
    public void OneHundredAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(oneHundredAchievement, 100f, (result) =>
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
    public void TwoHundredAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(twoHundredAchievement, 100f, (result) =>
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
    
    public void ThreeHundredAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(threeHundredAchievement, 100f, (result) =>
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
    
    public void FiveHundredAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(fiveHundredAchievement, 100f, (result) =>
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
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    
    
    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            // Continue with Play Games Services
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();
            connectedToGamePlay = true;
        } else
        {
            connectedToGamePlay = false;
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
   
}
