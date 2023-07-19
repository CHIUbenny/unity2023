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

    
    private ListObjData listObj;
    private void Awake()
    {
        gameManager = this;       
    }
    public void Initgameobjectpool(int count,Object obj)
    {
        listObj = new ListObjData();
        listObj.objdata = obj;
        listObj.OLdata = new List<GameObjData>();
        for(int i = 0; i < count;i++)
        {
            GameObject go = GameObject.Instantiate(obj,transform) as GameObject;
            GameObjData god = new GameObjData();
            god.gobj= go;
            god.gousing =false;
            god.id = listObj.OLdata.Count;
            go.SetActive(god.gousing);
            listObj.OLdata.Add(god);

        }
    }
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
