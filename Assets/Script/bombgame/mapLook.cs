using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLook : MonoBehaviour
{
    public static mapLook Instance;
    public Transform player;
    public float hight = 30f;

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);


        }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position+Vector3.up*hight;
    }
}
