using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public static class WordUtil
{
    public static string[] GetRandomWord()
    {
        int rowFound = 0;
        string[] data = new string[2];

        WebClient client = new WebClient();
        string downloadedString = client.DownloadString("https://randomword.com/");
        string[] rows = downloadedString.Split('\n');
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].Contains("random_word"))
            {
                data[rowFound] = GetStringRaw(rows[i]);
                rowFound++;
                if (rowFound > 1)
                {
                    break;
                }
            }
        }
        return data;
    }

    private static string GetStringRaw(string htmlRowResponse)
    {
        int startingPos = htmlRowResponse.IndexOf('>');
        string modifiedString = htmlRowResponse.Substring(startingPos + 1);
        int endPos = modifiedString.IndexOf('<');
        modifiedString = modifiedString.Substring(0, endPos);
        return modifiedString;
    }
}
