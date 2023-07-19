using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class TPCtest : MonoBehaviour
{
    
    public TPSTarget lookTarget;
    public float followDistance;
    public float scrollSpeed;
    private Vector3 horizontalDirection;
    private float verticalDegree = 0.0f;
    public float camCtrlSensitive = 1.0f;
    public LayerMask colMasl;
    public float hight = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookTarget.UpdateTarget();
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        verticalDegree = verticalDegree + mouseY * camCtrlSensitive; //�ƹ�Y�b�첾�q��s
        if (verticalDegree > 60.0f)
        {
            verticalDegree = 60.0f;
        }
        else if (verticalDegree < -30.0f)
        {
            verticalDegree = -30.0f;
        }
        //���YX�b����d��
        horizontalDirection = Quaternion.Euler(0, mouseX * camCtrlSensitive, 0) * horizontalDirection;
        //���YY�b����Υ|���ƤשԨ��B��X�V�q��
        Vector3 vAxis = Vector3.Cross(Vector3.up, horizontalDirection);
        //�H�V�qup�P�����e�i��V�~�n
        Vector3 finalVector = Quaternion.AngleAxis(verticalDegree, vAxis) * horizontalDirection;

        RaycastHit rh;
        Vector3 camPos = Vector3.zero;
        if (Physics.SphereCast(lookTarget.transform.position, 0.2f, -finalVector, out rh, followDistance + 0.2f, colMasl))
        //if (Physics.Raycast(lookTarget.transform.position, -finalVector, out rh, followDistance, colMasl))
        {
            //MoveCam();
            camPos = lookTarget.transform.position - finalVector * (rh.distance - 0.1f);
            //camPos = rh.point + finalVector*0.1f;
        }
        else
        {
            //MoveCam();
            camPos = lookTarget.transform.position - finalVector * followDistance;
        }

        transform.position = camPos;
        transform.LookAt(lookTarget.transform.position);
        transform.forward = lookTarget.transform.forward;
    }
    
}
