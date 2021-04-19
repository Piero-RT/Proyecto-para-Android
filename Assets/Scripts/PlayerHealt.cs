using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public int totalHealth = 10;
    private int health;
    private SpriteRenderer _renderer;
    

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }
    
    public void AddDamage(int amount)
    {
        health = health - amount;
        
        StartCoroutine("VisualFeedBack");
        if (health <= 0)
        {
            health = 0;
        }
        Debug.Log("Se Daño" + health);
    }
    public void AddHealth ( int amount)
    {
        health = health + amount;
        if (health > totalHealth)
        {
            health = totalHealth;
        }
        Debug.Log("vida" + health);
    }

    private IEnumerator VisualFeedBack()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _renderer.color = Color.white;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
