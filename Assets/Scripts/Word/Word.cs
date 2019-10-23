using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    string[] wordData;

    public string GetWord()
    {
        return wordData[0];
    }

    public string GetDescription()
    {
        return wordData[1];
    }

    public Word()
    {
        wordData = WordUtil.GetRandomWord();
    }
}
