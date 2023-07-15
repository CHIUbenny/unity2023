using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godViewMouse : MonoBehaviour
{
    public float movespeed = 5f;
    public float runspeed = 5f;
    private Camera cam;
    public LayerMask iLayerMask;
    private Vector3 sPos;
    private Ray ray;
    private float MoveTime = 0;
    private float RunningStart = 1f;

    public Animator player1;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }
    private void MoveForward(Vector3 dir, float fMoveAmount)
    {
        // Vector3 vOriDir = dir;
        dir.y = 0.0f;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.2f);
        Vector3 moveTarget = transform.position + dir * fMoveAmount;
        Vector3 moveTargetUp = moveTarget;
        moveTargetUp.y += 1.0f;
        RaycastHit rh;
        if (Physics.Raycast(moveTargetUp, -Vector3.up, out rh, 1.2f, iLayerMask))
        {
      
            transform.position = rh.point;
         
            Debug.Log("hit");
        }
        else
        {
            Debug.Log("no hit");
        }
       

    }
    // Update is called once per frame
    void Update()
    {
        
        mouseTouch(ref sPos,ref ray);
        Vector3 vDir;
        float fMoveAFrame;
        RaycastHit rh,rh2;

        
            /*if (Input.GetMouseButton(0))
            {
                //player1.SetBool("move",true);
                sPos = Input.mousePosition;
                ray = cam.ScreenPointToRay(sPos);

                if (Physics.Raycast(ray,out rh, 1000f, iLayerMask))
                {
                     vDir = rh.point - transform.position;
                     fMoveAFrame = movespeed * Time.deltaTime;
                    if (vDir.magnitude < fMoveAFrame)
                    {
                        transform.position = rh.point;
                    }
                    else
                    {
                        vDir.Normalize();
                        MoveForward(vDir, fMoveAFrame);
                    }
                }
            }
            else { //player1.SetBool("move",false);
                   }*/

        if (Physics.Raycast(ray, out rh, 1000f, iLayerMask))
        {
            vDir = rh.point - transform.position;
            //Debug.Log("556"+vDir.magnitude);
            player1.SetFloat("move", vDir.magnitude);
            bool ismove = (vDir.magnitude > 0.1);
            MoveTime = ismove ? (MoveTime+Time.deltaTime) : 0;
            bool isRun = (MoveTime > RunningStart);
            fMoveAFrame = isRun ? runspeed * Time.deltaTime : movespeed * Time.deltaTime;



            if (vDir.magnitude < fMoveAFrame)
            {
                transform.position = rh.point;
               
            }
            else
            {
                vDir.Normalize();
                MoveForward(vDir, fMoveAFrame);
                
            }
            player1.SetBool("run", isRun);
        }
        
        
 main.Instance().camCtrl.MoveCam();
    }
    private void mouseTouch(ref Vector3 sPos, ref Ray ray)
    {
        if (Input.GetMouseButton(0))
        {
            sPos = Input.mousePosition;
            ray = cam.ScreenPointToRay(sPos);
        }
    }
    
}
