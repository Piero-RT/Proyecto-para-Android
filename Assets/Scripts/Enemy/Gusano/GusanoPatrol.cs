using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusanoPatrol : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed = 2f;
    private float limitLeft;
    private float limitRight;
    int direccion = 1;
    private Animator _anim;
    public float umbralVelocidad;
    //tipodecomportamiento

    enum tipoDeComportamientoEnemy { pasivo, persecucion, ataque}
    tipoDeComportamientoEnemy comportamiento = tipoDeComportamientoEnemy.pasivo;

    private float entradaZonaPersecucion = 0.83f;
    private float salidaZonaPersecucion = 1.46f;
    private float zonaAtaque = 0.20f;

    private float distaciaConPlayer;
    public Transform player;

    //public LayerMask mask;

    private CircleCollider2D ccollider;

    private void Awake()
    {
        ccollider = GameObject.Find("RangeVision").GetComponent<CircleCollider2D>();
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
                    _anim.SetBool("Run", true);
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
                    _anim.SetBool("Run", true);
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
                    
                    _anim.SetTrigger("Attack");
                    _anim.SetBool("Run", false);
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

}
