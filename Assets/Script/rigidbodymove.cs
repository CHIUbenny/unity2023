using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rigidbodymove : MonoBehaviour
{

    public Rigidbody rb;
    public float movespeed = 2f;
    public Transform camTra;
    public float v = 0f;
    public float h = 0f;
    public bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        v = Input.GetAxis("Vertical");
         h = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        Vector3 mDir = Vector3.zero;
        Vector3 oldv = rb.velocity;

        if (v != 0 || h != 0)
        {
            if (v != 0)
            {
                mDir = camTra.transform.forward * v;
                mDir.y = 0f;
            }
            if (h != 0)
            {
                Vector3 temp = camTra.transform.right * h;
                temp.y = 0f;
                mDir += temp;
            }
            transform.forward = mDir;
            move = transform.forward * movespeed;

        }
        move.y = oldv.y;
        if (jump == true)
        {
            move += (Vector3.up * 10f);
            jump = false;
        }
        rb.velocity = move;

    }
}
