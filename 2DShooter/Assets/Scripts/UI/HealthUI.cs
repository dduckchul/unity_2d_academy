using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour, Observer
{
    private Image _hpImage;
    private GameObject _playerObject;
    private PlayerControler _playerControler;
    private float _playerMaxHp;
    
    // Start is called before the first frame update
    void Start()
    {
        _hpImage = GetComponent<Image>();
        _playerObject = GameObject.FindWithTag("Player");
        _playerControler = _playerObject.GetComponent<PlayerControler>();
        _playerMaxHp = _playerControler.MaxHp;
        _playerControler.RegisterObserver(this);
        
        Color tempColor = new Color(0, 1, 0, 1);
        _hpImage.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void update(GameManager gameManager)
    {
        // 머 안할거임
    }

    public void update(PlayerControler playerController)
    {
        float toFill = _playerControler.Hp / _playerMaxHp;
        
        _hpImage.color = new Color(1 - toFill, toFill, 0, 1);;
        _hpImage.fillAmount = toFill;
    }
}
