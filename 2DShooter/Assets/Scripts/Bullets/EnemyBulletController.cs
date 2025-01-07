using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D enemyBulletRb; // 물리 컴포넌트
    private Vector2 moveDirection; // 이동 방향

    private GameObject playerInfo; // 플레이어 정보를 담을 빈 게임 오브젝트
    
    
    // Start is called before the first frame update
    void Start()
    {
        enemyBulletRb = GetComponent<Rigidbody2D>();
        // 연산이 크기때문에 되도록 적게 호출
        playerInfo = GameObject.Find("Player");
        // 플레이어 좌표 - 현재 총알의 좌표 = 플레이어 방향으로 이동 할 총알의 방향
        moveDirection = playerInfo.transform.position - transform.position;
        moveDirection.Normalize();
        enemyBulletRb.AddForce(moveDirection * 1, ForceMode2D.Impulse);
    }

    // Tag 꼬리표 -> 충돌한 대상을 봤더니 Enemy네? -> 뭔가 동작함
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 게임 오브젝트 전체 정보를 들고올 수 있음
        // if (other.gameObject.tag == "Player")
        // {
        //     Destroy(gameObject);
        // }
        
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControler>().DecreaseHp();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
