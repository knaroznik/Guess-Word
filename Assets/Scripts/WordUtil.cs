using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public static class WordUtil
{
    private static List<string> definitionHolders = new List<string>() { "ds-list" , "ds-single"};
    private static string notFoundKeyWord = "notFound";

    private static string HolderDifinition(string rowRaw)
    {
        for(int i=0; i<definitionHolders.Count; i++)
        {
            if (rowRaw.Contains(definitionHolders[i])) return definitionHolders[i];
        }
        return notFoundKeyWord;
    }

    public static string[] GetRandomWord()
    {
        string[] data = new string[2];

        WebClient client = new WebClient();
        string downloadedString = client.DownloadString("https://wordunscrambler.me/random-word-generator");
        string[] rows = downloadedString.Split('\n');
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].Contains("/dictionary/"))
            {
                data[0] = GetStringRaw(rows[i]);
                data[1] = GetWordDescription(data[0]);
                if(data[1] == "")
                {
                    return GetRandomWord();
                }
                Debug.Log(data[0]);
                break;
            }
        }
        return data;
    }

    private static string GetStringRaw(string htmlRowResponse)
    {
        string modifiedString = htmlRowResponse;
        modifiedString = modifiedString.Replace(" ", "");
        modifiedString = modifiedString.Replace("<p><ahref=\"/dictionary/", "");
        modifiedString = modifiedString.Substring(0, modifiedString.Length - 1);
        return modifiedString;
    }

    private static string GetWordDescription(string word)
    {
        string output = "";
        WebClient client = new WebClient();
        string downloadedString = client.DownloadString("https://www.thefreedictionary.com/" + word);
        string[] rows = downloadedString.Split(new string[] { "<section data-src=" }, StringSplitOptions.None);
        for (int i = 0; i < rows.Length; i++)
        {
            string holder = HolderDifinition(rows[i]);
            if ( holder != notFoundKeyWord)
            {
                string[] row = rows[i].Split(new string[] { holder }, StringSplitOptions.None);
                for(int j=1; j<row.Length; j++)
                {
                    Debug.Log("OUTPUT Raw " + row[j]);
                    int endPosition = row[j].IndexOf("</div>");
                    row[j] = row[j].Substring(3, endPosition - 3);
                    Debug.Log("OUTPUT " + row[j]);
                    output += row[j] + "\n";
                }
            }
        }
        return output;
    }
}
