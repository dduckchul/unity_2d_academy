using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour, Observer
{
    public GameObject enemyPrefab; // 적 기체 프리팹 설계도
    private int coolTime;
    private int spawnSpeed;
    private Vector3 tempPosition;
    private bool isPlayerDead;
    
    void Start()
    {
        coolTime = 0;
        spawnSpeed = 3000;
        tempPosition = Vector3.zero;
        GameManager.Instance.RegisterObserver(this);
    }
    
    void Update()
    {
        coolTime++;
        if (coolTime > spawnSpeed)
        {
            tempPosition.x = Random.Range(-2, 2.0f);
            tempPosition.y = Random.Range(0, 3.0f);
            Instantiate(enemyPrefab, tempPosition, transform.rotation);
            coolTime = 0;
        }
    }

    public void update(GameManager gameManager)
    {
        if (gameManager.GetIsGameOver())
        {
            Destroy(this);
        }
    }

    public void update(PlayerController playerController)
    {
        // 아직 뭐 안할거임
    }
}
