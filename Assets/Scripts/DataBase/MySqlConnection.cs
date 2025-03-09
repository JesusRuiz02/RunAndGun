using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
[System.Serializable]
public class Users
{
    public string Score;
    public string User;
}
[System.Serializable]
public class AccuracyBoard
{
    public string Nickname;
    public string AverageAccuracy;
}

[System.Serializable]
public class UserInfo
{
    public string User_ID;
    public int HighScore;
    public bool isLogin;
}

public class GameInfo
{
    public string UserID;
    public string Score;
    public string Accuracy;
}


public class MySqlConnection : MonoBehaviour
{
    public List<Users> highScores = new List<Users>();
    public List<AccuracyBoard> AccuracyBoards = new List<AccuracyBoard>();
    public UserInfo userInfo = new UserInfo();
    
    private static MySqlConnection instance;
    public static MySqlConnection GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        userInfo.isLogin = PlayerPrefs.GetInt("Login", 0) == 1; 
        if (userInfo.isLogin)
        {
            userInfo.User_ID = PlayerPrefs.GetString("UserID");
            string highScore = PlayerPrefs.GetString("HighScore");
            userInfo.HighScore = int.Parse(highScore);
        }
        else
        {
            LogOut();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Login", userInfo.isLogin ? 1 : 0); 
        PlayerPrefs.SetString("UserID", userInfo.User_ID); 
        PlayerPrefs.SetString("HighScore", userInfo.HighScore.ToString());
        PlayerPrefs.Save();
    }
    

    public void SendGameData(int score, float accuracy)
    {
        if (score > userInfo.HighScore)
        {
            print("va actualizar el highscore");
            userInfo.HighScore = score;
            StartCoroutine(StartSendingGameData(accuracy, score,true));
        }
        else
        {
            StartCoroutine(StartSendingGameData(accuracy, score,false));
        }
    }
    public IEnumerator StartSendingGameData(float accuracy, int score, bool isHighScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("Score", score);
        form.AddField("Accuracy", accuracy.ToString());
        form.AddField("User_ID", userInfo.User_ID);
        form.AddField("IsHighScore", isHighScore.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://runandgun.com.mx/Database/GameLog.php", form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la conexión: " + www.error);
        }
        else
        {
            SaveData();
            Debug.Log("Respuesta: " + www.downloadHandler.text);
        }
        
    }

    private void OnDisable()
    {
        SaveData();
    }

    public void LogOut()
    {
        userInfo.User_ID = String.Empty;
        userInfo.HighScore = 0;
        userInfo.isLogin = false;
        SaveData();
    }
    
    
   /* public void LogIn(string user, string password)
    {
        StartCoroutine(StartLogin(user, password));
    }

    public void SignIn(string user, string password)
    {
        StartCoroutine(StartSignIn(user,password));
    }*/
    public IEnumerator StartSignIn(string user, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("https://runandgun.com.mx/Database/signIn.php", form);
        yield return www.SendWebRequest();
        
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la conexión: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            Debug.Log("Respuesta: " + jsonResponse);

            if (jsonResponse == "false")
            {
                Debug.LogError("Error: Usuario ya existente o fallo en el registro");
            }
            else
            { 
                userInfo = JsonConvert.DeserializeObject<UserInfo>(jsonResponse);
                
                userInfo.isLogin = true;

                Debug.Log($"Registro exitoso: User_ID = {userInfo.User_ID}, HighScore = {userInfo.HighScore}");
            }
        }
    }

    public IEnumerator StartLogin(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        UnityWebRequest www = UnityWebRequest.Post("https://runandgun.com.mx/Database/LogIn.php", form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la conexión: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            Debug.Log("Respuesta: " + jsonResponse);
            
             userInfo = JsonConvert.DeserializeObject<UserInfo>(jsonResponse);

            if (userInfo.isLogin)
            {
                Debug.Log($"Login exitoso: User_ID = {userInfo.User_ID}, HighScore = {userInfo.HighScore}");
            }
            else
            {
                Debug.LogError("Error: Usuario o contraseña incorrectos.");
            }
        }
        
    }
    

    public IEnumerator GetTop10HighScores()
    {
        
        string url = "https://runandgun.com.mx/Database/getData.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la conexión: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            highScores  = JsonConvert.DeserializeObject<List<Users>>(jsonResponse);
            foreach (Users s in highScores)
            {
                Debug.Log(s.User + " " + s.Score);
            }
        }
    }
    
    public IEnumerator GetTop10Accuracy()
    {
        string url = "https://runandgun.com.mx/Database/getAccuracyLeaderboard.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la conexión: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            AccuracyBoards  = JsonConvert.DeserializeObject<List<AccuracyBoard>>(jsonResponse);
            foreach (AccuracyBoard s in AccuracyBoards)
            {
                Debug.Log(s.Nickname + " " + s.AverageAccuracy);
            }
        }
    }
    
}


    


