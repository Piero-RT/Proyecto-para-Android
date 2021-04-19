using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaJoystick : MonoBehaviour
{

    public GameObject _bazookaBullet;
    public GameObject shooter;



    //public GameObject explosionEffect;
    private Transform _firePoint;
    public Transform _player;
    private PlayerJoystick player;

    //delay para disparar ya que no funciona el enabled
    public float fireRate = 3f;
    private float nextFire = 0f;

    

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
        player = GameObject.Find("Player").GetComponent<PlayerJoystick>();
        

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void ShootBazooka()
    {
        if (_firePoint != null && Time.time > nextFire && _bazookaBullet != null)
        {
            nextFire = Time.time + fireRate;
           
            if (_bazookaBullet != null && _firePoint != null && shooter != null)
            {
                GameObject bbullet = Instantiate(_bazookaBullet, _firePoint.position, _firePoint.rotation) as GameObject;
                BazookaBullet babullet = bbullet.GetComponent<BazookaBullet>();
                player.enabled = false;
                bbullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(1,0,0)* 20f, ForceMode2D.Impulse);
                if (shooter.transform.localRotation.y < 0f)
                {
                    babullet.direction = Vector2.left;
                }
                else
                {
                    babullet.direction = Vector2.right;
                }
            }


        }

    }


}
