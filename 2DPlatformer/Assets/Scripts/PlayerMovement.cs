using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigid;
    [SerializeField] private float movePower = 10.0f; // 프라이빗으로 유지는 하되, 인스펙터에는 노출시켜라
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private int jumpTime = 1;
    
    private bool isJumping = false; // 메서드로 해도 되긴 함 

    private float horizontalInput; // 수평 키 입력 수치를 기억시킬 변수

    public GameObject playerMuzzle; // 게임 오브젝트를 담는 새로운 방식
    public GameObject playerBullet;

    [SerializeField] private float shootCoolTime = 0.6f; // 총알 발사 주기
    [SerializeField] private float shootElapsedTime; // 

    public Animator animator;
    private int facingRight = 1; // 우측 바라보는 여부 변수 선언

    private int playerHealth = 20;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody2D>();
        shootElapsedTime = shootCoolTime; // 최초에 총 쏠 수 있도록
    }

    // Update is called once per frame
    // 호출 빈도는 극심한 부하가 있는 상화잉 아니면 보통 초에 60번 이상
    // 더 자주, 더 정밀하게 수행 되어야 할 키 입력과 같은 기능이 업데이트
    void Update()
    {
        // 시간개념 다음 업데이트까지 걸린 시간 측정, Time.DeltaTime
        shootElapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && jumpTime < 3)
        {
            isJumping = true;
        }
        
        if (Input.GetKeyDown(KeyCode.L) && shootCoolTime < shootElapsedTime)
        {
            GameObject firedBullet = Instantiate(playerBullet, playerMuzzle.transform.position, playerMuzzle.transform.rotation); // 어디서 총알을 생성?
            firedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * facingRight, 0);
            shootElapsedTime = 0;
            animator.SetTrigger("IsShoot");
        }        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Bullets"))
        {
            jumpTime = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            // 게임 매니져에게 끝났다고 알리기
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            jumpTime++;
            // myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // Vector2.Left = new Vector2(0,1)
            myRigid.velocity = new Vector2(myRigid.velocity.x, jumpPower);
            isJumping = false;
        }

        // 수평 축 정보 가져오기
        horizontalInput = Input.GetAxis("Horizontal");
        
        // a혹은 d키가 얼마나 눌렸는지
        if (horizontalInput != 0)
        {
            animator.SetBool("IsWalk", true);

            if (horizontalInput < 0)
            {
                facingRight = -1;
                // 각도 변경
                transform.rotation = new Quaternion(0, 180, 0,0);
            }
            else
            {
                facingRight = 1;
                // 각도 변경
                transform.rotation = new Quaternion(0, 0, 0,0);                
            }
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
        
        // 천천히 밀림
        // myRigid.AddForce(new Vector2(horizontalInput, 0) * movePower);

        // 바로 가속을 준다, input Manager에 sensitivity 와 영향 있음 (클수록 반응 빨라짐)
        myRigid.velocity = new Vector2(horizontalInput * movePower, myRigid.velocity.y);
    }
}
