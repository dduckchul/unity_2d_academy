using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _myRigid;
    public float movePower = 10.0f; // 프라이빗으로 유지는 하되, 인스펙터에는 노출시켜라
    public float jumpPower = 5.0f;
    [SerializeField] private int _jumpTime = 0;
    
    private bool isJumping = false; // 메서드로 해도 되긴 함 
    private bool isShoot = false;

    public GameObject playerMuzzle; // 게임 오브젝트를 담는 새로운 방식
    public GameObject playerBullet;

    [SerializeField] private float shootCoolTime = 0.6f; // 총알 발사 주기
    [SerializeField] private float shootElapsedTime; // 

    public Animator animator;
    private int _facingRight = 1; // 우측 바라보는 여부 변수 선언
    private int playerHealth = 20;

    [SerializeField] private IPlayerState currentState;
    public JoyStick joyStick;

    public int FacingRight
    {
        get => _facingRight;
        set => _facingRight = value;
    }

    public Rigidbody2D MyRigid
    {
        get => _myRigid;
        set => _myRigid = value;
    }

    public int JumpTime
    {
        get => _jumpTime;
        set => _jumpTime = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        MyRigid = GetComponent<Rigidbody2D>();
        shootElapsedTime = shootCoolTime; // 최초에 총 쏠 수 있도록
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    // 호출 빈도는 극심한 부하가 있는 상화잉 아니면 보통 초에 60번 이상
    // 더 자주, 더 정밀하게 수행 되어야 할 키 입력과 같은 기능이 업데이트
    void Update()
    {
        shootElapsedTime += Time.deltaTime;
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Bullets"))
        {
            JumpTime = 0;
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
        currentState.FixedUpdateState(this);
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void Jump()
    {
        ChangeState(new JumpingState());
    }

    public bool CanJump()
    {
        return JumpTime < 3;
    }

    public bool CanShoot()
    {
        return shootCoolTime < shootElapsedTime;
    }
    
    public void ResetShootTimer()
    {
        shootElapsedTime = 0;
    }
}
