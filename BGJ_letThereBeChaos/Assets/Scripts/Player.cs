using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float _speed = 5f;
    private float _jumpForce = 6f;
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

    //dash
    private float dashDistance = 7f;
    private bool isDashing;
    [SerializeField] private bool dashIsReady = true;

    private void Start()
    {
        _extraJumps = _extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        _moveInput = Input.GetAxis("Horizontal");

        if (!isDashing)
        {
            rb.velocity = new Vector2(_moveInput * _speed, rb.velocity.y);
        }

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
        //dashing
        if (dashIsReady == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                dashIsReady = false;

                if (_facingRight == true)
                {
                    StartCoroutine(Dash(1));
                }
                else if (_facingRight == false)
                {
                    StartCoroutine(Dash(-1));
                }
            }
        }
        

        if (_isGrounded == true)
        {
            _extraJumps = _extraJumpsValue;
            dashIsReady = true;
        }
    
    
        if((Input.GetKeyDown(KeyCode.UpArrow) || 
            Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.Space)) && _extraJumps > 0)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OutOfTheScreen"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(.2f);
        isDashing = false;
        rb.gravityScale = gravity;
    }
}
