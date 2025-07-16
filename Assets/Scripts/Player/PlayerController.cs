using System;
using System.Collections;
using TMPro;
using DG.Tweening;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private int scoreToReset = 0;
    [SerializeField] private UImanager _uImanager;
    public static PlayerController instance;
    private string leaderboardId = "CgkIs5ii8MkUEAIQAg";
    [SerializeField] private GameObject _invincibleCanvas;
    [SerializeField]  private GameObject _canvasPause;
    [SerializeField] private AudioClip BulletSfx;
    [SerializeField] private AudioClip _WallCrash;
    private float _shots = default;
    [SerializeField] private AudioClip _popSfx = default;
    [SerializeField] private AudioClip _song = default;
    [SerializeField] private TextMeshProUGUI _textAccuracy;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwUpForce;
    [SerializeField] private float score = default;
    [SerializeField] private TextMeshProUGUI _scoreText = default;
    private HealthController _healthController = default;
    public float Score => score;
    private bool _isInmune = default;

    [Header("Obstacle Values")]
    private int _randomObstacleSpawnValue = 6;
    private int _obstacleValueScore = 0;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _scoreText.text = score.ToString();
    }

    public int GetScoreToReset()
    {
        return scoreToReset;
    }

    public void StartResetScore()
    {
        scoreToReset = 0;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusic(_song);
        _healthController = gameObject.GetComponent<HealthController>();
    }
    

    public void AddScore(float scoreToAdd)
    {
        if (PlayGamesManager.GetInstance().connectedToGamePlay)
        {
            PlayGamesManager.GetInstance().FirstTimeAchievement();
        }
        GameManager.instance.SpawnObstacleNPowerUps(35f, OBSTACLE_TYPE.HealPowerUp);
        GameManager.instance.SpawnObstacleNPowerUps(55f, OBSTACLE_TYPE.ExtraLifePowerUp);

        score += scoreToAdd;
        _obstacleValueScore++;
        scoreToReset += (int)scoreToAdd;
        _scoreText.text = score.ToString();
        if (score >= 100)
        {
            if (PlayGamesManager.GetInstance().connectedToGamePlay)
            {
                PlayGamesManager.GetInstance().OneHundredAchievement();
            }
        }
        if (score >= 200 && PlayGamesManager.GetInstance().connectedToGamePlay)
        {
            PlayGamesManager.GetInstance().TwoHundredAchievement();
        }
        if (score >= 300 && PlayGamesManager.GetInstance().connectedToGamePlay)
        {
            PlayGamesManager.GetInstance().ThreeHundredAchievement();
        }
        if (score >= 500 && PlayGamesManager.GetInstance().connectedToGamePlay)
        {
            PlayGamesManager.GetInstance().FiveHundredAchievement();
        }
        if(  _obstacleValueScore >= _randomObstacleSpawnValue)
        {
            _randomObstacleSpawnValue = UnityEngine.Random.Range(6, 12);
            _obstacleValueScore = 0;
            SpawnerBalloon.instance.GetRandomObstacleBalloon();
        }
        SpawnerBalloon.instance.GetPooledObject(OBSTACLE_TYPE.Balloon);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            Health();
            AudioManager.instance.SetSound(SOUND_TYPE.POP_BALLLOON);
            other.gameObject.SetActive(false);
            Camera.main.DOShakePosition(0.25f, new Vector3(0, 1, 0), 80, 90f, true);
        }
        if (other.CompareTag("Wall"))
        {
              
                other.transform.parent.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                Camera.main.transform.DOMoveY(0.6f, 0.7f, true).SetEase(Ease.OutElastic).SetUpdate(true);
                Camera.main.transform.DORotate(new Vector3(-90, 0, 0), 0.4f).SetUpdate(true).SetEase(Ease.Flash);
                AudioManager.instance.SetSound(SOUND_TYPE.WALL_CRASHED);
                Camera.main.DOShakePosition(0.6f, new Vector3(2, 0, 0), 80, 90f, true).SetDelay(0.5f).SetUpdate(true);
                GameOver(); 
            
        }
    }


    public void GameOver()
    {
        _uImanager.GameOverFadeIn();
        float highScore = PlayerPrefs.GetFloat("highscore", score);
        float accuracy = (score / _shots ) * 100;
        double _accuracy = Math.Round(accuracy, 2);
        _accuracy = score == 0 ? 0 : _accuracy; //Para que la division no de infinito en caso de ser cero
       /* if (MySqlConnection.GetInstance().userInfo.isLogin)
        {
            MySqlConnection.GetInstance().SendGameData((int)score,accuracy);
        }*/
        if (highScore < score)
        {
            PlayerPrefs.SetFloat("highScore", score);
        }
        if (PlayGamesManager.GetInstance().connectedToGamePlay)
        {
           PlayGamesManager.GetInstance().PerseveranceAchievement(); 
        }
        _textScore.text = "Highscore : " + highScore;
        _textAccuracy.text = "Accuracy : " + _accuracy + "%";
        AddScoreToLeaderBoard(leaderboardId, (int)score);
        _canvasPause.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void AddScoreToLeaderBoard(string leaderboard, int points)
    {
        if (PlayGamesManager.GetInstance().connectedToGamePlay)
        {
            Social.ReportScore(points, leaderboard, (bool success) => {
                if (success)
                {
                    LoadPlayerScore(leaderboardId);
                }
                // handle success or failure
            });
        }
    }
    private void LoadPlayerScore(string leaderboardId)
    {
        PlayGamesPlatform.Instance.LoadScores(
            leaderboardId,
            LeaderboardStart.PlayerCentered,
            1,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) =>
            {
                if (data.Valid)
                {
                    IScore playerScore = data.PlayerScore;
                    if (playerScore != null)
                    {
                        if (playerScore.rank == -1)
                        {
                            _uImanager.TurnOffLeaderboard();
                        }
                    }
                    else
                    {
                        _uImanager.TurnOffLeaderboard();
                
                    }
                }
                else
                {
                    _uImanager.TurnOffLeaderboard();
                }
            });
    }

    public void Health()
    {
        if (!_isInmune)
        {
            _healthController.ReduceHealth();
        }
    }
    
    public void Shoot(Vector3 Targetposition, GameObject projectile)
    {
        _shots++;
        Vector3 forceDirection = transform.forward;
    
        forceDirection = (Targetposition - transform.position + new Vector3(0, 0.25f,-1)).normalized;

        Vector3 forceToAdd = forceDirection * _throwForce + transform.up * _throwUpForce;

        projectile.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);

        AudioManager.instance.SetSound(SOUND_TYPE.DART_THROWED);
    }

    public void CallCoroutine()
    {
        StartCoroutine(PowerUp());
    }

    public void callHealPW()
    {
        _healthController.Heal();
    }

    public void callExtraLife()
    {
        _healthController.AddExtraLife();
    }

    public IEnumerator PowerUp()
    {
        _isInmune = true;
        _invincibleCanvas.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        _invincibleCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _invincibleCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        _invincibleCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _invincibleCanvas.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        _invincibleCanvas.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _invincibleCanvas.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        _invincibleCanvas.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _invincibleCanvas.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _invincibleCanvas.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _invincibleCanvas.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _invincibleCanvas.SetActive(false);
        _isInmune = false;
    }
}
