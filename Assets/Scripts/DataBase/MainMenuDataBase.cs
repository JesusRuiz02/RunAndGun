using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDataBase : MonoBehaviour
{
    [SerializeField] private GameObject Achivements;
    [SerializeField] private GameObject Leaderboard;
    [SerializeField] private GameObject fondoLogIn;
    [SerializeField] private GameObject signIn;
    [SerializeField] private GameObject logIn;
    [SerializeField] private Button profileButton;
    [SerializeField] private Image profileImage;
    [SerializeField] private Sprite LogOut;
    [SerializeField] private Sprite LogInS;
    [SerializeField] private TMP_InputField logInPassword;
    [SerializeField] private TMP_InputField sigInPassword;
    [SerializeField] private TMP_InputField sigInUsername;
    [SerializeField] private TMP_InputField logInUsername;
    
    private void Start()
    {
        if (MySqlConnection.GetInstance().userInfo.isLogin)
        {
            profileImage.sprite = LogInS;
        }
        else
        {
            profileImage.sprite = LogOut;
        }
    }

    public void StartLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }
    
    public void StartAccuracyBoard()
    {
        StartCoroutine(GetAccuracyBoard());
    }

    public void ProfileButton()
    {
        if (MySqlConnection.GetInstance().userInfo.isLogin)
        {
            MySqlConnection.GetInstance().LogOut();
            profileImage.sprite = LogOut;
        }
        else
        {
            fondoLogIn.SetActive(true);
        }
    }

    public void SignIn()
    {
        StartCoroutine(StartSignIn());
    }

    public void LogIn()
    {
        StartCoroutine(StartLogIn());
    }

    private IEnumerator StartSignIn()
    {
        yield return StartCoroutine(MySqlConnection.GetInstance().StartSignIn(sigInUsername.text, sigInPassword.text));
        sigInUsername.text = String.Empty;
        sigInPassword.text = string.Empty;
        signIn.SetActive(false);
        logIn.SetActive(true);
        fondoLogIn.SetActive(false);
        if (MySqlConnection.GetInstance().userInfo.isLogin)
        {
            profileImage.sprite = LogInS;
        }
        else
        {
            profileImage.sprite = LogOut;
        }
    }

    private IEnumerator StartLogIn()
    {
        yield return StartCoroutine(MySqlConnection.GetInstance().StartLogin(logInUsername.text, logInPassword.text));
        logInPassword.text = String.Empty;
        logInUsername.text = string.Empty;
        fondoLogIn.SetActive(false);
        if (MySqlConnection.GetInstance().userInfo.isLogin)
        {
            profileImage.sprite = LogInS;
        }
        else
        {
            profileImage.sprite = LogOut;
        }
    }

    public IEnumerator GetLeaderboard()
    {
        yield return StartCoroutine(MySqlConnection.GetInstance().GetTop10HighScores());
        List<ChangeText> TextGameObject = new List<ChangeText>();
        Leaderboard.SetActive(true);
        TextGameObject.AddRange(Leaderboard.GetComponentsInChildren<ChangeText>());
        for (int i = 0; i < MySqlConnection.GetInstance().highScores.Count; i++)
        {
            TextGameObject[i].ChangeUser(MySqlConnection.GetInstance().highScores[i].User);
            TextGameObject[i].ChangeValue(MySqlConnection.GetInstance().highScores[i].Score);
        }
        
    }

    public IEnumerator GetAccuracyBoard()
    {
        yield return StartCoroutine(MySqlConnection.GetInstance().GetTop10Accuracy());
        List<ChangeText> TextGameObject = new List<ChangeText>();
        Achivements.SetActive(true);
        TextGameObject.AddRange(Achivements.GetComponentsInChildren<ChangeText>());
        for (int i = 0; i < MySqlConnection.GetInstance().AccuracyBoards.Count; i++)
        {
            TextGameObject[i].ChangeUser(MySqlConnection.GetInstance().AccuracyBoards[i].Nickname);
            TextGameObject[i].ChangeValue(MySqlConnection.GetInstance().AccuracyBoards[i].AverageAccuracy);
        }
    }

}
