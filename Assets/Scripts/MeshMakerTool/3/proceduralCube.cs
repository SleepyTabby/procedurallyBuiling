using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class proceduralCube : MonoBehaviour
{
    [Header("basic")]
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    [SerializeField] float scale = 1f;
    [SerializeField] int posX;
    [SerializeField] int posY;
    [SerializeField] int posZ;
    float adjustedScale;


    [Header("editor")]
    [SerializeField] GameObject vertex;
    List<GameObject> verticesInScene = new List<GameObject>();
    Transform verTrans;


    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 0.5f; // multiplying is less extentive on the system
    }
    //make a button to update stuff and add auto update modes
    private void Start()
    {
        MakeCube(adjustedScale, new Vector3((float)posX * scale, (float)posY * scale, (float)posZ * scale));
        UpdateMesh();

    }


    void MakeCube(float Scale, Vector3 cubeOffset)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            MakeFace(i, Scale, cubeOffset);
        }
        AddEditableVerts();
    }

    private void FixedUpdate()
    {
        //add editor capability

        for (int i = 0; i < verticesInScene.Count; i++)
        {
            if (vertices[i] != verticesInScene[i].transform.position)
            {
                vertices[i] = verticesInScene[i].transform.position;
                UpdateMesh();
            }
        }
    }
    void AddEditableVerts()
    {
        //instantiate editor verts
        foreach (Vector3 i in vertices)
        {
            verticesInScene.Add(Instantiate(vertex, verTrans));

        }
        for (int i = 0; i < verticesInScene.Count; i++)    // fuse these together and add a way to see the lines which form te geometry and add a possibility to turn it off 
        {
            verticesInScene[i].transform.position = vertices[i];
        }

    }

    void MakeFace(int dir, float CScale, Vector3 facesOffset)
    {
        vertices.AddRange(cubeMeshData.faceVertices(dir, CScale, facesOffset));

        int vCount = vertices.Count;
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
    private void OnDrawGizmos()
    {
        if (transform.position != new Vector3((float)posX * scale, (float)posY * scale, (float)posZ * scale))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, new Vector3((float)posX * scale, (float)posY * scale, (float)posZ * scale));
        }
        
    }
}
