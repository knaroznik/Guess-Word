using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area<T> where T : IPosition
{
    public Bounds limits;
    public AreaNode<T>[,] nodes; //ADD

    public Area(Bounds _limits)
    {
        limits = _limits;

        nodes = new AreaNode<T>[10, 7];

        float xOffset = (limits.max.x - limits.min.x) / 10;
        float yOffset = (limits.max.y - limits.min.y) / 6;

        for (int i=0; i<10; i++)
        {
            for(int j=0; j<6; j++)
            {
                nodes[i, j] = new AreaNode<T>(new Vector3(limits.min.x + xOffset * i, limits.min.y + yOffset * j, 0f));
            }
        }
    }

    public Vector3 GetRandomPoint(T obj)
    {
        int x, y;
        x = Random.Range(0, 10);
        y = Random.Range(0, 6);
        AreaNode<T> node = nodes[x, y];
        if (node.IsFree)
        {
            node.AssignObject(obj);
            return node.NodePosition;
        }
        else return GetRandomPoint(obj, x + 1, y + 1);
    }

    private Vector3 GetRandomPoint(T obj, int x, int y)
    {
        if (y > 5)
        {
            x++;
            y = 0;
        }
        if (x > 9) x = 0;
        AreaNode<T> node = nodes[x, y];
        if (node.IsFree)
        {
            node.AssignObject(obj);
            return node.NodePosition;
        }
        else return GetRandomPoint(obj, x + 1, y + 1);
    }
}
