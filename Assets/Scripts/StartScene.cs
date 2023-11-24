using System;
using DG.Tweening;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public AudioClip _lobbyMusic;
    [SerializeField] private float _fadeTime;
    [SerializeField] private RectTransform playRectTransform;
    [SerializeField] private CanvasGroup _canvasPlayGroup;
    [SerializeField] private RectTransform logoRectTransform;
    [SerializeField] private RectTransform settingsRectTransform;
    [SerializeField] private RectTransform creditsRectTransform;

    

    void Start()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlayMusic(_lobbyMusic);
       InvokeRepeating("LogoSpin", 3, 10f);
       Invoke("StartAnimation", 0.5f);
       Invoke("BalloonAnimation", 1.7f);
    }

    private void OnDestroy()
    {
        CancelInvoke("LogoSpin");
    }


    private void StartAnimation()
    {
        _canvasPlayGroup.alpha = 0f;
        playRectTransform.transform.localPosition = new Vector3(0f, 500f, 0f);
        playRectTransform.DOAnchorPos(new Vector2(0f, 100f), 1.0f, false).SetEase(Ease.Linear).SetUpdate(true);
        _canvasPlayGroup.DOFade(1, _fadeTime).SetUpdate(true);
       settingsRectTransform.transform.localPosition = new Vector3(-500f, 75f, 0f);
       settingsRectTransform.DOAnchorPos(new Vector2(-250f, 75f), 0.5f, false).SetEase(Ease.Linear).SetUpdate(true);
       creditsRectTransform.transform.localPosition = new Vector3(500f, 75f, 0f);
       creditsRectTransform.DOAnchorPos(new Vector2(250f, 75f), 0.5f, false).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void BalloonAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isActiveAndEnabled)
        {
            sequence.Append(playRectTransform.DOAnchorPos(new Vector2(0, 100f), 1f, false).SetEase(Ease.Linear)
                .SetUpdate(true));
            sequence.Append(playRectTransform.DOAnchorPos(new Vector2(0, 105f), 1f, false).SetEase(Ease.Linear)
                .SetUpdate(true));
            sequence.AppendCallback(BalloonAnimation);
        }
        
    }

    private void LogoSpin()
    {
        logoRectTransform.DORotate(new Vector3(0,0,360),1f, RotateMode.FastBeyond360);
    }
    

}
