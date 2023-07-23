using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badnpc : MonoBehaviour
{
    private float rf = 5f;
    private float Oriqua;
    
    // Start is called before the first frame update
    void Start()
    {
        Oriqua = transform.eulerAngles.y;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Return))
        {
            instBadNPC.Instance().Removebadnpc(this.gameObject);
        }*/




    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "sword")
        {
            Debug.Log("aaaa");
            instBadNPC.Instance().Removebadnpc(this.gameObject);
        }
    }

}
