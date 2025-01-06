using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 2초 후에 지워라
        Destroy(gameObject, 2);        
    }

    // Update is called once per frame
    // 두가지 문제, 하이어라키 창에서 안사라짐
    // 0,0 에서 나감    
    void Update()
    {
        transform.Translate(0, 0.1f, 0);

        // 원초적인 코딩 y가 얼마 이상이면 지워라
        // if (transform.position.y > 10)
        // {
        //     Destroy(gameObject);
        // }
    }
}
