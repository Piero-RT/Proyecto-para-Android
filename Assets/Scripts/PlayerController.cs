using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float jumpForce = 2f;
    //chequeo del suelo
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadious;

    //referencias
    private Rigidbody2D _rb;
    private Animator _animator;

    //moviemiento
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;

    //Arma
    public GameObject poseearma;
    public GameObject poseearma2;
    public GameObject poseearma3;
    //private bool tienearma;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        //movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0f);

        //flip player
        if (horizontalInput < 0f && _facingRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && _facingRight== false)
        {
            Flip();
        }
        // _is grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, groundLayer);

        //isJumping?

        if (Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void FixedUpdate()
    {
        float horizontalVelocity = _movement.normalized.x * speed;
        _rb.velocity = new Vector2(horizontalVelocity, _rb.velocity.y);
    }
    private void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rb.velocity.y);
    }
    private void Flip()
    {

        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);

        //"!" remplaza a if
       /* float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arma"))
        {
            //tienearma = true;
            Destroy(collision.gameObject);
            poseearma.gameObject.SetActive(true);
            poseearma2.gameObject.SetActive(false);
            poseearma3.gameObject.SetActive(false);

        }
        if (collision.gameObject.CompareTag("Arma1"))
        {
            Destroy(collision.gameObject);
            poseearma2.gameObject.SetActive(true);
            poseearma.gameObject.SetActive(false);
            poseearma3.gameObject.SetActive(false);

            
        }
        if (collision.gameObject.CompareTag("Arma2"))
        {
            Destroy(collision.gameObject);
            poseearma3.gameObject.SetActive(true);
            poseearma2.gameObject.SetActive(false);
            poseearma.gameObject.SetActive(false);

        }
    }

}

