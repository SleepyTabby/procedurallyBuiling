using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProceduralMeshMakerMaker : MonoBehaviour
{//order the file and make this the tool file and the other where the magic happends 
    Vector3[] vertices;
    int[] triangles;
    Mesh mesh;
    [SerializeField] bool renderMesh;
    [SerializeField] GameObject vertex;
    ProceduralMesh MeshManager;
    [Header("creatorMenu")]
    [SerializeField] bool newVert;
    List<GameObject> verticesInScene = new List<GameObject>();
    void Start()
    {
        MeshManager = GetComponent<ProceduralMesh>();
        mesh = GetComponent<MeshFilter>().mesh;
        MakeMeshData();
    }
    //maybe save certain meshes as prefab in script 
    // Update is called once per frame
    void MakeMeshData()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(vertex, transform.position, transform.rotation);
            verticesInScene.Add(obj);
        }
        //stop dit in een scriptable object and later in a file 

        //create an array of verteces 
        vertices = new Vector3[]
        {
            //it no worky

            verticesInScene[verticesInScene.Count].transform.position,
            verticesInScene[verticesInScene.Count-1].transform.position,
            verticesInScene[verticesInScene.Count-2].transform.position

        };
        //and create an array of integers
        triangles = new int[]
        {
            0,
            1,
            2
        };
    }
    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
    void Update()
    {
        if (renderMesh)
        {
            CreateMesh();
            renderMesh = false;
        }
        if (newVert)
        {
            MakeMeshData();
            newVert = false;
        }
    }
}
