using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    [SerializeField]
    MyLevelData data;
    // Start is called before the first frame update
    void Start()
    {
        JsonTest.SaveMyLevel("Assets/abc.txt");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            data = JsonTest.LoadMyLevel("Assets/abc.txt");
        }
    }
}
