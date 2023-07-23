using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instBadNPC : MonoBehaviour
{
    private static instBadNPC gameManager = null;
    public static instBadNPC Instance() { return gameManager; }

    private GameObject badNPCObject = null;
    private List<GameObjData> badNPC = new List<GameObjData>();
    public int Objectnum;
    Resourcetest r = new Resourcetest();
    // Start is called before the first frame update
    private void Awake()
    {
        
        gameManager = this;
        r.Ingo();
        

    }
    void Start()
    {
        badNPCObject = r.LoadObject("testbomb2/badnpc")as GameObject;
        objectpool.Instance().Initgameobjectpool(10,badNPCObject);

        GenerateBadnpc(Objectnum);

       
    }

    // Update is called once per frame
    void Update()
    {

      
            
        
    }
    public void Removebadnpc(GameObject go)
    {
        objectpool pool = objectpool.Instance();
        for(int i=0;i<badNPC.Count;i++) 
        {
            GameObjData objData = badNPC[i];
            if(objData.gobj==go)
            {
                badNPC.RemoveAt(i);
                pool.UnLoadObjFromPool(objData);
            }
        }
    }
    private void GenerateBadnpc(int num)
    {
        if (badNPC == null)
        {
            badNPC=new List<GameObjData>();
        }
       objectpool pool = objectpool.Instance();
        for(int i = 0; i < num; i++)
        {
            GameObjData objData = pool.LoadObjFromPool(true);
            GameObject insta = objData.gobj;
            Vector3 vdir = new Vector3(Random.Range(-5.0f, 5.0f), 0f, Random.Range(-5.0f, 5.0f));
            if (vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            insta.transform.position = transform.position+vdir*Random.Range(10f,21f);
            insta.transform.Rotate(0,Random.Range(0f, 181f), 0);
            insta.SetActive(true);
            badNPC.Add(objData);
           
        }
    }
}
