using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjData
{
    public GameObject gobj;
    public bool gousing;
    public int id;
}
public class ListObjData
{
    public Object objdata;
    public List<GameObjData> OLdata;
}

public class objectpool : MonoBehaviour
{
    private static objectpool gameManager = null;
    public static objectpool Instance() { return gameManager; }

    public class BNPCBarData
    {
        public GameObject sourceTarget;
        public BNPCbar bar;
    }
   // public Camera mainCamera;
    public GameObject uibar;
    private ListObjData listObj;
    //private List<BNPCBarData> barlist = new List<BNPCBarData>();
    public Transform UIbargroup;
    private void Awake()
    {
        gameManager = this;       
    }
    public void Initgameobjectpool(int count,Object[] obj)
    {
        listObj = new ListObjData();
        //listObj.objdata = obj;
        listObj.OLdata = new List<GameObjData>();
        for(int i = 0; i < count;i++)
        {
            int Objectrange = Random.Range(0, obj.Length);
            listObj.objdata = obj[Objectrange];
            GameObject go = GameObject.Instantiate(obj[Objectrange],transform) as GameObject;
            GameObjData god = new GameObjData();
            god.gobj= go;
            god.gousing =false;
            god.id = listObj.OLdata.Count;
            go.SetActive(god.gousing);
            listObj.OLdata.Add(god);

            /*BNPCBarData bardata = new BNPCBarData();
            bardata.sourceTarget = null;
            bardata.bar = Instantiate(uibar).GetComponent<BNPCbar>();
            bardata.bar.transform.SetParent(UIbargroup);
            bardata.bar.UpdateBarValue(1.0f);
            bardata.bar.gameObject.SetActive(false);
            barlist.Add(bardata);*/
        }
    }

   /* public BNPCbar BarToTarget(GameObject target)
    {
        int icount = barlist.Count;
        for(int i = 0;i < icount;i++)
        {
            if (barlist[i].sourceTarget == null)
            {
                barlist[i].sourceTarget = target;
                barlist[i].bar.gameObject.SetActive(true);
                barlist[i].bar.UpdateBarValue(1f);
                return barlist[i].bar;
            }
        }
        BNPCBarData bardata = new BNPCBarData();
        bardata.sourceTarget = null;
        bardata.bar = Instantiate(uibar).GetComponent<BNPCbar>();
        bardata.bar.transform.SetParent(UIbargroup);
        bardata.bar.UpdateBarValue(1.0f);
        bardata.bar.gameObject.SetActive(false);
        barlist.Add(bardata);
        return bardata.bar;
    }
    public void RemoveBarByTarget(GameObject target)
    {
        int iCount = barlist.Count;
        for (int i = 0; i < iCount; i++)
        {
            if (barlist[i].sourceTarget == target)
            {
                barlist[i].sourceTarget = null;
                barlist[i].bar.gameObject.SetActive(false);
                //barList.RemoveAt(i);
                break;
            }
        }
    }*/
    public GameObjData LoadObjFromPool(bool bload)
    {
        int count  = listObj.OLdata.Count;
        int i =0;
        for(i = 0; i < count; i++)
        {
            GameObjData obj = listObj.OLdata[i];
            if(obj.gousing)
            {
                continue;
            }
            obj.gousing = true;
            return obj;
        }
        if( bload)
        {
            GameObject go = GameObject.Instantiate(listObj.objdata, transform) as GameObject;
            GameObjData god = new GameObjData();
            god.gobj = go;
            god.gousing = true;
            god.id = listObj.OLdata.Count;
            go.SetActive(false);
            listObj.OLdata.Add(god);
            return god;
        }
        return null;
    }
    public void UnLoadObjFromPool(GameObjData god)
    {
        int id = god.id;
        listObj.OLdata[id].gousing = false;
        listObj.OLdata[id].gobj.SetActive(false);
        /*int count = listObj.OLdata.Count;
        int i = 0;
        for (i = 0; i < count; i++)
        {
            GameObjData obj = listObj.OLdata[i];
            if (obj.gobj==god)
            {

                obj.gobj.SetActive(false);
                obj.gousing = false;
                return;
            }
        }*/
    }

}
