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

    private void Start()
    {
        userWord = new List<LetterBehaviour>();

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

    public bool IsConnected(LetterBehaviour nextLetter)
    {
        if (userWord.Count == 0) return true;

        if(connections.HasConnection(userWord[userWord.Count-1], nextLetter))
        {
            return true;
        }

        return false;
    }

    public void OnGUI()
    {
        string customWord = "";
        for(int i=0; i < userWord.Count; i++)
        {
            customWord += userWord[i].letter + " ";
        }
        GUI.Box(new Rect(0, 0, 300, 100), "Word");
        GUI.Label(new Rect(10, 10, 300, 50), "Word : " + customWord);
    }


    public Color GetNextLetterColor(bool _selecting, LetterBehaviour letter)
    {
        Color output;

        if (userWord.Count == 0)output = Color.green;
        else output = Color.red;

        if (!_selecting)
        {
            userWord.Add(letter);
        }
        else
        {
            int index = userWord.IndexOf(letter);
            int wordLength = userWord.Count-1;

            for(int i=wordLength; i>= index; i--)
            {
                userWord[i].Deselect();
                userWord.Remove(userWord[i]);
            }
            
        }
        return output;
    }
}
