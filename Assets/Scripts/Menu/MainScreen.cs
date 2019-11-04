using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainScreen : BaseScreen
{

    public TextMeshProUGUI[] buttonTexts;

    private void Awake()
    {
        float fontSize = buttonTexts[0].fontSize;
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].enableAutoSizing = false;
            buttonTexts[i].fontSize = fontSize;
        }
    }
}
