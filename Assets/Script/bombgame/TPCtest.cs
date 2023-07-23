using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class TPCtest : MonoBehaviour
{
    public static TPCtest Instance;
    public Transform lookTarget;
    public float followDistance;
    public float scrollSpeed;
    private Vector3 horizontalDirection;
    private float verticalDegree = 0.0f, horizontalDegree = 0.0f;
    public float camCtrlSensitive = 1.0f;
    public LayerMask colMasl;
    public float hight = 1.3f;
    public Transform player;
    Transform frontPos;
    Transform jumpPos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    // Start is called before the first frame update
    void Start()
    {
        horizontalDirection = player.forward;
        frontPos= GameObject.Find("FrontPos").transform;
        if (GameObject.Find("JumpPos"))
            jumpPos = GameObject.Find("JumpPos").transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lookTarget.position = player.position+lookTarget.up*hight;
        MoveCam();
        //lookTarget.UpdateTarget();
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
       verticalDegree = verticalDegree + mouseY * camCtrlSensitive; //滑鼠Y軸位移量更新
        if (verticalDegree > 60.0f)
        {
            verticalDegree = 60.0f;
        }
        else if (verticalDegree < -30.0f)
        {
            verticalDegree = -30.0f;
        }

        horizontalDegree = mouseX * camCtrlSensitive;
        //鏡頭X軸旋轉範圍
        horizontalDirection = Quaternion.Euler(0, horizontalDegree, 0) * horizontalDirection;
        //鏡頭Y軸旋轉用四元數尤拉角運算出向量值
        Vector3 vAxis = Vector3.Cross(player.up, horizontalDirection);
        //以向量up與視角前進方向外積
        Vector3 finalVector = Quaternion.AngleAxis(verticalDegree, vAxis) * horizontalDirection;
        Vector3 moveplayer = Quaternion.AngleAxis(verticalDegree, Vector3.Cross(player.up,player.forward))*player.forward;
        Vector3 CamOri = lookTarget.transform.position - moveplayer * followDistance;
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
        //Vector3 camPos = lookTarget.transform.position - player.forward * followDistance;
        if (IdleEvent.Instance().isMove)
        {
            transform.position = Vector3.Lerp(transform.position,CamOri,0.2f);
            
        }
        else
        {
            
            transform.position = Vector3.Lerp(transform.position, camPos, 0.2f); 
        }
        transform.LookAt(lookTarget.position);
        if (IdleEvent.win) { setCameraPositionFrontView(); }
        else if (Input.GetButton("Fire2"))
        {   
            setCameraPositionJumpView();
        }
    }

    void setCameraPositionFrontView()
    {
       
        transform.position = frontPos.position;
        transform.forward = frontPos.forward;
    }
    void setCameraPositionJumpView()
    {
      
        transform.position = Vector3.Lerp(transform.position, jumpPos.position, Time.fixedDeltaTime * 3f);
        transform.forward = Vector3.Lerp(transform.forward, jumpPos.forward, Time.fixedDeltaTime * 3f);
    }
    public void MoveCam()
    {
        Vector2 vScroll = Input.mouseScrollDelta;
        followDistance -= vScroll.y * scrollSpeed;
        if (followDistance < 2)
        {
            followDistance = 2.0f;

        }
        else if (followDistance > 8.0f)
        {
            followDistance = 8.0f;
        }

    }
}
