using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private static Manager manager = null;
    public static Manager Instance (){  return manager;  }

    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }   
    }
    void FinishLoadScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("FinishLoadScene " + scene.name);
    }
    void Start()
    {
        SceneTest key = SceneTest.Instance();
        if (key == null) 
        {
            key = new SceneTest();
            key.TestKey();
        }
        key.SetupLoadingCallback(FinishLoadScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            if (SceneManager.GetActiveScene().name == "xxx")
            {
                SceneTest.Instance().ChangeScene("xxxx");
                // SceneManager.LoadScene("fps", LoadSceneMode.Single);
            }
            else
            {
                SceneTest.Instance().ChangeScene("xxx");
                //SceneManager.LoadScene("menu", LoadSceneMode.Single);
            }
        }
    }
}
