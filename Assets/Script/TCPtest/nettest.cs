using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Linq;
using System.Net;
using System;
using UnityEditor.PackageManager;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor.TextCore.Text;
using UnityEditor.VersionControl;

public class MessageData
{
    public string name;
    public string message;
}

public class nettest : MonoBehaviour
{   public static nettest instance;
    public TMPro.TMP_InputField addressInput = null;
    public TMPro.TMP_InputField nameInput = null;
    public TMPro.TMP_InputField messageInput = null;
     ChatClient client = new ChatClient();
    bool bOK;
    //public TMPro.TMP_Text tack ;
    public List<TMPro.TMP_Text>playerdisplaymesg;
    string sName;

    private List<MessageData> pmessage = new List<MessageData>();
    private List<MessageData> latamessage = new List<MessageData>();
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InvokeRepeating("GoRun", 1f , 1f);
        
    }
    

    // private IEnumerator
    // Update is called once per frame
    void Update()
    {
       
        

    }
    public void GoRun()
    {
     if (bOK) 
            {

                client.Run(); 
            }
    }
    public void CheckButton()
        
    {
        
         string sd =addressInput.text;
         bOK = client.Connect(sd, 4099);
         sName = nameInput.text;
        
        if (bOK)
        {
            client.SendName(sName);
        }
    }
    public void SendMessage()
    {
        if (bOK)
        {
            string sMessage = messageInput.text;
            client.SendBroadcast(sMessage);
            Addmessage( sName,sMessage);
            //tack1.text += sName+" said:"+sMessage+"\n";

        }
    }
    public void Addmessage(string pname,string str)
    {
        MessageData pmsg = new MessageData();
        pmsg.name = pname;
        pmsg.message = str;
        pmessage.Add(pmsg);
        if (pmessage.Count>10)
        {
            pmessage.RemoveAt(0);
        }
        //Debug.Log("s:" + pmsg.name+"\t"+pmsg.message);
        RefreshPMessage();
    }
    public void getLatestMessages(List<MessageData> outDatas, int iWant)
    {
        int iLastID = pmessage.Count -1;
        int iWantProcessCount = iWant;
        for (int i = iLastID; i >= 0; i--)
        {
            outDatas.Add(pmessage[i]);
            iWantProcessCount--;
            if (iWantProcessCount <= 0)
            {
                break;
            }
        }
    }

    public void RefreshPMessage()
    {
        int TMPdisplay = playerdisplaymesg.Count;
        latamessage.Clear();
        getLatestMessages(latamessage, TMPdisplay);
        int icount = latamessage.Count;
        
        for (int i = 0; i < icount; i++)
        {
            //Debug.Log("df:" + pmessage[i].name + ":" + pmessage[i].message);

            playerdisplaymesg[i].text = latamessage[i].name + " said:" + latamessage[i].message; 

        }
        for (int i = icount; i < TMPdisplay; i++)
        {
            playerdisplaymesg[i].text = "";
        }
    }



   
}
