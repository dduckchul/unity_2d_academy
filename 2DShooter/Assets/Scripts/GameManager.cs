using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    private float _score;
    private bool _isGameOver;
    private List<Observer> _observers;

    public event Action<float> OnScoreChanged; // 점수 변화 이벤트
    public event Action OnGameOver; // 게임 오버 진행되면 알아서 수행할 메서드 등록할 액션

    public void TriggerGameOver()
    {
        _isGameOver = true;
        OnGameOver?.Invoke(); // 게임 오버 등록된 애들한테 전부 수행 알림 쏴줌
    }

    public static GameManager Instance // 프로퍼티로 만들었다, 싱글톤 패턴
    {
        get; private set;
    }

    public float Score
    {
        get { return _score; }
    }

    public void SetIsGameOver()
    {
        _isGameOver = true;
        NotifyObserver();
        _observers = new List<Observer>();
    }

    public bool GetIsGameOver()
    {
        return _isGameOver;
    }

    // 최초 실행
    private void Awake()
    {
        if (Instance == null) // 맨 처음 호출이라 인스턴스가 비어있을 땐?
        {
            Instance = this; // 이 스크립트 컴포넌트를 instance 껍데기에 담아라
            DontDestroyOnLoad(gameObject); // 그리고 이 오브젝트 파괴하지 말아주세용
            
            _score = 0;
            _isGameOver = false;
            _observers = new List<Observer>();            
        } 
        else if (Instance != this) // 안전망
        {
            Destroy(gameObject); // 그렇지 않다면 파괴
        }
    }
    
    public void AddScore()
    {
        _score += 100;
        OnScoreChanged?.Invoke(_score);
        NotifyObserver();
    }

    public void RegisterObserver(Observer o)
    {
        _observers.Add(o);
    }

    public void RemoveObserver(Observer o)
    {
        _observers.Remove(o);
    }

    public void NotifyObserver()
    {
        foreach (Observer o in _observers)
        {
            o.update(this);
        }
    }
}
