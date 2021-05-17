using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voxelData
{
    int[,] data = new int[,]
    {
        { 1, 1, 1 },
        { 1, 0, 1 },
        { 1, 1, 1 },
    };

    public int width
    {
        get
        {
            return data.GetLength(0);
        }
    }

    public int depth
    {
        get
        {
            return data.GetLength(1);
        }
    }

    public int GetCell(int x, int z)
    {
        return data[x, z];
    }
}
