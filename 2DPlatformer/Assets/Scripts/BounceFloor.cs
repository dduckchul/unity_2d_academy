using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFloor : MonoBehaviour
{
    private PhysicsMaterial2D myMat;
    // Start is called before the first frame update
    void Start()
    {
        myMat = new PhysicsMaterial2D();
        myMat.friction = 0.1f; // 마찰 계수
        myMat.bounciness = 0.7f; // 반발력 

        Collider2D coll = GetComponent<Collider2D>();

        if (coll != null)
        {
            coll.sharedMaterial = myMat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
