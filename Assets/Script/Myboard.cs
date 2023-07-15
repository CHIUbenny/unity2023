using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myboard : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();

        Mesh m = new Mesh();

        Vector3[] vertices = new Vector3[4];

        vertices[0] = new Vector3(-0.5f, -0.5f, 0.0f);
        vertices[1] = new Vector3(0.5f, -0.5f, 0.0f);
        vertices[2] = new Vector3(0.5f, 0.5f, 0.0f);
        vertices[3] = new Vector3(-0.5f, 0.5f, 0.0f);

        m.vertices = vertices;
        int numberTri = 2;
        int numIndices = 3 * numberTri;
        int[] tris = new int[numIndices];
        tris[0] = 0;
        tris[1] = 1;
        tris[2] = 2;
        tris[3] = 0;
        tris[4] = 2;
        tris[5] = 3;
        m.triangles = tris;
        m.RecalculateNormals();

        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 1);
        m.uv = uvs;
        mf.mesh = m;

        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mr.material = mat;
        //  mr.material = new Material(Shader.Find("Diffuse"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
