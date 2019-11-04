using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public void SetActive(bool _value)
    {
        gameObject.SetActive(_value);
    }
}
