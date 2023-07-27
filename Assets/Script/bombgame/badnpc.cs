using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class badnpc : MonoBehaviour
{
    private float rf = 20f;
    private float Oriqua;
    private float Maxhp;
    private float hp;
    private Material smaterial;
    //public BNPCbar nbar;
    public GameObject UIbar;
    public Image bar;
    private int RorL;
    bool x;
    public Transform muzzle;
    private GameObject bullet;
    float time = 0f;
    float ss = 0f;
    Resourcetest r = new Resourcetest();
    // Start is called before the first frame update
    private void Awake()
    {
        
        r.Ingo();
    }
    void Start()
    {
        //nbar = instBadNPC.Instance().RequestFloatingBar(gameObject);
        Oriqua = transform.eulerAngles.y;
        RorL = Random.Range(0, 2);
        bullet = r.LoadObject("bullet")as GameObject;
        hp = 100f;
        smaterial =  GetComponentInChildren<Renderer>().material;
        Maxhp = 100f;
        
            //InvokeRepeating("MuzzleShoot", 0.5f, 1f); 
        
    }

    // Update is called once per frame
    void Update()
    {
        BadnpcIdle(x);
        MuzzleRay();
        UIbarupdate();
        
    }
    public void BadnpcIdle(bool x)
    {
        float a, angle;
        switch (RorL)
        {

            case 0:
                x = true;
                transform.Rotate(0, rf * Time.deltaTime, 0);
                break;
            case 1:
                x = false;
                transform.Rotate(0, -rf * Time.deltaTime, 0);
                break;
                case 2:
                transform.Rotate(0, 0, 0);
                   time += Time.deltaTime;
                if (time > 2f)
                {
                    MuzzleShoot();
                    time = 0f;
                }
                break;
        }
        a = transform.eulerAngles.y;
        angle = Mathf.Abs(Oriqua - a);
        if (angle > 60f)
        {

            if (x)
            {
                RorL = 1;
            }
            else
            {
                RorL = 0;
            }

        }
    }
    
    public void BadnpcDamage(float nb)
    {
        hp -= nb;
        if (hp <=0)
        {
            hp = 0;
            instBadNPC.Instance().Removebadnpc(this.gameObject);
            //instBadNPC.Instance().ReleaseFlotingBar(gameObject);
        }
        if (hp > 50)
        {
            smaterial.color = new Color(10f, 20.0f, 0f);
        }
        else
        {
            smaterial.color = new Color(1.0f, 0.0f, 0.0f);
        }
        UIbarupdate();
        // nbar.UpdateBarValue(sd);
    }
    public void UIbarupdate()
    {
        float sd = hp / Maxhp;
        if (sd < 0)
        {
            sd = 0;
        }
        else if (sd > 1)
        {
            sd = 1;
        }
        bar.fillAmount = sd;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "sword")
        {
            Debug.Log("aaaa");
            BadnpcDamage(10f);
        }
    }
    public void MuzzleRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.forward,out hit, 3f))
        {
            
            if (hit.collider.transform.GetComponent<IdleEvent>() != null)
            {
                //Debug.Log("sss" + hit.collider.name);
                RorL = 2;
            }
            else
            {
               
                if (RorL == 2)
                {
                    
                    ss +=Time.deltaTime;
                    if (ss > 0.5f)
                    {
                        Debug.Log("aaa");
                        RorL = Random.Range(0, 2);
                        ss = 0;
                    }
                }
            }

        }
 
        Debug.DrawRay(muzzle.position, muzzle.forward * 3f,Color.cyan);
    }
    public void MuzzleShoot()
    {
        Instantiate(bullet, muzzle.position, muzzle.rotation,muzzle);
    }
    /*public void LateUpdate()
    {
        if (nbar != null)
        {
            nbar.UpdatePosition(instBadNPC.Instance().barmanger.mainCamera, transform.position, 2.0f);
        }
    }*/

}
