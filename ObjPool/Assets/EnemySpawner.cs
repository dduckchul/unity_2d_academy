using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public EnemyAi enemyPrefab; // 생성할 적군 프리팹 꽂아 넣어 주는 곳
    private ObjectPool<EnemyAi> enemyPool; // 여기서 진짜 풀 생성
    
    // Start is called before the first frame update
    void Start()
    {
        enemyPool = new ObjectPool<EnemyAi>
        (
            createFunc: () =>
            {
                EnemyAi enemy = Instantiate(enemyPrefab);
                enemy.SetPool(enemyPool);
                return enemy;
            },
            actionOnGet: enemy => enemy.gameObject.SetActive(true),
            actionOnRelease: enemy => enemy.gameObject.SetActive(false),
            actionOnDestroy: enemy => Destroy(enemy.gameObject),
            defaultCapacity: 5,
            maxSize: 10
        );
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void spawnEnemy(Vector2 toSpawn)
    {
        EnemyAi enemy = enemyPool.Get();
        enemy.transform.position = toSpawn;
    }
}
