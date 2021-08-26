using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSquare : MonoBehaviour
{
    private float pushForce = 500f;
    Rigidbody2D rb;
    [SerializeField] private float direction;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //direction = -1;
        rb.AddForce(transform.right * pushForce * direction);
    }

}
