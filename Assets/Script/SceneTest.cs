using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneTest 
{
    private static SceneTest manager = null;
    public static SceneTest Instance() { return manager; }
    // Start is called before the first frame update
    public void TestKey() 
    {
        manager = this;
    }
    public void SetupLoadingCallback(UnityAction<Scene, LoadSceneMode> finishLoaded)
    {
        SceneManager.sceneLoaded += finishLoaded;
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
