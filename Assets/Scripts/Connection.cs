using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    private LineRenderer connectionRenderer;

    public Connection()
    {
        connectionRenderer = null;
    }

    public Connection(LetterBehaviour A, LetterBehaviour B, LineRenderer _renderer)
    {
        connectionRenderer = _renderer;
        connectionRenderer.SetPosition(0, A.GetCustomPosition());
        connectionRenderer.SetPosition(1, B.GetCustomPosition());
    }
}
