using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSco : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform controlCamera;
    public float moveSpeed = 2f;
    public float rotateSpeed = 1.0f;
    public Transform cameraFollowPt;
    public Transform gunRoot;
    public Transform emitPoint;
    private float cameraH = 0.0f;
    private float focusDistance = 5.0f;
    public Object hitEffect;


    void Start()
    {
        Vector3 vec = controlCamera.position - transform.position;
        cameraH = vec.y;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float rotateGO= mouseX * rotateSpeed;
        transform.Rotate(0,rotateGO,0);
       

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 fv = transform.forward;
        controlCamera.Rotate(mouseY, 0, 0, Space.Self);
        controlCamera.Rotate(0, rotateGO, 0, Space.World);
       

        gunRoot.Rotate(mouseY, 0, 0, Space.Self);
        Vector3 rv = transform.right;
        Vector3 moveGO = (fv * v + h * rv) * moveSpeed * Time.deltaTime;
        transform.position += moveGO;
        controlCamera.position = Vector3.Lerp(controlCamera.position, cameraFollowPt.position, 1.0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray r = new Ray(controlCamera.position, controlCamera.forward);
            int iLayerMask = 1 << LayerMask.NameToLayer("pig") | 1 << LayerMask.NameToLayer("Terrain")| 1 << LayerMask.NameToLayer("Wall");
       
            RaycastHit rh = new RaycastHit();
            bool bHit = Physics.Raycast(r, out rh, 1000.0f, iLayerMask);

            //GameObject go = GameObject.Instantiate(lineEffect) as GameObject;
            //BulletLine bleffect = go.GetComponent<BulletLine>();
            //go.transform.position = emitPoint.position;
            if (bHit)
            {
                Debug.Log(rh.collider.name);
                Debug.Log(rh.point);
                /*GameObject gEffect = GameObject.Instantiate(hitEffect) as GameObject;
                gEffect.transform.position = rh.point;
                gEffect.transform.forward = rh.normal;
                if (rh.collider.tag == "pig")
                {
                    pig ey = rh.collider.gameObject.GetComponent<pig>();
                    ey.Damage(10.0f);
                }*/
                if (rh.distance < focusDistance)
                {
                    Vector3 tempPos = controlCamera.position + controlCamera.forward * focusDistance;
                    Vector3 emitDir = tempPos - emitPoint.position;
                    //go.transform.forward = emitDir;
                    emitDir.Normalize();
                    Ray r2 = new Ray(emitPoint.position, emitDir);
                    RaycastHit rh2 = new RaycastHit();
                    if (Physics.Raycast(r2, out rh2, 1000.0f, iLayerMask))
                    {

                       GameObject gEffect = GameObject.Instantiate(hitEffect) as GameObject;
                        gEffect.transform.position = rh2.point;
                        gEffect.transform.forward = rh2.normal;
                        if (rh2.collider.tag == "pig")
                        {
                            pig ey = rh.collider.gameObject.GetComponent<pig>();
                            ey.Damage(10.0f);
                         }
                    }
                    else
                    {
                        //bleffect.SetupLine(1000.0f, 0.1f);
                    }
                }
                else
                {
                    GameObject gEffect = GameObject.Instantiate(hitEffect) as GameObject;
                    gEffect.transform.position = rh.point;
                    gEffect.transform.forward = rh.normal;
                    //go.transform.forward = rh.point - emitPoint.position;
                   // bleffect.SetupLine(rh.distance, 0.1f);
                    if (rh.collider.tag == "pig")
                    {
                        GameObject g = rh.collider.gameObject;
                        g.SendMessage("Damage", 10.0f);
                        // enemy ey = g.GetComponent<pig>();
                        // ey.Damage(10.0f);

                    }
                }

            }
        }
    }
}
