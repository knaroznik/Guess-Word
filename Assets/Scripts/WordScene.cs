using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// TODO : No internet Connection
/// TODO : Moving letters?
/// TODO : Graph from letters
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

    private void Start()
    {
        GenerateArea();
        GenerateWord();
        LetterBehaviour[] word = new LetterBehaviour[randomWord.GetWord().Length];
        for(int i=0; i<randomWord.GetWord().Length; i++)
        {
            GameObject letter = Instantiate(LetterPrefab, LetterParent);
            letter.GetComponent<LetterBehaviour>().Init(randomWord.GetWord()[i], this);
            AddLetterToArea(letter.GetComponent<LetterBehaviour>());
            word[i] = letter.GetComponent<LetterBehaviour>();
        }

        GenerateConnections(word);
    }

    private void AddLetterToArea(LetterBehaviour letter)
    {
        Vector3 pos = cameraArea.GetRandomPoint();
        letter.gameObject.transform.position = pos;

        if (!cameraArea.Add(letter))
        {
            AddLetterToArea(letter);
        }
    }

    public void GenerateArea()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Bounds b = new Bounds(Camera.main.transform.position + new Vector3(0,-descriptionSize/2,10), new Vector3(pos.x, pos.y, 0f) * 2f - new Vector3(letterSize*2, letterSize*2 + descriptionSize/2, 0f));
        cameraArea = new Area<LetterBehaviour>(letterSize, b);
    }

    public void GenerateWord()
    {
        randomWord = new Word();
        Debug.Log(randomWord.GetWord());
        //wordText.text = randomWord.GetWord();
        descriptionText.text = randomWord.GetDescription();
    }

    private void GenerateConnections(LetterBehaviour[] word)
    {
        connections = new Connections(word, connectionPrefab);
    }

    private char currentLetter = '\x0000';
    public Color GetNextLetterColor(char _nextLetter)
    {
        Color output;

        if (currentLetter == '\x0000') output = Color.green;
        else output = Color.red;

        currentLetter = _nextLetter;

        return output;
    }
}
