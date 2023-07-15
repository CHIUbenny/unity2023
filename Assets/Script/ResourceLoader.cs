using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{
    private static ResourceLoader _instance = null;
    public static ResourceLoader Instance() { return _instance; }
    // Start is called before the first frame update
    public void Init()
    {
        _instance = this;
    }

    public GameObject LoadGameObject(string sName)
    {
        GameObject o = Resources.Load(sName) as GameObject;
        return o;
    }

    public Object LoadObject(string sName)
    {
        Object o = Resources.Load(sName);
        return o;
    }

    public Object[] LoadAllObject(string sName)
    {
        Object[] o = Resources.LoadAll(sName);
        foreach (Object oo in o)
        {
            Debug.Log(oo.name);
        }
        return o;
    }

    public Texture2D LoadTextureObject(string sName)
    {
        Texture o = Resources.Load<Texture>(sName);
        Debug.Log(o);
        return o as Texture2D;
    }

    public string LoadTextObject(string sName)
    {
        TextAsset o = Resources.Load<TextAsset>(sName);
        Debug.Log(o.text);
        return o.text;
    }


    public IEnumerator LoadGameObjectAsync(string sName, System.Action<Object> act)
    {
        ResourceRequest rr = Resources.LoadAsync(sName);
        yield return rr;

        if (rr.isDone && rr.asset != null)
        {
            act(rr.asset);
        }
        /* int aa = 0;
         Debug.Log("aa " + aa);
         yield return 0;
         aa++;
         Debug.Log("aa " + aa);
         yield return new WaitForSeconds(1.0f);
         aa++;
         Debug.Log("aa " + aa);
         Debug.Log("LoadGameObjectAsync finish");*/
    }
}