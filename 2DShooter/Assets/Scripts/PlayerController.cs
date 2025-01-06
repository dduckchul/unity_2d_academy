using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Transform playerTr;
    private Rigidbody2D playerRb;

    private Vector2 pushPower; // 미는 힘 선언

    public float moveSpeed = 5f;
    public float maxSpeed = 10f;

    public GameObject bulletPrefab; // 게임 오브젝트를 담을 수 있는 껍데기
    public GameObject secondBulletPrefab; // 두번째 총알 담을 오브젝트

    private Vector3 tempPos; // 임시 포지션 값

    public bool isDoubleFire;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
        pushPower = new Vector2(); // 시작과 동시에 초기화
        // pushPower = Vector2.zero; // 시작과 동시에 초기화, 같은거
        isDoubleFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        pushPower = Vector2.zero;
        
        if (Input.GetKey(KeyCode.A))
        {
            // 다 같은 코드
            // playerTr.position = playerTr.position + new Vector3(-0.01f, 0, 0);
            // playerTr.Translate(new Vector3(-0.01f, 0, 0));
            // playerTr.Translate(-0.01f, 0, 0); 
            // transform.Translate(-0.01f, 0, 0); // 내장된 transform

            // 자연스럽게 움직이도록 힘 추가하는 방법?
            // playerRb.AddForce(new Vector2(-1, 0));

            // 일정한 속도로 움직이기?
            pushPower.x = -1;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            // playerTr.Translate(0.01f, 0, 0); 
            // playerRb.AddForce(new Vector2(1, 0));
            
            // mass 질량
            // liniar drag -> 저항
            // angular drag -> 뭔가 회전할때 저항 뺑글 뺑글 도는거 막아줌
            // velocity 속도 (속력 + 방향)
            pushPower.x = 1;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            pushPower.y = 1;
            // playerRb.AddForce(new Vector2(0, 1));
        }

        else if (Input.GetKey(KeyCode.S))
        {
            pushPower.y = -1;
            // playerRb.AddForce(new Vector2(0, -1));
        }

        // 플레이 모드 중 L키가 눌린다면?
        // Bullet 이라는 게임 오브젝트가 하이어라키에 추가되었으면 좋겠다
        // 생성된 Bullet엔 BulletController 스크립트도 있었으면 좋겠다.
        // 그 총알이 스크립트 대로 이동했으면 좋겠다.
        // Prefab asset -> 설계도 따서 만드는 에셋
        
        // 총알이 발사 되었으면 좋겠다.
        if (Input.GetKeyDown(KeyCode.L))
        {
            tempPos = transform.position; // L 키가 눌리면 현재 플레이어 정보 위치 담기

            Vector3 v1 = new Vector3(tempPos.x-0.15f, tempPos.y, 0);
            Vector3 v2 = new Vector3(tempPos.x+0.15f, tempPos.y, 0);

            // Instatiate(bulletPrefab, 위치, 회전) 
            // Instantiate(bulletPrefab);
            // 이 위치, 이 회전률로 프리팹을 인스턴스화 해라
            Instantiate(bulletPrefab, v1, transform.rotation);
            if (isDoubleFire)
            {
                Instantiate(bulletPrefab, v2, transform.rotation);                
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            tempPos = transform.position;
            tempPos.y += 0.5f;

            Instantiate(secondBulletPrefab, tempPos, transform.rotation);
        }
        
        // 버니홉 같은거 방지하기, 정규화하기, 1을 넘지 않도록, 무조건 1이하의 숫자로 나옴
        // pushPower.Normalize(); // 정규화된 수치로 변경, 값이 아예 바껴버림
        pushPower = pushPower.normalized * moveSpeed; // 읽어와서 참고만, 정규화된 수치만 필요하다?
        
        // 정규화된 수치를 addForce 시킨다.
        playerRb.AddForce(pushPower);

        // velocity는 속도 (속력 + 방향), magnitude는 그냥 퓨어한 속도 그 자체
        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxSpeed;
        }
    }
}
