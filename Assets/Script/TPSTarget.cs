using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSTarget : MonoBehaviour
{
    public Transform targetTo;
    public float height;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTarget()
    {
        transform.position = targetTo.position + Vector3.up * height;
    }
}
