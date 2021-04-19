using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
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
    private PlayerController player;
    private BazookaWeapon bazookaInst;
    private Animator _animatorplayer;
    public GameObject _bazookaSmokeEffect;
    private Transform _smokepoint;

    






    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        bazookaInst = GameObject.Find("bazokav2_0").GetComponent<BazookaWeapon>();
        _animatorplayer = GameObject.Find("Player").GetComponent<Animator>();
        _smokepoint = transform.Find("SmokeEffect");
    }

    // Start is called before the first frame update
    void Start()
    {
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
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        //direction = new Vector2(x, y);
        if (Input.GetKey(KeyCode.W))
        {
            direction = new Vector2(0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = new Vector2(0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {

            direction = new Vector2(-1, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = new Vector2(1, 0);
        }
        


    }
    private void FixedUpdate()
    {
        //_rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
        //_rb.velocity = speed * direction;

        
        Vector2 movement = direction.normalized * speed;
        _rb.velocity = movement;
       
 
       

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Enemy")
        {
            Debug.Log("Choque");
            Exploded();
            //collision.collider.SendMessageUpwards("TakeDamage", damage);

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
            nearbyobject.GetComponent<Rigidbody2D>().AddForce(direccion * force);
            if (nearbyobject.gameObject.CompareTag("Enemy"))
            {
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
