using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlected : MonoBehaviour
{
    public enum Player {Caballero, Astronauta, Payaso, PigMan };
    public Player playerSelected;


    public Animator anim;
    public SpriteRenderer spriteRenderer;

    public RuntimeAnimatorController[] playerController;
    public Sprite[] playerRenderer;


    // Start is called before the first frame update
    void Start()
    {
        switch (playerSelected)
        {
            case Player.Caballero:
                spriteRenderer.sprite = playerRenderer[0];
                anim.runtimeAnimatorController = playerController[0];
                break;
            case Player.Astronauta:
                spriteRenderer.sprite = playerRenderer[1];
                anim.runtimeAnimatorController = playerController[1];
                break;
            case Player.Payaso:
                spriteRenderer.sprite = playerRenderer[2];
                anim.runtimeAnimatorController = playerController[2];
                break;
            case Player.PigMan:
                spriteRenderer.sprite = playerRenderer[3];
                anim.runtimeAnimatorController = playerController[3];
                break;
            default:
                break;
        }

    }


}
