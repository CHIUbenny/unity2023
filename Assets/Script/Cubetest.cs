using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubetest : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed ;
    public float r = 1f;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Rotate(0, speed*Time.deltaTime, 0);
        if (Input.GetKey("d")) {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.position -= new Vector3(0,0, speed * Time.deltaTime);
        }
    }

     private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, r);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 3f);
    }
}
