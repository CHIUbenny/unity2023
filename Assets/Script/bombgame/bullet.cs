using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 500);

    }
    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "badnpc") 
        {
            return;
        }
        else {
           
            Destroy(gameObject,2f); }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
