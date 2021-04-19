using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaSmokeEffect : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer _spriteRPublic { get { return _spriteR; } }
    public SpriteRenderer _spriteR;

    // Start is called before the first frame update
    void Start()
    {
        int chooserandom = Random.Range(0, sprites.Length);
        _spriteR.sprite = sprites[chooserandom];
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.localRotation = Quaternion.Euler(0, 180, 0);
        //}
        //else
        //{
        //    transform.localRotation = Quaternion.Euler(0, 0, 0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
