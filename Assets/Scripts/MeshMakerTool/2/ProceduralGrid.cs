using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    [Header("basic")]
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    [Header("grid Settings")]
    [SerializeField] float cellSize = 1;
    [SerializeField] Vector3 gridOffset;
    [SerializeField] int gridSize = 1; //maybe change this to vector2
    [Header("editor")]
    [SerializeField] GameObject vertex;
    List<GameObject> verticesInScene = new List<GameObject>();
    Transform verTrans;



    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        MakeContiguousProceduralGrid();
        UpdateMesh();
        
    }

    void MakeDiscreteProceduralGrid() //make unlinked quads
    {
        //set array size       x axis length   z axis length   times   verteces
        vertices = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];


        //set tracker ints
        int v = 0;
        int t = 0;


        //set vertex offset
        float vertexOffset = cellSize * 0.5f; // to keep the origin centered //try to devide as little as you can as it is expensive on the computer

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 cellOffset = new Vector3(x + cellSize, 0, y * cellSize);


                //populate the tris and verts arrays
                /*
                                               x axis           y axis          z axis
                 vertices[0] = new Vector3(-vertexOffset,         0,        -vertexOffset);
                 */
                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffset + cellOffset;
                /*   0.5 z axis
                [ ][ ][ ][ ] 
                [ ][ ][ ][ ] 
            -0.5[ ][ ][ ][ ]0.5  x axis
                [ ][ ][ ][ ] 
                [ ][ ][ ][ ] 
                [#][ ][ ][ ] 
                    -0.5
                 */
                vertices[v + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + gridOffset + cellOffset;
                /*   0.5 z axis
               [#][ ][ ][ ] 
               [ ][ ][ ][ ] 
           -0.5[ ][ ][ ][ ]0.5  x axis
               [ ][ ][ ][ ] 
               [ ][ ][ ][ ] 
               [ ][ ][ ][ ] 
                   -0.5
                */
                vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + gridOffset + cellOffset;
                /*   0.5 z axis
               [ ][ ][ ][ ] 
               [ ][ ][ ][ ] 
           -0.5[ ][ ][ ][ ]0.5  x axis
               [ ][ ][ ][ ] 
               [ ][ ][ ][ ] 
               [ ][ ][ ][#] 
                   -0.5
                */
                vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + gridOffset + cellOffset;
                /*   0.5 z axis
               [ ][ ][ ][#] 
               [ ][ ][ ][ ] 
           -0.5[ ][ ][ ][ ]0.5  x axis
               [ ][ ][ ][ ] 
               [ ][ ][ ][ ] 
               [ ][ ][ ][ ] 
                   -0.5
                */



                //set triangles 
                triangles[t] = v;
                triangles[t + 1] = v + 1;
                triangles[t + 2] = v + 2;
                triangles[t + 3] = v + 2;
                triangles[t + 4] = v + 1;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }
    }

    void MakeContiguousProceduralGrid() //make linked quads
    {
        //set array size       x axis length   z axis length   times   verteces
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)]; //maybe work with vector2 ?
        triangles = new int[gridSize * gridSize * 6];


        //set tracker ints
        int v = 0;
        int t = 0;


        //set vertex offset
        float vertexOffset = cellSize * 0.5f; // to keep the origin centered //try to devide as little as you can as it is expensive on the computer

        //create vertex grid
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, 0, (y * cellSize) - vertexOffset);
                v++;
            }
        }
        v = 0; // reset tracker

        //making the tris
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                //set triangles 
                //triangles[t] = v;
                //triangles[t + 1] = v + 1;
                //triangles[t + 2] = v + 2;
                //triangles[t + 3] = v + (gridSize + 1);
                //triangles[t + 4] = v + 1;
                //triangles[t + 5] = v + (gridSize + 2);
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1; 
                triangles[t + 2] = triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize + 2);
                v++;
                t += 6;
            }
            v++;
        }
        AddEditableVerts();
    }

    void AddEditableVerts()
    {
        //instantiate editor verts
        foreach (Vector3 i in vertices)
        {
            verticesInScene.Add(Instantiate(vertex, verTrans));

        }
        for (int i = 0; i < verticesInScene.Count; i++)
        {
            verticesInScene[i].transform.position = vertices[i];
        }
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

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
    }
}
