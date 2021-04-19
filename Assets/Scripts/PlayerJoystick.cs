using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    //joystick control
    private float horizontalMove = 0f;
    //private float verticalMove = 0f;
    public Joystick joystick;
    public float runSpeedJoystick = 2f;

    public bool usingLadder = false;

    //run y jump
    public float speed = 2f;
    public float jumpForce = 3f;

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
    [HideInInspector]
    public bool _isGrounded;

    //Arma
    public GameObject poseearma;
    public GameObject poseearma2;
    public GameObject poseearma3;

    //Boton virtual por arma
    public GameObject botonArma;
    public GameObject botonArma2;
    public GameObject botonArma3;
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
        //float horizontalInput = Input.GetAxisRaw("Horizontal");
        //_movement = new Vector2(horizontalInput, 0f);

        //flip player
        if (horizontalMove < 0f && _facingRight == true)
        {
            Flip();
            
        }
        else if (horizontalMove > 0f && _facingRight == false)
        {
            Flip();
            
        }
        // _is grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, groundLayer);

        //isJumping?

        //if (Input.GetButtonDown("Jump") && _isGrounded == true)
        //{
        //    _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //}
    }
    public void FixedUpdate()
    {
        //joystickmovement
        horizontalMove = joystick.Horizontal * runSpeedJoystick;
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * runSpeedJoystick;
        

        float horizontalVelocity = _movement.normalized.x * speed;
        _rb.velocity = new Vector2(horizontalVelocity, _rb.velocity.y);
    }
    private void LateUpdate()
    {
        _animator.SetBool("Idle", horizontalMove == 0f);
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
            // bonton arma
            botonArma.gameObject.SetActive(true);
            botonArma2.gameObject.SetActive(false);
            botonArma3.gameObject.SetActive(false);

        }
        if (collision.gameObject.CompareTag("Arma1"))
        {
            Destroy(collision.gameObject);
            poseearma2.gameObject.SetActive(true);
            poseearma.gameObject.SetActive(false);
            poseearma3.gameObject.SetActive(false);
            // bonton arma
            botonArma.gameObject.SetActive(false);
            botonArma2.gameObject.SetActive(true);
            botonArma3.gameObject.SetActive(false);


        }
        if (collision.gameObject.CompareTag("Arma2"))
        {
            Destroy(collision.gameObject);
            poseearma3.gameObject.SetActive(true);
            poseearma2.gameObject.SetActive(false);
            poseearma.gameObject.SetActive(false);
            // bonton arma
            botonArma.gameObject.SetActive(false);
            botonArma2.gameObject.SetActive(false);
            botonArma3.gameObject.SetActive(true);

        }
    }

    public void Jump()
    {
        if (_isGrounded == true)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (!usingLadder)
        {
            _animator.SetFloat("VerticalVelocity", _rb.velocity.y);
        }
    }

}
