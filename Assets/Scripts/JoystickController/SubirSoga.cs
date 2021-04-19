using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubirSoga : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerJoystick PJoystick;

    private float verticalMove = 0f;
    public Joystick joystick;
    private Vector2 _movement;


    //public BoxCollider2D platformGround;
    
    public bool onLadder = false;

    public float climbSpeed;
    public float exitHop = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //platformGround = GetComponentsInChildren<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        PJoystick = GetComponent<PlayerJoystick>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            onLadder = true;
            if (verticalMove != 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, verticalMove * climbSpeed);
                _rb.gravityScale = 0;
                //platformGround.enabled = false;
                PJoystick.usingLadder = onLadder;
                Debug.Log("subo");
            }
            else if (verticalMove == 0 && onLadder)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") && onLadder)
        {
            _rb.gravityScale = 1;
            onLadder = false;
            //platformGround.enabled = true;
            PJoystick.usingLadder = onLadder;     
            if (!PJoystick._isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, exitHop);
            }
        }
    }
    public void FixedUpdate()
    {
        if (onLadder == true)
        {
            verticalMove = joystick.Vertical * climbSpeed;
            transform.position += new Vector3(0, verticalMove, 0) * Time.deltaTime * climbSpeed;


            float VerticalVelocity = _movement.normalized.x * 1f;
            _rb.velocity = new Vector2(_rb.velocity.x, VerticalVelocity);
        } 
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
