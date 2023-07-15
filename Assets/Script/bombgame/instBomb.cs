using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class instBomb : MonoBehaviour
{
    private static instBomb gameManager = null;
    public static instBomb Instance() { return gameManager; }


    // Start is called before the first frame update
    public GameObject[] bomb;
    public Transform point5;
    public static int count;
    public float instime = 0.5f;
    public float x1, x2 ,z1,z2;
    public float y;
    public int countup;
    public GameObject[] bombx;
    public Transform[] x;
    public Transform[] xz;

    private void Awake()
    {
       //if (gameManager == null)
       // {
            gameManager = this;
          //DontDestroyOnLoad(gameObject);
       //}
       // else { Destroy(gameObject); }

    }
    void Start()
    {
        
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
        int RandomBomb = Random.Range(0, bomb.Length);

        if (count<countup) {
            Instantiate(bomb[RandomBomb], new Vector3(Random.Range(x1, x2), y, Random.Range(z1, z2)), Quaternion.identity, point5);
            count++;
        }
      
    }
    public void InsBombx()
    {
        
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j <= 12; j+=2)
            {
                int bombwall = Random.Range(0,2);
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
                        Instantiate(bombx[2], xz[i].transform.position + new Vector3(0, 0, j), xz[i].rotation, xz[i]);
                    }
                    break;
                case 1:
                    for (int j = 0; j <= 6; j += 2)
                    {
                        Instantiate(bombx[2], xz[i].transform.position + new Vector3(j, 0, j), xz[i].rotation, xz[i]);
                    }
                    break; 
                case 2:
                    for (int j = 0; j <= 6; j += 2)
                    {
                        Instantiate(bombx[2], xz[i].transform.position + new Vector3(-j, 0, j), xz[i].rotation, xz[i]);
                    }
                    break;
            }
            
           
        }
    }
    /*public  void Replay()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("testbomb");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("testbomb2");

    }
    public void OnQuit()
    {
        Application.Quit();
    }*/
}
