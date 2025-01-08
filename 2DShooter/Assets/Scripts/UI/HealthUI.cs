using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour, Observer
{
    private Image _hpImage;
    private GameObject _playerObject;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        _hpImage = GetComponent<Image>();
        _playerObject = GameObject.FindWithTag("Player");
        _playerController = _playerObject.GetComponent<PlayerController>();
        _playerController.RegisterObserver(this);

        _playerController.OnHealthChanged += UpdateHealthUI;
        
        Color tempColor = new Color(0, 1, 0, 1);
        _hpImage.color = tempColor;
    }

    // PlayerController 속에 있는 이벤트에 구독시킬 메서드
    public void UpdateHealthUI(int currentHp, int MaxHp)
    {
        float toFill = _playerController.Hp / _playerController.MaxHp;
        
        _hpImage.color = new Color(1 - toFill, toFill, 0, 1);;
        _hpImage.fillAmount = toFill;
    }

    private void OnDestroy()
    {
        if (_playerController != null)
        {
            _playerController.OnHealthChanged -= UpdateHealthUI;
        }
    }
    
    public void update(GameManager gameManager)
    {
        // 머 안할거임
    }

    public void update(PlayerController playerController)
    {
        float toFill = playerController.Hp / playerController.MaxHp;
        
        _hpImage.color = new Color(1 - toFill, toFill, 0, 1);;
        _hpImage.fillAmount = toFill;
    }
}
