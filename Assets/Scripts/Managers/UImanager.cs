using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _rectAudioTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private CanvasGroup _canvasAudioGroup;
    [SerializeField] private bool _isPause = false;

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TileMovemenent");
    }


    public void PanelFadeIn()
    {
        TooglePause();
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutElastic).SetUpdate(true);
        _canvasGroup.DOFade(1, _fadeTime).SetUpdate(true);
    }

    private void TooglePause()
    {
        _isPause = !_isPause;
        int scaleTime = _isPause ? 0 : 1;
        Time.timeScale = scaleTime;
    }
    
    public void PanelFadeOut(bool isExitButton)
    {
        _canvasGroup.alpha = 1f;
        var sequence = DOTween.Sequence();
        _rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
        if (isExitButton)
        {
            sequence.Append(_canvasGroup.DOFade(0, _fadeTime).SetUpdate(true));
            sequence.AppendCallback(TooglePause).SetUpdate(true);
        }
        else
        {
            sequence.Append(_canvasGroup.DOFade(0, 0.3f).SetUpdate(true));
            sequence.AppendCallback(PanelAudioFadeIn).SetUpdate(true);
        }
      
    }

    public void PanelAudioFadeIn()
    {
        _canvasAudioGroup.alpha = 0f;
        _rectAudioTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        _rectAudioTransform.DOAnchorPos(new Vector2(0f, 0f), 0.4f, false).SetEase(Ease.OutElastic).SetUpdate(true);
        _canvasAudioGroup.DOFade(1, _fadeTime).SetUpdate(true);
    }

    public void PanelAudioFadeOut()
    {
        _canvasAudioGroup.alpha = 1f;
        var sequence = DOTween.Sequence();
        _rectAudioTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        _rectAudioTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
        sequence.Append(_canvasGroup.DOFade(0, _fadeTime).SetUpdate(true));
        sequence.AppendCallback(TooglePause).SetUpdate(true);
    }
}
