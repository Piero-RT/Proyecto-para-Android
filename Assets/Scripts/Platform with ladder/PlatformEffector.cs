using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffector : MonoBehaviour
{
    
    private PlatformEffector2D pEffector;

    public float waittime;
    public Joystick joystick;
    private float verticalMove;

    private void Awake()
    {
        
        pEffector = GetComponent<PlatformEffector2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void FixedUpdate()
    {
        verticalMove = joystick.Vertical;
        
    }
    // Update is called once per frame
    void Update()
    {
       
        //if (verticalMove < 0)
        //{
        //    waittime = 0.5f;
        //}
        if (verticalMove < 0)
        {
            //if (waittime <= 0)
            //{
                pEffector.rotationalOffset = 180f;
                waittime = 0.5f;
            //}
        }
        if (verticalMove > 0)
        {
            pEffector.rotationalOffset = 0;
        }
    }
  
}
