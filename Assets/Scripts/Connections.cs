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
            GameObject connection = MonoBehaviour.Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            Connection newConnection = new Connection(word[i], word[i - 1], connection.GetComponent<LineRenderer>());
            letterGraph[i, i - 1] = newConnection;
            letterGraph[i - 1, i] = newConnection;
        }
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
}
