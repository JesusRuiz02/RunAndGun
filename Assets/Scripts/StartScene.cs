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
        AudioManager.instance.PlayMusic(_lobbyMusic);
       InvokeRepeating("LogoSpin", 3, 10f);
       Invoke("StartAnimation", 0.5f);
    }

    private void OnDestroy()
    {
        CancelInvoke("LogoSpin");
    }


    private void StartAnimation()
    {
        _canvasPlayGroup.alpha = 0f;
        playRectTransform.transform.localPosition = new Vector3(0f, 500f, 0f);
        playRectTransform.DOAnchorPos(new Vector2(0f, -96f), 2.0f, false).SetEase(Ease.Linear).SetUpdate(true);
        _canvasPlayGroup.DOFade(1, _fadeTime).SetUpdate(true);
       settingsRectTransform.transform.localPosition = new Vector3(-500f, -119f, 0f);
       settingsRectTransform.DOAnchorPos(new Vector2(-200f, -119f), 1.0f, false).SetEase(Ease.Linear).SetUpdate(true);
       creditsRectTransform.transform.localPosition = new Vector3(500f, -119f, 0f);
       creditsRectTransform.DOAnchorPos(new Vector2(200f, -119f), 1.0f, false).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void LogoSpin()
    {
        logoRectTransform.DORotate(new Vector3(0,0,360),1f, RotateMode.FastBeyond360);
    }
    

}
