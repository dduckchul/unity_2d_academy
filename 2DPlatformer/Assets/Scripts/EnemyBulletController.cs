using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private EnemyAI fromWho;

    public void SetSpawner(EnemyAI from)
    {
        fromWho = from;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 어느 카메라에서도 볼 수 없게 되었을 때
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (fromWho != null)
            {
                // 이건 왜 안되지?
                // fromWho.bulletPool.ReturnObject(gameObject);                
                fromWho.bulletPool.ReturnObject(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
            // gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
