using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourcetest 
{
    public static Resourcetest InstanceResource=null;
  public void Ingo()
    {
        InstanceResource = this;
    }
    public Object[] LoadAllbomb(string sName)
    {
        Object[] bomb = Resources.LoadAll(sName);
        return bomb ;
    }
     public Object LoadObject(string sName)
    {
        Object o = Resources.Load(sName);
        return o;
    }
    public IEnumerator LoadGameObjectAsync(string sName, System.Action<Object> act)
    {
        ResourceRequest rr = Resources.LoadAsync(sName);
        yield return rr;

        if (rr.isDone && rr.asset != null)
        {
            act(rr.asset);
        }
    }
  }
