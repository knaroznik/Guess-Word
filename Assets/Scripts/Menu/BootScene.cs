using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootScene : MonoBehaviour
{
    public Image[] splashes;

    IEnumerator Start()
    {
        HideSplashes();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;

        yield return StartCoroutine(ShowSplashes());

        asyncOperation.allowSceneActivation = true;

        while (!asyncOperation.isDone)
        {
            yield return 0;
        }
        
    }

    private void HideSplashes()
    {
        for (int i = 0; i < splashes.Length; i++)
        {
            splashes[i].color = Color.clear;
        }
    }

    private  IEnumerator ShowSplashes()
    {
        for (int i = 0; i < splashes.Length; i++)
        {
            var currentA = Color.clear;
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / 1f;
                splashes[i].color = Color.Lerp(currentA, Color.white, t);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            currentA = Color.white;
            t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / 1f;
                splashes[i].color = Color.Lerp(currentA, Color.clear, t);
                yield return null;
            }
        }
    }
}
