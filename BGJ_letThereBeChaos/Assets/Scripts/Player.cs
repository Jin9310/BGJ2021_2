using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 5f;
    private float _jumpForce = 7f;
    private float _moveInput;

    private Rigidbody2D rb;

    private bool _facingRight = true;

    [SerializeField] private bool _isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    
    //jumping
    [SerializeField] private int _extraJumps;
    private int _extraJumpsValue = 1;
    

    private void Start()
    {
        _extraJumps = _extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
   
        _moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(_moveInput * _speed, rb.velocity.y);

        if(_facingRight == false && _moveInput > 0)
        {
            Flip();
        }else if(_facingRight == true && _moveInput < 0)
        {
            Flip();
        }
    }
    
    private void Update()
    {
        if(_isGrounded == true)
        {
            _extraJumps = _extraJumpsValue;
        }
    
    
        if(Input.GetKeyDown(KeyCode.UpArrow) && _extraJumps > 0)
        {
            rb.velocity = Vector2.up * _jumpForce;
            _extraJumps--;
        } else if(Input.GetKeyDown(KeyCode.UpArrow) && _extraJumps == 0 && _isGrounded == true)
        {
            rb.velocity = Vector2.up * _jumpForce;
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
