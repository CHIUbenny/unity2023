using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class main : MonoBehaviour
{
    private static main _instance = null;
    public static main Instance() { return _instance; }
    // Start is called before the first frame update
    private GameObject enemyObject = null;

    
    private List<GameObjectData> pigs = new List<GameObjectData>();
    private Texture2D pigTexture;
    //public Object pig;
    //private GameObject[] pigs;
    public godviewcamera camCtrl;
    //public Transform s;
    private void Awake()
    {
        _instance = this;
        ResourceLoader r = new ResourceLoader();
        r.Init();
        //r.LoadObject("mats/Flash");
        // enemyTexture = r.LoadObject("textures/Lightning") as Texture2D;
       /* pigTexture = r.LoadTextureObject("textures/Lightning");
        r.LoadTextureObject("mats/Flash");
        r.LoadAllObject("game1");

        r.LoadTextObject("game1/data");*/

    }
    private void Start()
    {
        StartCoroutine(ResourceLoader.Instance().LoadGameObjectAsync("Pig", FinishAsyncLoadGameObject));
    }
    void FinishAsyncLoadGameObject(Object o)
    {
        enemyObject = o as GameObject;
        gameobjectPool.Instance().InitObjectPool(100, enemyObject);
      
        Debug.Log("FinishAsyncLoadObject " + o.name);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GeneratePig(1);
        }
    }
    public void RemoveEnemy(GameObject go)
    {
        gameobjectPool pool = gameobjectPool.Instance();
        for (int i = 0; i < pigs.Count; i++)
        {
            Debug.Log("RemoveEnemy " + go.name + ":" + pigs[i].go.name);
            GameObjectData gData = pigs[i];
            if (gData.go == go)
            {
                Debug.Log("RemoveEnemyIII  " + i);
                pigs.RemoveAt(i);
                pool.UnLoadObjectToPool(gData);

            }
        }
    }
    private void GeneratePig(int num)
    {

        if (pigs == null)
        {
            pigs = new List<GameObjectData>();
        }
        gameobjectPool pool = gameobjectPool.Instance();
        for (int i = 0; i < num; i++)
        {
            GameObjectData gData = pool.LoadObjectFromPool(false);
            GameObject go = gData.go;
            Vector3 vdir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            if (vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(5.0f, 10.0f);
            go.SetActive(true);
            pigs.Add(gData);
        }
    }
}
