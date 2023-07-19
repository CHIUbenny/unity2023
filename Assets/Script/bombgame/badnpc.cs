using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class badnpc : MonoBehaviour
{
    private float rf = 5f;
    private float Oriqua;
    // Start is called before the first frame update
    void Start()
    {
        Oriqua = transform.eulerAngles.y;
        
        Debug.Log("目前的角度"+Oriqua);
    }

    // Update is called once per frame
    void Update()
    {
       // Oriqua = transform.eulerAngles.y;
        //if (Oriqua <= 90) { transform.Rotate(0, rf, 0); } 
        
            
        

    }
}
