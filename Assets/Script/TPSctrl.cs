using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets._2D;
using UnityStandardAssets.Utility;

public class TPSctrl : MonoBehaviour
{
    public CharacterController cc;
    public float forwardSpeed = 2.0f;
    public float runSpeed = 5f;
    private float moveTime = 0f;
    private float RunningStart = 1.2f;
    public float rotateSpeed = 2.0f;


    public TPSTarget lookTarget;
    public float followDistance;
    public float scrollSpeed;
    public Transform camTransform;
    private Vector3 horizontalDirection;
    private float verticalDegree = 0.0f;
    public float camCtrlSensitive = 1.0f;
    public LayerMask colMasl;
    private Animator player2;
    public Transform firstPos;
    int weapon = 0;


    // Start is called before the first frame update
    void Start()
    {
        player2 = GetComponentInChildren<Animator>();
        horizontalDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        bool firstPlayer = (followDistance == 0f);
        float fMoveV = Input.GetAxis("Vertical");
        float fMoveH = Input.GetAxis("Horizontal");
        player2.SetFloat("walk", fMoveV);
        player2.SetFloat("RL", fMoveH);
        
        Vector3 firstmove = transform.forward*fMoveV;
        bool isMove = (fMoveV > 0.1f||fMoveV<-0.1f);
        moveTime = isMove ? (moveTime + Time.deltaTime) : 0f;
        
       
        bool isRun = (RunningStart <= moveTime);
        float attack = Input.GetAxis("Fire1");
        player2.SetFloat("attack", attack);
        
        if (Input.GetKey(KeyCode.R)) 
        {
            switch(weapon)
            {
                case 0:
                    weapon ++;
                    player2.SetInteger("weapon", weapon);
                    break; 
                case 1:
                    weapon--;
                    player2.SetInteger("weapon", weapon);
                    break;
            }
        }
        if (firstPlayer) 
        {
            if (isMove)
            {
                transform.Rotate(0, fMoveH * rotateSpeed, 0);
                float moveSpeed = isRun ? runSpeed : forwardSpeed;
                firstmove *= moveSpeed;
            }
            player2.SetBool("run", isRun);
            cc.SimpleMove(firstmove);
        }
        else
        {
            if (fMoveV != 0 /*|| fMoveH != 0*/)
            {
                Vector3 vCamF = camTransform.forward;
                vCamF.y = 0;
                vCamF.Normalize();
                Vector3 vCamR = camTransform.right;
                Vector3 VecF = fMoveV * vCamF;
                Vector3 VecR = fMoveH * vCamR;
                VecF = VecF + VecR;
                float inputSpeed = VecF.magnitude;

                if (inputSpeed > 1.0)
                {
                    inputSpeed = 1.0f;
                }
                Vector3 Player2move = VecF * inputSpeed;
                if (isMove)
                {
                    float moveSpeed = isRun ? runSpeed : forwardSpeed;
                    Player2move *= moveSpeed;
                }
                transform.forward = Vector3.Lerp(transform.forward, VecF, 0.2f);
                player2.SetBool("run", isRun);
                cc.SimpleMove(Player2move);
            }
        }
        //以上為腳色位移控制
        //transform.Rotate(0, fMoveH, 0);
        //cc.SimpleMove(transform.forward * fMoveV * moveSpeed);

        MoveCam();
        lookTarget.UpdateTarget();
        float mouseX =Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        if (firstPlayer) { firstplay(); }
        else
        {
            verticalDegree = verticalDegree + mouseY * camCtrlSensitive; //滑鼠Y軸位移量更新
            if (verticalDegree > 60.0f)
            {
                verticalDegree = 60.0f;
            }
            else if (verticalDegree < -30.0f)
            {
                verticalDegree = -30.0f;
            }
            //鏡頭X軸旋轉範圍
            horizontalDirection = Quaternion.Euler(0, mouseX * camCtrlSensitive, 0) * horizontalDirection;
            //鏡頭Y軸旋轉用四元數尤拉角運算出向量值
            Vector3 vAxis = Vector3.Cross(Vector3.up, horizontalDirection);
            //以向量up與視角前進方向外積
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

            camTransform.position = camPos;
            camTransform.LookAt(lookTarget.transform.position);
        }
    }
 

    public void firstplay()
    {
        
        camTransform.forward = firstPos.forward;
        camTransform.position = Vector3.Lerp(camTransform.position, firstPos.position, 1.0f);
    }

    public void MoveCam()
    {
        Vector2 vScroll = Input.mouseScrollDelta;
        followDistance -= vScroll.y * scrollSpeed;
        if (followDistance < 0)
        {
            followDistance = 0.0f;
    
        }
        else if (followDistance > 10.0f)
        {
            followDistance = 10.0f;
        }

    }
}
