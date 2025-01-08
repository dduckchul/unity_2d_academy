using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour, Observer
{
    private Text scoreTextGameObject;
    
    void Start()
    {
        scoreTextGameObject = GetComponent<Text>();
        GameManager.Instance.OnScoreChanged += UpdateScoreUi;
        GameManager.Instance.RegisterObserver(this);
    }

    void UpdateScoreUi(float newScore)
    {
        string toShow = "Score : " + newScore.ToString("000");
        scoreTextGameObject.text = toShow;        
    }

    private void OnDestroy()
    {   
        // ScoreUI의 Update 구독 해지
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScoreUi;
        }
    }

    public void update(GameManager gameManager)
    {
        float tempScore = GameManager.Instance.Score;
        string toShow = "Score : " + tempScore.ToString("000");
        scoreTextGameObject.text = toShow;
    }

    public void update(PlayerController playerController)
    {
        // throw new System.NotImplementedException();
    }
    
}
