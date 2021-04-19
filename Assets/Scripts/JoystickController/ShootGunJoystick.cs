using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunJoystick : MonoBehaviour
{
    public bool poseerarma;
    


    public GameObject _particlesPrefab;
    public float force = 4;
    public int damage;


    public LineRenderer lineRenderer;
    private float fireRate = 0.8f;
    private float nextFire = 0f;

    public Transform _firepoint;
    private bool _isShooting;
    private Animator _anim;

    public LayerMask mask;

    void Awake()
    {
        
        _firepoint = transform.Find("FirePoint");
        _anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        //{
        //    nextFire = Time.time + fireRate;
        //    StartCoroutine("ShootWithRaycast");
        //    _anim.SetBool("IsShooting", true);
        //}
        //else
        //{
        //    _anim.SetBool("IsShooting", false);
        //} 

    }

    public IEnumerator ShootWithRaycast()
    {
        if (_particlesPrefab != null && lineRenderer != null)
        {

            RaycastHit2D hitInfo = Physics2D.Raycast(_firepoint.position, _firepoint.right, mask);


            if (hitInfo)
            {
                GameObject _effect = Instantiate(_particlesPrefab, hitInfo.point, Quaternion.identity);
                Destroy(_effect, 0.5f);
                if (hitInfo.collider != null && hitInfo.collider.GetComponent<Rigidbody2D>() != null)
                {
                    if (hitInfo.collider.CompareTag("Enemy"))
                    {
                        hitInfo.collider.GetComponent<Rigidbody2D>().AddForce(hitInfo.normal * force, ForceMode2D.Impulse);
                        hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                    }
                    lineRenderer.SetPosition(0, _firepoint.position);
                    lineRenderer.SetPosition(1, hitInfo.point);
                }

            }
            else
            {
                lineRenderer.SetPosition(0, _firepoint.position);
                lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100f);
            }

            lineRenderer.enabled = true;

            yield return null;

            lineRenderer.enabled = false;
        }

    }

    //public void Shoot()
    //{
        //if ( poseerarma = true && Time.time > nextFire)
        //{
        //    nextFire = Time.time + fireRate;
        //    StartCoroutine("ShootWithRaycast");
        //    _anim.SetBool("IsShooting", true);

        //}
        //else
        //{
        //    _anim.SetBool("IsShooting", false);
        //}

        //}
  
    public void BotonapretadoS()
    {
        if (poseerarma = true && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine("ShootWithRaycast");
            _anim.SetBool("IsShooting", true);

        }
    }
    public void BotonsueltoS()
    {
        _anim.SetBool("IsShooting", false);
        StopCoroutine("ShootWithRaycast");
    }

}
