using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScreen : BaseScreen
{
    public Text descriptionText;

    public GameObject debugCube;
    public GameObject LetterPrefab;
    public GameObject connectionPrefab;
    public Transform LetterParent;
    public float descriptionSize = 1f;

    Area<LetterBehaviour> cameraArea;
    LetterBehaviour[] word;

    private Word randomWord;
    private Connections connections;

    private List<LetterBehaviour> userWord;

    public GameObject endPanel;

    public void OnEnable()
    {
        endPanel.SetActive(false);
        userWord = new List<LetterBehaviour>();

        GenerateArea();
        GenerateWord();
        word = new LetterBehaviour[randomWord.GetWord().Length];
        for (int i = 0; i < randomWord.GetWord().Length; i++)
        {
            GameObject letter = Instantiate(LetterPrefab, LetterParent);
            letter.GetComponent<LetterBehaviour>().Init(randomWord.GetWord()[i], this);
            AddLetterToArea(letter.GetComponent<LetterBehaviour>());
            word[i] = letter.GetComponent<LetterBehaviour>();
        }

        GenerateConnections(word);
    }

    private void OnDisable()
    {
        connections.Disable();
        for(int i=0; i<word.Length; i++)
        {
            if(word[i] != null) Destroy(word[i].gameObject);
        }
        word = new LetterBehaviour[0];
    }

    private void AddLetterToArea(LetterBehaviour letter)
    {
        Vector3 pos = cameraArea.GetRandomPoint(letter);
        letter.gameObject.transform.position = pos;

    }

    public void GenerateArea()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Bounds b = new Bounds(Camera.main.transform.position + new Vector3(0, -descriptionSize / 2, 10), new Vector3(pos.x, pos.y, 0f) * 2f - new Vector3(4, 4 + descriptionSize / 2, 0f));
        cameraArea = new Area<LetterBehaviour>(b);
    }

    public void GenerateWord()
    {
        randomWord = new Word();
        descriptionText.text = randomWord.GetDescription();
    }

    private void GenerateConnections(LetterBehaviour[] word)
    {
        connections = new Connections(word, connectionPrefab);
    }

    public bool IsConnected(LetterBehaviour nextLetter)
    {
        if (userWord.Count == 0) return true;

        if (connections.HasConnection(userWord[userWord.Count - 1], nextLetter))
        {
            return true;
        }

        return false;
    }

    public Color GetNextLetterColor(bool _selecting, LetterBehaviour letter)
    {
        Color output;

        if (userWord.Count == 0) output = Color.green;
        else output = Color.red;

        if (!_selecting)
        {
            userWord.Add(letter);

            TryEndCondition();
        }
        else
        {
            int index = userWord.IndexOf(letter);
            int wordLength = userWord.Count - 1;

            for (int i = wordLength; i >= index; i--)
            {
                userWord[i].Deselect();
                userWord.Remove(userWord[i]);
            }

        }
        return output;
    }

    public void TryEndCondition()
    {
        string currentWord = "";
        for(int i=0; i<userWord.Count; i++)
        {
            currentWord += userWord[i].letter;
        }
        string originalWord = randomWord.GetWord();

        currentWord = currentWord.ToLower();
        originalWord = originalWord.ToLower();

        if(currentWord == originalWord)
        {
            endPanel.SetActive(true);
            OnDisable();
            endPanel.transform.GetChild(0).GetComponent<Text>().text = "YES!\n The word is :\n " + currentWord;
        }
    }
}
