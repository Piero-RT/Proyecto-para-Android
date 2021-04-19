using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    public bool isEntry;
    public float distance = 0.3f;
    private float livingTime = 2.7f;
    // Start is called before the first frame update
    void Start()
    {
        if (isEntry == false)
        {
            destination = GameObject.FindGameObjectWithTag("EntryPortal").GetComponent<Transform>();
        }
        else if(isEntry == true)
        {
            destination = GameObject.FindGameObjectWithTag("ExitPortal").GetComponent<Transform>();
        }
        Destroy(gameObject, livingTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > distance*0.5)
        {
            collision.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
