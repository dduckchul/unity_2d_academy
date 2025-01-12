using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private int enemyHealth;
    private Animator enemyAnimator;

    public LayerMask playerLayerMask;
    private float enemySightRange = 10; // 적군 시야 범위

    // 직렬화 (Serialization)
    // 데이터 구조 또는 게임 오브젝트의 상태를 보관하고 관리하는 기법
    // 인스펙터 창에서 오브젝트의 직렬화 된 멤버 변수 값을 보여준다.
    // 이를 이용해서 소스코드의 수정 없이 유니티 에디터에서 값을 변경 가능
    
    // 데이터 직렬화
    // 오브젝트의 멤버 변수 값을 확인하거나 변경
    // 오브젝트의 멤버 변수 참조를 드래그 앤 드랍 방식으로 연결 가능하게 해준다.

    [Space(30)] [Header("Unity Attribute")] [SerializeField]
    private int privateValue; // 프라이빗 이어도 직렬화 해서 인스펙터에 보인다.

    [HideInInspector] public int publicValue; // 퍼블릭인데도 숨길수 있다
    [TextArea(3, 5)] public string textField; // 으흠

    [Serializable] // 직렬화 가능한 속성을 부여한다.
    public struct StructType
    {
        public int val1;
        public int val2;
    }

    public StructType type1;
    
    [Serializable] // 직렬화 가능한 속성을 부여한다.    
    public class MyClass
    {
        public int val;
    }
    
    // Attribute
    [Header("Shooting Var")]
    [SerializeField] private float shootCoolTime = 2; // 쿨타임
    [SerializeField] private bool isAttacking = false; // 공격중?
    [SerializeField] private float shootElapsedTime; // 흐른 시간

    [Header("멘트")] // 헤더 달았음
    public GameObject enemyBullet;
    public GameObject enemyMuzzle; // 게임 오브젝트를 담는 새로운 방식
    
    private int initPoolSize = 10; // 풀 사이즈 추가 했음

    public ObjectPool<EnemyBulletController> bulletPool;
    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        bulletPool = new ObjectPool<EnemyBulletController>(enemyBullet.gameObject.GetComponent<EnemyBulletController>(), initPoolSize);
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
            enemyAnimator.ResetTrigger("IsShoot");
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

        // 나의 풀에서 총알 하나 가져오기
        GameObject tempBullet = bulletPool.GetObject().gameObject;
        
        if (tempBullet !=null && shootElapsedTime < shootCoolTime)
        {
            // 켜주고 위치 속도 등 설정
            tempBullet.transform.position = enemyMuzzle.transform.position;
            tempBullet.transform.rotation = enemyMuzzle.transform.rotation;
            tempBullet.GetComponent<Rigidbody2D>().velocity = new Vector2();
            tempBullet.GetComponent<EnemyBulletController>().SetSpawner(this);
            tempBullet.SetActive(true);
            
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
