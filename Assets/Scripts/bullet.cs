using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject player;
    private Transform playerTrans;

    public Sprite[] sprites; //En el inspector añadir los 3 sprites de Bullet. Poner el tamaño a 3
    public SpriteRenderer spriteRPublic { get { return _spriteR; } }
    public SpriteRenderer _spriteR;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        int chooserandom = Random.Range(0, sprites.Length);
        _spriteR.sprite = sprites[chooserandom];
        if (playerTrans.localRotation.y >= 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
