using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusanoAttack : MonoBehaviour
{
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hittable"))
        {
            collision.SendMessageUpwards("AddDamage", damage);
        }
    }
}
