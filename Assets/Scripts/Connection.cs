using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public LineRenderer connectionRenderer;
    public LetterBehaviour A;
    public LetterBehaviour B;
    public bool Initialized = false;

    public Connection()
    {
        connectionRenderer = null;
        A = null;
        B = null;
        Initialized = false;
    }

    public Connection(LetterBehaviour _A, LetterBehaviour _B, LineRenderer _renderer)
    {
        connectionRenderer = _renderer;
        connectionRenderer.SetPosition(0, _A.GetCustomPosition());
        connectionRenderer.SetPosition(1, _B.GetCustomPosition());

        A = _A;
        B = _B;
        Initialized = true;
    }
}
