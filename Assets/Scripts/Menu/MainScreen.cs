using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainScreen : BaseScreen
{

    public TextMeshProUGUI[] buttonTexts;

    public UnityEvent onEnableEvent;

    private void Awake()
    {
        float fontSize = buttonTexts[0].fontSize;
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].enableAutoSizing = false;
            buttonTexts[i].fontSize = fontSize;
        }
    }

    public void OnEnable()
    {
        onEnableEvent.Invoke();
    }
}
