using UnityEngine;
using DG.Tweening;

public class UImanager : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _isPause = false;


    public void PanelFadeIn()
    {
        
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        TooglePause();
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutElastic).SetUpdate(true);
        _canvasGroup.DOFade(1, _fadeTime).SetUpdate(true);
        
    }

    public void TooglePause()
    {
        _isPause = !_isPause;
        Debug.Log(_isPause);
        Time.timeScale = _isPause ? 0 : 1;
    }
    public void PanelFadeOut()
    {
        _canvasGroup.alpha = 1f;
        var sequence = DOTween.Sequence();
        _rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
        sequence.Append(_canvasGroup.DOFade(0, _fadeTime).SetUpdate(true));
        Invoke("TooglePause", _fadeTime);
    }
}
