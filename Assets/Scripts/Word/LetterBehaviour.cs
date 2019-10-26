using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class LetterBehaviour : MonoBehaviour, IPosition, IPointerDownHandler
{
    public Vector3 GetCustomPosition()
    {
        return transform.position;
    }

    public TextMeshProUGUI letterText;
    public char letter;
    private WordScene gameScript;
    private Material customMaterial;


    public void Init(char _letterValue, WordScene _gameScript)
    {
        _letterValue = Char.ToUpper(_letterValue);
        letter = _letterValue;
        letterText.text = _letterValue.ToString();
        gameScript = _gameScript;

        customMaterial = letterText.gameObject.GetComponent<TextMeshProUGUI>().fontSharedMaterial;
    }

    private bool _selected = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (_selected) StartCoroutine(StartOutLine(0f));
        else StartCoroutine(StartOutLine(0.7f));

        _selected = !_selected;
    }

    IEnumerator StartOutLine(float expectedValue)
    {
        
        Color c = gameScript.GetNextLetterColor(letter);
        Debug.Log((c == Color.red) + " " + (c == Color.green));
        customMaterial.SetColor("_OutlineColor", c);

        float currentValue = letterText.outlineWidth;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 1f;
            letterText.outlineWidth = Mathf.Lerp(currentValue, expectedValue, t);
            yield return null;
        }
        customMaterial = letterText.gameObject.GetComponent<TextMeshProUGUI>().fontSharedMaterial;
    }
}
