using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _isPause = false;

    public void RestartGame()
    {
        SceneManager.LoadScene("TileMovemenent");
        Time.timeScale = 1;
    }


    public void PanelFadeIn()
    {
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        TooglePause();
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutElastic).SetUpdate(true);
        _canvasGroup.DOFade(1, _fadeTime).SetUpdate(true);
    }

    private void TooglePause()
    {
        _isPause = !_isPause;
        int scaleTime = _isPause ? 0 : 1;
        Time.timeScale = scaleTime;
    }
    
    public void PanelFadeOut()
    {
        _canvasGroup.alpha = 1f;
        var sequence = DOTween.Sequence();
        _rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
        sequence.Append(_canvasGroup.DOFade(0, _fadeTime).SetUpdate(true));
        sequence.AppendCallback(TooglePause).SetUpdate(true);
    }
}
