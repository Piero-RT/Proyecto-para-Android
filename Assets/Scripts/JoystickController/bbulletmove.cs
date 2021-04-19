using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bbulletmove : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = direction.normalized * speed;
        _rb.velocity = movement;
        direction = new Vector2(1, 0);
    }
}
