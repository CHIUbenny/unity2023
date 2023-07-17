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
     public Object Loadbomb(string sName)
    {
        Object o = Resources.Load(sName);
        return o;
    }
}
