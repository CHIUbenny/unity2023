using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class NPCData
{
    public float fHp;
    public List<int> iBuffers;
}

[System.Serializable]
public class ItemData
{
    public string sName;
    public float fAddHp;
}

[System.Serializable]
public class MyLevelData
{
    public string sLevel;
    public float fHp;
    public float fMp;
    public int iLV;
    public Vector3 pos;
    public List<int> iItems;
    [SerializeField]
    public List<ItemData> pDatas;
    [SerializeField]
    public List<NPCData> npcdict;
    // public Dictionary<int, NPCData> npcdict;
}
public class JsonTest {

	public static void SaveMyLevel(string sFile)
    {
        MyLevelData lData = new MyLevelData();
        lData.sLevel = "Boss";
        lData.fHp = 100.0f;
        lData.fMp = 10.0f;
        lData.iLV = 2;
        lData.pos = Vector3.one;
        lData.iItems = new List<int>();
        lData.iItems.Add(1);
        lData.iItems.Add(2);
        lData.iItems.Add(1);
        lData.pDatas = new List<ItemData>();
        ItemData d = new ItemData();
        d.sName = "補血藥";
        d.fAddHp = 10.0f;
        lData.pDatas.Add(d);

        d = new ItemData();
        d.sName = "強力補血藥";
        d.fAddHp = 100.0f;
        lData.pDatas.Add(d);
        d = new ItemData();
        d.sName = "毒藥";
        d.fAddHp = -100.0f;
        lData.pDatas.Add(d);
       // lData.npcdict = new Dictionary<int, NPCData>();
        lData.npcdict = new List<NPCData>();
        NPCData nData = new NPCData();
        nData.fHp = 50.0f;
        nData.iBuffers = new List<int>();
        nData.iBuffers.Add(1);
        nData.iBuffers.Add(2);
        NPCData nData2 = new NPCData();
        nData2.fHp = 80.0f;
        nData2.iBuffers = new List<int>();
        nData2.iBuffers.Add(1);
        nData2.iBuffers.Add(3);
        lData.npcdict.Add(nData);
        lData.npcdict.Add(nData2);
        // lData.npcdict.Add(10, nData);
        //lData.npcdict.Add(20, nData2);
        string saveString = JsonUtility.ToJson(lData);
        File.WriteAllText(sFile, saveString);

        //StreamWriter file = new StreamWriter(sFile);
        //file.Write(saveString);
        //file.Close();
    }

    public static MyLevelData LoadMyLevel(string sFile)
    {
        string sText = File.ReadAllText(sFile);

        //StreamReader file = new StreamReader(sFile);
        //string loadJson = file.ReadToEnd();
        //file.Close();


        MyLevelData lData = JsonUtility.FromJson<MyLevelData>(sText);
        Debug.Log(lData.sLevel);
        Debug.Log(lData.fMp);
        Debug.Log(lData.fHp);
        Debug.Log(lData.iLV);
        Debug.Log(lData.pos);
        Debug.Log(lData.npcdict.Count);
        foreach (NPCData n in lData.npcdict)
        {
            Debug.Log(n.fHp);
        }
        return lData;

        //StreamWriter file = new StreamWriter(sFile);
        //file.Write(saveString);
        //file.Close();
    }
}
