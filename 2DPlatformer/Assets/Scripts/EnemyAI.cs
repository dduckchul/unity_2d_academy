using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private int enemyHealth;
    private Animator enemyAnimator;

    public LayerMask playerLayerMask;
    private float enemySightRange = 10; // 적군 시야 범위

    private float shootCoolTime = 2; // 쿨타임
    [SerializeField] private bool isAttacking = false; // 공격중?
    [SerializeField] private float shootElapsedTime; // 흐른 시간

    public GameObject enemyBullet;
    private Transform target;
    
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        shootElapsedTime += Time.deltaTime;

        if (shootElapsedTime > 2)
        {
            shootElapsedTime = 0;
            isAttacking = false;
        }        
        
        if (isAttacking)
        {
            return;
        }
        
        DetectPlayer();
    }

    // 플레이어 감지하는 메서드
    void DetectPlayer()
    {
        Collider2D player; // 일단 빈 콜라이더 선언
        // 플레이어와 겹치면? -> 플레이어 정보를 담아둔다.
        player = Physics2D.OverlapCircle(transform.position, enemySightRange, playerLayerMask); // 오버랩 서클
        
        if (player != null)
        {
            target = player.transform;
            AttackPlayer();
        }
        else
        {
            enemyAnimator.SetBool("IsDetect", true);            
        }
    }

    void AttackPlayer()
    {
        enemyAnimator.SetTrigger("IsShoot");
        enemyAnimator.SetBool("IsDetect", false);

        int facingRight;

        if (target.position.x < transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            facingRight = -1;
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            facingRight = 1;            
        }

        if (shootElapsedTime < shootCoolTime)
        {
            GameObject tempBullet = Instantiate(enemyBullet, transform.position, transform.rotation);
            tempBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * facingRight, 0);
            shootElapsedTime = 0;
            isAttacking = true;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
