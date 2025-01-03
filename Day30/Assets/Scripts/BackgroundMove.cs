using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = -0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,moveSpeed,0);

        if (transform.position.y < -6f)
        {
            transform.Translate(0, 10, 0);
        }
    }
}
