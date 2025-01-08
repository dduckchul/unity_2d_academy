using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigid;
    private float movePower = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            myRigid.AddForce(Vector2.left * movePower); // Vector2.Left = new Vector2(-1,0)
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            myRigid.AddForce(Vector2.right * movePower); // Vector2.Left = new Vector2(1,0)
        }        
    }
}
