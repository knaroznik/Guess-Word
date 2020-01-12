using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public static class WordUtil
{
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
        return modifiedString;
    }

    private static string GetWordDescription(string word)
    {
        string output = "";
        WebClient client = new WebClient();
        string downloadedString = client.DownloadString("https://www.thefreedictionary.com/" + word);
        string[] rows = downloadedString.Split(new string[] { "<div class" }, StringSplitOptions.None);
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].Contains("ds-list"))
            {
                rows[i] = rows[i].Replace("=\"ds-list\">", "");
                rows[i] = rows[i].Replace("=\"syn ds-list\">", "");
                rows[i] = rows[i].Split(new string[] { "</div>" }, StringSplitOptions.None)[0];
                output += rows[i] + "\n";
            }
        }
        return output;
    }
}
