using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headrot : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform head;
    public Transform looksp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        Vector3 ver = looksp.position - head.position;
        Vector3 modelf= transform.forward;
        Vector3 hver = ver;
        hver.y = 0;
        hver.Normalize();
        float vAngle = Vector3.Angle(ver,hver);
        float hAngle = Vector3.Angle(modelf,hver);
        if (hAngle > 30)
        {
            hAngle = 30;
        }
        float fDot = Vector3.Dot(transform.right,hver);
        if(fDot < 0) 
        {
            hAngle = -hAngle;
        }

        if (vAngle > 20)
        {
            vAngle = 20;
        }
        if (ver.y > 0)
        {
            vAngle = -vAngle;
        }
        //Debug.Log("lookupdowe¨¤«×" + vAngle);
        // new vector = quaternion * old vector
        Quaternion qRot = Quaternion.Euler(0,hAngle,0);
        Vector3 vr = qRot * modelf;
        Vector3 vra = Vector3.Cross(transform.up, vr);
        Quaternion qRot1 = Quaternion.AngleAxis(vAngle,vra);
        ver = qRot1 * vr;


        head.forward = ver;
        head.Rotate(0,0,-90,Space.Self);
        head.Rotate(90, 0, 0, Space.Self);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(head.position,looksp.position);
    }
}
