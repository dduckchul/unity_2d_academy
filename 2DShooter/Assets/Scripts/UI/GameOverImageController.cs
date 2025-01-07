using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverImageController : MonoBehaviour, Observer
{
    private Image gameOverImage;
    private Color toChange;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverImage = GetComponent<Image>();
        toChange = new Color(0.5f, 0, 0, 0);
        GameManager.Instance.RegisterObserver(this);
    }

    public void update(GameManager gameManager)
    {
        if (GameManager.Instance.GetIsGameOver())
        {
            // 이렇게 바꾸면 바뀌는듯한 애니메이션을 할수는 없을듯?
            toChange.a = 1f;
            gameOverImage.color = toChange;
        }
    }
    
    public void update(PlayerControler playerController)
    {
        // throw new System.NotImplementedException();
    }
}
