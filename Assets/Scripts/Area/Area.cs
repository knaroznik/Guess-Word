using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area<T> where T : IPosition
{
    public Bounds limits;
    private float regularObjectSize;
    private List<T> areaObjects;

    public Area(float _regularObjectSize, Bounds _limits)
    {
        regularObjectSize = _regularObjectSize;
        limits = _limits;
        areaObjects = new List<T>();
    }

    public Vector3 GetRandomPoint()
    {
        return new Vector3(
        Random.Range(limits.min.x, limits.max.x),
        Random.Range(limits.min.y, limits.max.y),
        Random.Range(limits.min.z, limits.max.z)
    );
    }

    public bool Add(T _newValue)
    {
        Vector3 customPosition = _newValue.GetCustomPosition();
        if (limits.Contains(customPosition))
        {
            for(int i=0; i<areaObjects.Count; i++)
            {
                if(Vector3.Distance(areaObjects[i].GetCustomPosition(), customPosition) < regularObjectSize)
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        areaObjects.Add(_newValue);
        return true;
    }

    public void Remove(T _newValue)
    {
        areaObjects.Remove(_newValue);
    }
}
