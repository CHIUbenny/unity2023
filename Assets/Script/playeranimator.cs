using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private Vector3 ver;
    public float forwardSpeed = 1.0f;
    public float rotateSpeed = 2f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       float v= Input.GetAxis("Vertical");
       anim.SetFloat("speed",v);
       float h = Input.GetAxis("Horizontal");
       anim.SetFloat("RL",h);

       ver =new Vector3(0,0,v);
       ver = transform.TransformDirection(ver);
       if(v>0.1){
        ver *= forwardSpeed;
        transform.Rotate (0, h * rotateSpeed, 0);
       }

       transform.localPosition += ver*Time.deltaTime;
       /*if(v>0){
       transform.Rotate (0, h * rotateSpeed, 0);
       }*/
    }
}
