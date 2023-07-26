using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BNPCbar : MonoBehaviour
{
    //public Image barImage;
    public Image bar;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public void UpdateBarValue(float value)
    {
        bar.fillAmount = value;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation= instBadNPC.Instance().barmanger.transform.rotation;
    }
   /* public void UpdatePosition(Camera mainCamera, Vector3 followPos, float height)
    {
        followPos.y += height;
        Vector3 sPos = mainCamera.WorldToScreenPoint(followPos);
        this.transform.position = sPos;
    }*/
}
