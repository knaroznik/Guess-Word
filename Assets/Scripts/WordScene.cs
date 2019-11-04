using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// TODO : No internet Connection
/// TODO : Moving letters?
/// TODO : Filling word
/// TODO : Menu
/// </summary>
public class WordScene : MonoBehaviour
{
    public Text wordText;
    public Text descriptionText;

    public GameObject debugCube;
    public GameObject LetterPrefab;
    public GameObject connectionPrefab;
    public Transform LetterParent;
    public float letterSize = 2f;
    public float descriptionSize = 1f;

    Area<LetterBehaviour> cameraArea;

    private Word randomWord;
    private Connections connections;
    
    private List<LetterBehaviour> userWord;
}
