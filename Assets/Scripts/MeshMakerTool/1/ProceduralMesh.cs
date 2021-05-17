using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralMesh : MonoBehaviour
{
    [Header("vertices lesson 1&2")]
    Vector3[] vertices;
    int[] triangles;
    
    [Header("quads")]
    Vector3[] qVertices;
    int[] qTriangles;

    [Header("cube")]
    Vector3[] cVertices;
    int[] cTriangles;

    [Header("common")]
    Mesh mesh;
    [SerializeField]GameObject vertex;
    List<GameObject> verticesInScene = new List<GameObject>();
    Transform verTrans;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        //qMakeMeshData();
        cMakeMeshData();
        CreateMesh();
        
    }


    void qMakeMeshData()
    {
        //create an array of verteces to form a quad
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1)
        };
        //and create an array of integers
        triangles = new int[]
        {
            0,
            1,
            2,
            2,
            1,
            3
        };
    }
    void extrude() { 
    }
    void cMakeMeshData()
    {
        //create an array of verteces to form a quad
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0), //0  
            new Vector3(0, 0, 1), //1
            new Vector3(1, 0, 0), //2
            new Vector3(1, 0, 1), //3
            new Vector3(0, -1, 0), //4
            new Vector3(1, -1, 0), //5
            new Vector3(1, -1, 1), //6
            new Vector3(0, -1, 1) //7

        };
        foreach (Vector3 i in vertices) 
        {
            verticesInScene.Add(Instantiate(vertex, verTrans));
        }

        //and create an array of integers
        triangles = new int[]
        {
            //quad top
            0, // 0 -> 1
            1, // 0 -> 2
            2, // 2 -> 0
            2, // 2 -> 1
            1, // 1 -> 3
            3, // 3 -> 2
            //quad left
            4,
            0,
            5,
            5,
            0,
            2,
            //quad back
            5,
            2,
            6,
            6,
            2,
            3,
            //quad right
            6,
            3,
            7,
            7,
            3,
            1,
            //quad front
            7,
            1,
            4,
            4,
            1,
            0,
            //quad bottom
            7,
            4,
            6,
            6,
            4,
            5
   
        };
        for (int i = 0; i < vertices.Length; i++)
        {
            verticesInScene[i].transform.position = vertices[i];
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if(vertices[i] != verticesInScene[i].transform.position)
            {
                vertices[i] = verticesInScene[i].transform.position;
                Debug.Log("i am supposed to work");
                CreateMesh();
                PaintQuadCentre();
            }
        }
    }
    void PaintQuadCentre()
    {

    }
    void MakeMeshData()//make traingles
    {
        //create an array of verteces 
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0)
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
        mesh.RecalculateNormals();
        mesh.triangles = triangles;
    }
    
    void UpdateMesh()
    {
        //in editor points get made and they connect them selfes to the ther vertex they need to ben connected to 
        //make an use window in the inspector
        //have undo states 
        //it renders the mesh when you put the new vert in there
        //add option to disable or enable back face culling (making a point on the other side of the model)
    }

}
