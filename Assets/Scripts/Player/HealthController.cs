using System;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    [SerializeField] private int _playerHealth = default;
    [SerializeField] private int _healthNumber = default;
    [SerializeField] private Image[] _lifeContainer = default;
    [SerializeField] private Sprite _fullContainer = default;
    [SerializeField] private Sprite _emptyContainer = default;
    [SerializeField] private int _touchDamage = default;
    private PlayerController _playerController = default;

    private void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
    }

    private void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (_playerHealth <= 0 )
        {
            _playerController.GameOver();
        }
        if (_playerHealth > _healthNumber)
        {
            _playerHealth = _healthNumber;
        }
        for (int i = 0; i < _lifeContainer.Length; i++)
        {
            if (i < _playerHealth)
            {
                _lifeContainer[i].sprite = _fullContainer;
            }
            else
            {
                _lifeContainer[i].sprite = _emptyContainer;
            }

            if (i < _healthNumber)
            {
                _lifeContainer[i].enabled = true;
            }
            else
            {
                _lifeContainer[i].enabled = false;
            }
        }
    }

    public void ReduceHealth()
    {
        _playerHealth -= _touchDamage;
        UpdateHealth();
    }

    public void Heal()
    {
        _playerHealth += _touchDamage;
        if (_playerHealth > _healthNumber)
        {
            _playerHealth = _healthNumber;
        }
        UpdateHealth();
    }

    public void AddExtraLife()
    {
        _healthNumber += 1;
        if (_healthNumber > 5)
        {
            _healthNumber = 5;
        }
        UpdateHealth();
    }
}