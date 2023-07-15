using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertouch1 : MonoBehaviour
{
    public GameObject bomb;
    //public AudioSource audioPlayer;
   
   
    // Start is called before the first frame update
    void Start()
    {
        
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
            
        }
        

    }
}
