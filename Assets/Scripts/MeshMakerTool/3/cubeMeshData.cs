using System.Collections;
using UnityEngine;

public static class cubeMeshData
{
    public static Vector3[] vertices =
    {
        new Vector3(1,1,1),//top right north        0
        new Vector3(-1,1,1),//top left north        1
        new Vector3(-1,-1,1),//bottom left north    2
        new Vector3(1,-1,1),//bottom right north    3
        new Vector3(-1,1,-1),//top left west        4
        new Vector3(1,1,-1),// top right west       5
        new Vector3(1,-1,-1),//bottom left west     6
        new Vector3(-1,-1,-1)//bottom right west    7
    };

    public static int[][] faceTriangles =
    {
        new int[]
        {
            0,
            1,
            2,
            3
        },
         new int[]
        {
            5,
            0,
            3,
            6
        },
        new int[]
        {
            4,
            5,
            6,
            7 
        },
         new int[]
        {
            1,
            4,
            7,
            2
        },
        new int[]
        {
            5,
            4,
            1,
            0
        },
        new int[]
        {
            3,
            2,
            7,
            6
        },
    };
    public static Vector3[] faceVertices(int dir, float scale, Vector3 offset)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = (vertices[faceTriangles[dir][i]] * scale) + offset;
        }
        return fv;
    }
}
