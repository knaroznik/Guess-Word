using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNode<T> where T : IPosition
{
    private Vector3 nodePosition;
    public Vector3 NodePosition {
        get
        {
            return nodePosition;
        }
    }


    private T nodeObject;

    private bool isFree;
    public bool IsFree {
        get
        {
            return isFree;
        }
    }
    public AreaNode(Vector3 _nodePosition)
    {
        nodePosition = _nodePosition;
        isFree = true;
    }

    public void AssignObject(T _obj)
    {
        nodeObject = _obj;
        isFree = false;
    }

    public void ClearObject() {
        nodeObject = default(T);
        isFree = true;
    }
}
