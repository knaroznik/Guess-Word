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
        for(int i=0; i<splashes.Length; i++)
        {
            splashes[i].color = Color.clear;
        }

        for(int i=0; i<splashes.Length; i++)
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

        SceneManager.LoadScene(1);
        
    }
}
