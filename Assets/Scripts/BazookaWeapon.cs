using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaWeapon : MonoBehaviour
{
    public GameObject _bazookaBullet;
    public GameObject shooter;
    
    

    //public GameObject explosionEffect;
    private Transform _firePoint;
    public Transform _player;
    private PlayerController player;
    
   

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (_bazookaBullet != null && _firePoint != null && shooter != null)
            {
                GameObject bbullet = Instantiate(_bazookaBullet, _firePoint.position, _firePoint.rotation) as GameObject;
                BazookaBullet babullet = bbullet.GetComponent<BazookaBullet>();
                player.enabled = false;
                
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
