using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 기체 프리팹 설계도
    private int coolTime;
    private int spawnSpeed;
    private Vector3 tempPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        coolTime = 0;
        spawnSpeed = 100;
        tempPosition = Vector3.zero;
    }

    // Update is called once per frame
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
}
