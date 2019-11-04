using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connections
{
    private Connection[,] letterGraph;
    private GameObject linePrefab;

    public Connections(LetterBehaviour[] word, GameObject _linePrefab)
    {
        linePrefab = _linePrefab;

        int length = word.Length;
        letterGraph = new Connection[length, length];
        for(int i=0; i < letterGraph.GetLength(0); i++)
        {
            for (int j = 0; j < letterGraph.GetLength(1); j++)
            {
                letterGraph[i, j] = new Connection();
            }
        }

        for(int i=1; i<length; i++)
        {
            AddConnection(word, i, i - 1);
        }

        //Random Connections
        int randomConnectionCount = length / 3;
        for (int i = 0; i < randomConnectionCount; i++)
        {
            int A = Random.Range(0, length);
            int B = Random.Range(0, length);
            if (A == B)
            {
                A += 2;
                A = A % length;
            }
            if (!letterGraph[A, B].Initialized)
            {
                AddConnection(word, A, B);
            }
        }
    }

    public void AddConnection(LetterBehaviour[] word, int i, int j)
    {
        GameObject connection = MonoBehaviour.Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        Connection newConnection = new Connection(word[i], word[j], connection.GetComponent<LineRenderer>());
        letterGraph[i, j] = newConnection;
        letterGraph[j, i] = newConnection;
    }

    //Force algorithm : fix
    public bool HasConnection(LetterBehaviour A, LetterBehaviour B)
    {
        for (int i = 0; i < letterGraph.GetLength(0); i++)
        {
            for (int j = 0; j < letterGraph.GetLength(1); j++)
            {
                if (letterGraph[i, j].A == null) continue;

                if (letterGraph[i, j].A == A && letterGraph[i, j].B == B)
                {
                    return true;
                }

                if (letterGraph[i, j].A == B && letterGraph[i, j].B == A)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Disable()
    {
        for (int i = 0; i < letterGraph.GetLength(0); i++)
        {
            for (int j = 0; j < letterGraph.GetLength(1); j++)
            {
                if (letterGraph[i, j].connectionRenderer != null) MonoBehaviour.Destroy(letterGraph[i, j].connectionRenderer.gameObject);
            }
        }
        letterGraph = new Connection[0, 0];
    }
}
