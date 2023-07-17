using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class instBomb : MonoBehaviour
{
    private static instBomb gameManager = null;
    public static instBomb Instance() { return gameManager; }


    // Start is called before the first frame update
    public Object[] bomb ;
    public Transform bomb50;
    public static int count;
    public float instime = 0.5f;
    public float x1, x2 ,z1,z2;
    public float y;
    public int countup;
    public Object[] bombx;
    public Transform[] x;
    public Transform[] xz;
    Resourcetest r = new Resourcetest();


    private void Awake()
    {
      gameManager = this;
        r.Ingo();
    
    }
    void Start()
    {
        
        
        bomb = r.LoadAllbomb("testbomb1/BombPfb");
        
        //r.Loadbomb("testbomb1/BombPfb/Bomb1");


        Debug.Log("bomb 陣列數目" + bomb.Length);
        bombx = r.LoadAllbomb("testbomb1/BombWallPfb");
        Debug.Log("bomb 陣列數目" + bombx.Length);
        count = 0;
        InvokeRepeating("InsBomb", instime, instime);
        
        InsBombx();
        
    }
    

    // Update is called once per frame
    void Update()
    {

    }
    public void InsBomb()
    {
   
        int RandomBomb = Random.Range(0,101);
        int n;
        if (RandomBomb <= 40) { n = 0; }else if (RandomBomb >= 90) { n = 2; } else { n=1; }
        if (count<countup) {
            Instantiate(bomb[n], new Vector3(Random.Range(x1, x2), y, Random.Range(z1, z2)), Quaternion.identity, bomb50);
            count++;
        }
      
    }
    public void InsBombx()
    {
        
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j <= 12; j+=2)
            {
                int bombwall = Random.Range(1,3);
                Instantiate(bombx[bombwall], x[i].transform.position + new Vector3(j, 0, 0), x[i].rotation, x[i]);
            }
            
        }
        for (int i = 0;i < xz.Length; i++)
        {
            int s = Random.Range(0, 3);
                switch (s) 
            {   case 0:

                    for (int j = 0; j <= 6; j += 2)
                    {
                        Instantiate(bombx[0], xz[i].transform.position + new Vector3(0, 0, j), xz[i].rotation, xz[i]);
                    }
                    break;
                case 1:
                    for (int j = 0; j <= 6; j += 2)
                    {
                        Instantiate(bombx[0], xz[i].transform.position + new Vector3(j, 0, j), xz[i].rotation, xz[i]);
                    }
                    break; 
                case 2:
                    for (int j = 0; j <= 6; j += 2)
                    {
                        Instantiate(bombx[0], xz[i].transform.position + new Vector3(-j, 0, j), xz[i].rotation, xz[i]);
                    }
                    break;
            }
            
           
        }
    }
   
}
