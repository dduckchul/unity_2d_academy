using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    // 맵의 위쪽에서 y 좌표가 3이 될때까지 힘을 받아 이동한다
    // 이동이 완료되면 플레이어를 향해 일정한 주기로 총알을 발사한다.

    private Rigidbody2D enemyRb;
    private Vector2 moveForce;

    public GameObject enemyBulletPrefab; // 적 총알 꽃아 넣을 프리팹 슬롯

    private int coolTime; // Time을 아직 안쓸거다
    private bool isPlayerDead;
    
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        moveForce = new Vector2(0, -1); // 초기 미는 힘 설정
        coolTime = 0;
    }
    
    void Update()
    {
        // 이런 부분 최적화 하고싶다 = 옵저버 패턴
        // 플레이어 죽으면 안움직이게
        isPlayerDead = GameManager.Instance.GetIsGameOver();

        if (isPlayerDead)
        {
            return;
        }

        if (transform.position.y > 3)
        {
            enemyRb.AddForce(moveForce);
        }
        else
        {
            coolTime++;

            // 업데이트 1000번 할때마다 총알 쏘기
            if (coolTime >= 1000)
            {
                Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
                coolTime = 0;
            }
        }
    }
}
