using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangoattack : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed = 2f;
    private float limitLeft;
    private float limitRight;
    int direccion = 1;
    private Animator anim;
    //Laser
    public LineRenderer line;
    public Transform fire;
    public GameObject _particlesPrefab;
    public int damage = 1;
    private float fireRate = 2f;
    private float nextFire = 0f;
    public Transform target;
    private WaitForSeconds shotDuration = new WaitForSeconds(1f);
    public LayerMask layer;

    public float umbralVelocidad;
    //tipodecomportamiento

    enum tipoDeComportamientoEnemy { pasivo, persecucion, ataque }
    tipoDeComportamientoEnemy comportamiento = tipoDeComportamientoEnemy.pasivo;

    private float entradaZonaPersecucion = 1.90f;
    private float salidaZonaPersecucion = 1.98f;
    private float zonaAtaque = 1.50f;

    private float distaciaConPlayer;
    public Transform player;

    private CircleCollider2D ccollidero;

    


    private void Awake()
    {
        ccollidero = GameObject.Find("RangeVisionOjo").GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        limitLeft = transform.position.x - ccollidero.radius;
        limitRight = transform.position.x + ccollidero.radius;
    }


    private void Update()
    {
        distaciaConPlayer = Mathf.Abs(player.position.x - transform.position.x);
        transform.localScale = new Vector3(1 * direccion, 1, 1);
        StopCoroutine("ShootLaser");
        anim.SetBool("RunO", true);
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
                    StopCoroutine("ShootLaser");
                {
                    _rb.velocity = new Vector2(speed * 2f * direccion, _rb.velocity.y);
                    
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
                    StartCoroutine("ShootLaser");
                    anim.SetTrigger("AttackL");
                    anim.SetBool("RunO", false);
                  
                    _rb.velocity = new Vector2(0, _rb.velocity.y);

                    if (player.position.x > transform.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        direccion = 1;

                    }
                    if (player.position.x < transform.position.x)
                    {
                        
                        direccion = 1;
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }

                    if (distaciaConPlayer > zonaAtaque)
                    {
                        comportamiento = tipoDeComportamientoEnemy.persecucion;
                        StopCoroutine("ShootLaser");
                        line.enabled = false;
                        StopCoroutine("ShotEffect");
                    }
                }
                break;
        }



    }


    public IEnumerator ShootLaser()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //dificil

            StartCoroutine("ShotEffect");
            StartCoroutine("LaserPe");
            //Para hacerlo mas facil usar invoke ya que da tiempo para escapar

            //Invoke("Apuntar", 0.1f);
            //Invoke("LaserPe", 0.9f);
            yield return null;
        }


        else
        {

        }
    }

    public IEnumerator LaserPe()
    {
        if (target != null)
        {
            //si fuese persiguidor
            //RaycastHit2D hitinfo = Physics2D.Raycast(fire.position, transform.right = target.position - transform.position, layer);
            RaycastHit2D hitinfo = Physics2D.Raycast(fire.position, transform.right, layer);
            if (hitinfo)
            {
                GameObject efect = Instantiate(_particlesPrefab, hitinfo.point, Quaternion.identity);
                Destroy(efect, 0.05f);
                line.SetPosition(0, fire.position);
                line.SetPosition(1, hitinfo.point);
                if (hitinfo.collider.CompareTag("Hittable"))
                {

                    hitinfo.collider.SendMessageUpwards("AddDamage", damage);
                }
            }
            else
            {
                line.SetPosition(0, fire.position);
                line.SetPosition(1, fire.right * 10000f);
            }
        }
        yield return null;
    }


    //private void Apuntar()
    //{
    //    if (target != null)
    //    {

    //        RaycastHit2D hitinfo = Physics2D.Raycast(fire.position, transform.right = target.position - transform.position);

    //        if (hitinfo)
    //        {

    //            line.SetPosition(0, fire.position);
    //            line.SetPosition(1, hitinfo.point);

    //        }
    //        else
    //        {
    //            line.SetPosition(0, fire.position);
    //            line.SetPosition(1, fire.right * 10000f);
    //        }
    //    }

    //}


    public IEnumerator ShotEffect()
    {
        // Play the shooting sound effect


        // Turn on our line renderer
        line.enabled = true;


        //Wait for .07 seconds
        yield return shotDuration;


        // Deactivate our line renderer after waiting
        line.enabled = false;


        yield return shotDuration;


        StartCoroutine("ShotEffect");



    }
}
