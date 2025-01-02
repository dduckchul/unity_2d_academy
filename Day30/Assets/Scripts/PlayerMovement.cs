using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 playerPos; // x와 y를 동시에 지닌 자료형

    private Vector3 playerPos3; // x,y,z를 기억시켜야 할때

    
    // Start is called before the first frame update
    void Start()
    {
        // 초기 위치값 세팅
        playerPos.x = 0; 
        playerPos.y = -3;
    }

    // Update is called once per frame
    void Update()
    {
        // 특정 키가 안눌려 있으면 false, 누름 유지중이면 true 반환
        // Input.GetKey()
        // 누를때
        // Input.GetKeyDown();
        // 키를 뗄때
        // Input.GetKeyUp();
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            playerPos.x -= 0.001f;
        }
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            playerPos.x += 0.001f;
        }
        
        // transform 안에 있는 포지션을 통채로 갈아끼움
        transform.position = playerPos;
    }
}
