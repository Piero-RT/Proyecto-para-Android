using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
//    public LineRenderer line;
//    public Transform fire;
//    public GameObject _particlesPrefab;
//    public int damage = 1;
//    private float fireRate = 2f;
//    private float nextFire = 0f;
//    public Transform target;
//    private WaitForSeconds shotDuration = new WaitForSeconds(1f);
//    [SerializeField] private LayerMask layer;
    


//    private void Awake()
//    {
        
//        line = GetComponent<LineRenderer>();

//    }
//    private void Update()
//    {
//        StartCoroutine("ShootLaser");
//    }
//    public IEnumerator ShootLaser()
//    {
//        if (Time.time > nextFire)
//        {
//            nextFire = Time.time + fireRate;
//            //dificil

//            StartCoroutine("ShotEffect");
//            StartCoroutine("LaserPe");
//            //Para hacerlo mas facil usar invoke ya que da tiempo para escapar

//            //Invoke("Apuntar", 0.1f);
//            //Invoke("LaserPe", 0.9f);
//            yield return null;
//        }


//        else
//        {

//        }
//    }

//    public IEnumerator LaserPe()
//    {
//        if (target != null)
//        {
            
//            RaycastHit2D hitinfo = Physics2D.Raycast(fire.position, transform.right = target.position - transform.position, layer);

//            if (hitinfo)
//            {
//                GameObject efect = Instantiate(_particlesPrefab, hitinfo.point, Quaternion.identity);
//                Destroy(efect, 0.5f);
//                line.SetPosition(0, fire.position);
//                line.SetPosition(1, hitinfo.point);
//                if (hitinfo.collider.CompareTag("Hittable"))
//                {
                    
//                    hitinfo.collider.SendMessageUpwards("AddDamage", damage);
//                }
//            }
//            else
//            {
//                line.SetPosition(0, fire.position);
//                line.SetPosition(1, fire.right * 10000f);
//            }
//        }
//        yield return null;
//    }


//    //private void Apuntar()
//    //{
//    //    if (target != null)
//    //    {

//    //        RaycastHit2D hitinfo = Physics2D.Raycast(fire.position, transform.right = target.position - transform.position);

//    //        if (hitinfo)
//    //        {
              
//    //            line.SetPosition(0, fire.position);
//    //            line.SetPosition(1, hitinfo.point);
               
//    //        }
//    //        else
//    //        {
//    //            line.SetPosition(0, fire.position);
//    //            line.SetPosition(1, fire.right * 10000f);
//    //        }
//    //    }
        
//    //}


//    public IEnumerator ShotEffect()
//    {
//        // Play the shooting sound effect


//        // Turn on our line renderer
//        line.enabled = true;
        

//        //Wait for .07 seconds
//        yield return shotDuration;


//        // Deactivate our line renderer after waiting
//        line.enabled = false;
        

//        yield return shotDuration;
        

//        StartCoroutine("ShotEffect");

        

//    }
}

