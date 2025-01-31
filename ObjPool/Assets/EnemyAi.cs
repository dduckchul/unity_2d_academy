using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyAi : MonoBehaviour
{
    private ObjectPool<EnemyAi> pool;

    public void SetPool([CanBeNull] ObjectPool<EnemyAi> enemyPool)
    {
        pool = enemyPool;
    }

    public void TakeDamage(int damage)
    {
        if (damage >= 3)
        {
            pool.Release(this);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
