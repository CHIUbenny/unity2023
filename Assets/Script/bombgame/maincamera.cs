using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maincamera : MonoBehaviour
{
    public float smooth = 3f;       // カメラモーションのスムーズ化用変数
    Transform standardPos;          // the usual position for the camera, specified by a transform in the game
    Transform frontPos;         // Front Camera locater
    Transform jumpPos;
    Transform firstPos;

    // スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
    bool bQuickSwitch = false;  //Change Camera Position Quickly
    bool x = false;

    void Start()
    {
        // 各参照の初期化
        standardPos = GameObject.Find("CamPos").transform;

        if (GameObject.Find("FrontPos"))
            frontPos = GameObject.Find("FrontPos").transform;

        if (GameObject.Find("JumpPos"))
            jumpPos = GameObject.Find("JumpPos").transform;
        if (GameObject.Find("FirstPos"))
            firstPos = GameObject.Find("FirstPos").transform;
        //カメラをスタートする
        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
    }

    void FixedUpdate()  // このカメラ切り替えはFixedUpdate()内でないと正常に動かない
    {

        if (Input.GetButton("Fire1")||IdleEvent.win)
        {   // left Ctlr	
            // Change Front Camera
            setCameraPositionFrontView();
        }
        else if (Input.GetButton("Fire2"))
        {   //Alt	
            // Change Jump Camera
            setCameraPositionJumpView();
        }
        else if (Input.GetKey(KeyCode.Tab)||x==true)
        {
            setFirstView();
            if (Input.GetKeyDown(KeyCode.Tab)) { x = bFirstPos(x); }

           //Debug.Log(bFirstPos(x) + "  : " + x);
        }
        else
        {
            // return the camera to standard position and direction
            setCameraPositionNormalView();
        }
       
    }
    bool bFirstPos(bool fir)
    {
        if (fir) { return false; }
        return true;
    }

    void setCameraPositionNormalView()
    {
        if (bQuickSwitch == false)
        {
            // the camera to standard position and direction
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
        }
        else
        {
            // the camera to standard position and direction / Quick Change
            transform.position = standardPos.position;
            transform.forward = standardPos.forward;
            bQuickSwitch = false;
        }
    }

    void setCameraPositionFrontView()
    {
        // Change Front Camera
        bQuickSwitch = true;
        transform.position = frontPos.position;
        transform.forward = frontPos.forward;
    }
    void setFirstView()
    {
       
        bQuickSwitch = true;
        transform.position = firstPos.position;
        transform.forward = firstPos.forward;
    }

    void setCameraPositionJumpView()
    {
        // Change Jump Camera
        bQuickSwitch = false;
        transform.position = Vector3.Lerp(transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);
        transform.forward = Vector3.Lerp(transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);
    }
}

