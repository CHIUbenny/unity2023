using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertouch : MonoBehaviour
{
    public GameObject bomb;
    public static GameObject flower;
    //private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        flower= GameObject.Find("StrangeFlower");
        
        
    }

    // Update is called once per frame
    void Update()
    {

      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(bomb, transform.position, transform.rotation);
            
            Destroy(gameObject);
            //gameObject.SetActive(false);
            //IdleEvent.disappear = true;
        }

    }
}
