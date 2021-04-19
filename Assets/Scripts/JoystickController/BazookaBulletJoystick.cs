using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBulletJoystick : MonoBehaviour
{
    //joystick control
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private Joystick joystick;
    public float runSpeedJoystick = 0f;
    public bool movbb;


    public float speed = 2f;
    public Vector2 direction;
    public float livingTime = 3f;
    public int damage = 50;
    private Rigidbody2D _rb;

    public float delay = 3f;
    private float countDown;
    bool hasExploded = false;
    public GameObject explosioneffectfx;
    public float radius = 5f;
    public float force = 300f;
    public LayerMask LayerToHit;



    private float _startingTime;
    private PlayerJoystick player;
    private BazookaJoystick bazookaInst;
    private Animator _animatorplayer;
    public GameObject _bazookaSmokeEffect;
    private Transform _smokepoint;

    private Transform _firepoint;
    //private SpriteRenderer _renderer;
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerJoystick>();
        bazookaInst = GameObject.Find("bazokav2_0").GetComponent<BazookaJoystick>();
        _animatorplayer = GameObject.Find("Player").GetComponent<Animator>();
        _smokepoint = transform.Find("SmokeEffect");
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        _firepoint = GameObject.Find("Player").GetComponent<Transform>();
        //_renderer = GameObject.Find("bazokav2_0").GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movbb = false;
        countDown = delay;
        _startingTime = Time.time;

        Destroy(gameObject, livingTime);

    }
    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;  
        if (countDown <= 0f && !hasExploded)
        {
            Exploded();
            hasExploded = true;
            
        }

        GameObject _effectba = Instantiate(_bazookaSmokeEffect, _smokepoint.position, _smokepoint.rotation);
        Destroy(_effectba, 0.4f);

        Vector2 movement = direction.normalized * speed;
        _rb.velocity = movement;       
        
        if (movbb == false) 
        {
            direction = _firepoint.right;
            movbb = true;
            
        }
        else if (movbb == true)
        {
            _rb.velocity = movement;
            
        }
        if (verticalMove < 0)
        {
            direction = new Vector2(0, 1);
        }
        else if (verticalMove > 0)
        {
            direction = new Vector2(0, -1);
        }
        if (horizontalMove < 0)
        {

            direction = new Vector2(-1, 0);
        }
        else if (horizontalMove > 0)
        {
            direction = new Vector2(1, 0);
        }



    }
    private void FixedUpdate()
    {
        //joystickmovement
        horizontalMove = joystick.Horizontal;
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime ;
        
        verticalMove = joystick.Vertical * runSpeedJoystick;
        transform.position += new Vector3(0, verticalMove, 0) * Time.deltaTime;
        //if (asdasdasd && facing == true)
        //{

        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Choque");
            Exploded();
            
            collision.collider.SendMessageUpwards("TakeDamage", damage);

        }

    }

    private void OnEnable()
    {
        _animatorplayer.SetBool("Idle", true);
        bazookaInst.enabled = false;
    }

    private void OnDisable()
    {
        player.enabled = true;
        bazookaInst.enabled = true;
    }
    void Exploded()
    {
        
        //show
        GameObject Explosionefecto = Instantiate(explosioneffectfx, transform.position, Quaternion.identity);
        Destroy(Explosionefecto, 1f);
        // get object
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerToHit);
        foreach (Collider2D nearbyobject in colliders)
        {
            Vector2 direccion = nearbyobject.transform.position - transform.position;
            
            if (nearbyobject.gameObject.CompareTag("Enemy"))
            {
                nearbyobject.GetComponent<Rigidbody2D>().AddForce(direccion * force, ForceMode2D.Impulse);
                nearbyobject.SendMessageUpwards("TakeDamage", damage);
            }
            //if (nearbyobject.gameObject.CompareTag("Player"))
            //{
            //    nearbyobject.SendMessage("", damage);
            //}

        }
        //damage

        Destroy(gameObject);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
