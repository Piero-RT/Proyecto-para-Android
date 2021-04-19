using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoradoPatrol : MonoBehaviour
{
    //missile
    public GameObject Missile;
    public Transform firePoint;
    //Shoot
    private float fireRate = 2f;
    private float nextFire = 0;


    private Rigidbody2D _rb;
    public float speed = 2f;
    private float limitLeft;
    private float limitRight;
    int direccion = 1;
    private Animator _anim;
    public float umbralVelocidad;
    //tipodecomportamiento

    enum tipoDeComportamientoEnemy { pasivo, persecucion, ataque }
    tipoDeComportamientoEnemy comportamiento = tipoDeComportamientoEnemy.pasivo;

    private float entradaZonaPersecucion = 0.83f;
    private float salidaZonaPersecucion = 1.46f;
    private float zonaAtaque = 1.20f;

    private float distaciaConPlayer;
    public Transform player;

    //public LayerMask mask;

    private CircleCollider2D ccollider;

    private void Awake()
    {
        ccollider = GameObject.Find("RangeVisionM").GetComponent<CircleCollider2D>();
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
                    StartCoroutine("HommingMissile");

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


    public IEnumerator HommingMissile()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //dificil


            //Para hacerlo mas facil usar invoke ya que da tiempo para escapar
            Invoke("MissileH", 0.1f);
    
             
            //Invoke("Apuntar", 0.1f);
            //Invoke("LaserPe", 0.9f);
            yield return null;
        }
    }
    void MissileH()
    {
        GameObject MissileH = Instantiate(Missile, firePoint.position, Quaternion.Euler(0, 0, 90)) as GameObject;
        HommingMissile missilecomponent = MissileH.GetComponent<HommingMissile>();

    }



}
