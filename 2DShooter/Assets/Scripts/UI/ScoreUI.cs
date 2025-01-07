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
        GameManager.Instance.RegisterObserver(this);
    }

    public void update(GameManager gameManager)
    {
        float tempScore = GameManager.Instance.Score;
        string toShow = "Score : " + tempScore.ToString("000");
        scoreTextGameObject.text = toShow;
    }

    public void update(PlayerControler playerController)
    {
        // throw new System.NotImplementedException();
    }
    
}
