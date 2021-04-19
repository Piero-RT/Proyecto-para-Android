using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingMissile : MonoBehaviour
{
    //pruebas
    public Vector2 direction;
    private WaitForSeconds shootM = new WaitForSeconds(1f);

    private GameObject target;
    public GameObject explosion;
    public float rotationSpeed;

    Quaternion rotateTraget;
    Vector3 dir;

    Rigidbody2D rb;

    public float damage = 1;

    private void Awake()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        StartCoroutine("missilecomportamiento");
    }
    IEnumerator missilecomportamiento()
    {
        // Play the shooting sound effect


        // Turn on our line renderer
        rb.AddForce(Vector2.up * 0.02f, ForceMode2D.Impulse);

        //Wait for .07 seconds
        yield return shootM;


        // Deactivate our line renderer after waiting

        dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotateTraget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateTraget, Time.deltaTime * rotationSpeed);
        rb.velocity = new Vector2(dir.x * 1, dir.y * 1);

    }

    // Update is called once per frame



     
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hittable"))
        {
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Stuff"))
        {
            Destroy(gameObject);
        }
    }
}
