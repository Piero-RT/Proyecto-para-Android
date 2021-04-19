using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsquletoPatrol : MonoBehaviour
{
    //poratldistance
    private Transform _firePointP;
    public GameObject portal;
    public GameObject portaled;
    public float distance;

    //mov
    private Rigidbody2D _rb;
    public float speed = 2f;
    private float limitLeft;
    private float limitRight;
    int direccion = 1;
    private Animator _anim;
    public float umbralVelocidad;
    //tipodecomportamiento
    private float fireRate = 4f;
    private float nextFire = 0;
    enum tipoDeComportamientoEnemy { pasivo, persecucion, ataque }
    tipoDeComportamientoEnemy comportamiento = tipoDeComportamientoEnemy.pasivo;

    private float entradaZonaPersecucion = 1f;
    private float salidaZonaPersecucion = 1.46f;
    private float zonaAtaque = 1.33f;

    private float distaciaConPlayer;
    public Transform player;

    //public LayerMask mask;

    private CircleCollider2D ccollider;

    private void Awake()
    {
        _firePointP = transform.Find("FirePointP");
        ccollider = GameObject.Find("RangeVisionE").GetComponent<CircleCollider2D>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

        limitLeft = transform.position.x - ccollider.radius;
        limitRight = transform.position.x + ccollider.radius;
    }


    private void Update()
    {
        distaciaConPlayer = Mathf.Abs(player.position.x - transform.position.x);
        transform.localScale = new Vector3(1 * direccion, 1, 1);

        switch (comportamiento)
        {
            case tipoDeComportamientoEnemy.pasivo:
                if (_rb.velocity.magnitude < umbralVelocidad)
                {
                    _rb.velocity = new Vector2(speed * direccion, _rb.velocity.y);
                    _anim.SetBool("Idle", false);
                    if (transform.position.x < limitLeft)
                    {
                        direccion = 1;

                    }
                    if (transform.position.x > limitRight)
                    {
                        direccion = -1;

                    }
                    if (distaciaConPlayer < entradaZonaPersecucion)
                    {
                        comportamiento = tipoDeComportamientoEnemy.persecucion;
                    }
                }
                break;

            case tipoDeComportamientoEnemy.persecucion:
                if (_rb.velocity.magnitude < umbralVelocidad)
                {
                    _rb.velocity = new Vector2(speed * direccion, _rb.velocity.y);
                    _anim.SetBool("Idle", false);
                    if (player.position.x > transform.position.x)
                    {
                        direccion = 1;

                    }
                    if (player.position.x < transform.position.x)
                    {
                        direccion = -1;

                    }
                    if (distaciaConPlayer > salidaZonaPersecucion)
                    {
                        comportamiento = tipoDeComportamientoEnemy.pasivo;
                    }
                    if (distaciaConPlayer < zonaAtaque)
                    {
                        comportamiento = tipoDeComportamientoEnemy.ataque;
                    }
                }
                break;

            case tipoDeComportamientoEnemy.ataque:
                if (_rb.velocity.magnitude < umbralVelocidad)
                {
                    if (Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        //dificil
                        _anim.SetBool("Idle", true);
                        _anim.SetTrigger("AttackP");
                        if (direccion == 1)
                        {
                            Instantiate(portal, _firePointP.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
                            Instantiate(portaled, player.position + new Vector3(0.1f, 0.1f ,0), Quaternion.identity);
                        }
                        else if (direccion == -1)
                        {
                            Instantiate(portal, _firePointP.position  + new Vector3(-0.2f, 0, 0), Quaternion.identity);
                            Instantiate(portaled, player.position + new Vector3(-0.1f, 0.1f, 0), Quaternion.identity);
                        }
                
                    }

                        _rb.velocity = new Vector2(0, _rb.velocity.y);
                    if (player.position.x > transform.position.x)
                    {
                        direccion = 1;

                    }
                    if (player.position.x < transform.position.x)
                    {
                        direccion = -1;

                    }

                    if (distaciaConPlayer > zonaAtaque)
                    {
                        comportamiento = tipoDeComportamientoEnemy.persecucion;
                    }
                }
                break;
        }



    }
    //public void Effect()
    //{
        
    //    if (portal != null && _firePointP != null)
    //    {
    //        GameObject myBullet = Instantiate(portal, _firePointP.position * Vector2.right * 2f, Quaternion.identity) as GameObject;

    //        Portal bulletComponent = myBullet.GetComponent<Portal>();

    //    //    RaycastHit2D hitInfo = Physics2D.Raycast(_firePointP.position, _firePointP.right);
        
    //    //Debug.DrawRay(_firePointP.position, _firePointP.right * distance, Color.red);
    //    //if (hitInfo)
    //    //{
    //    //    GameObject _effect = Instantiate(portal, hitInfo.point, Quaternion.identity);
    //    //    GameObject _portale = Instantiate(portaled, hitInfo.point + Vector2.right * 2f, Quaternion.identity);
    //    }
    //}
}
