using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonAnimatorBehaviour : MonoBehaviour
{
    public float AnimationDelay;
    
    public UnityEvent endAnimationAction;

    private Vector3 originalPosition;


    private IEnumerator Start()
    {
        originalPosition = transform.position;
        Vector3 offScreenPosition = new Vector3(Screen.width + this.GetComponent<RectTransform>().rect.width/2, transform.position.y, transform.position.z);
        transform.position = offScreenPosition;

        yield return new WaitForSeconds(AnimationDelay);

        StartCoroutine(LerpPosition(offScreenPosition, originalPosition));
    }

    private IEnumerator LerpPosition(Vector3 start, Vector3 end)
    {
        var currentA = start;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 0.25f;
            transform.position = Vector3.Lerp(currentA, end, t);
            yield return null;
        }
    }


    public void PlayUnityAction()
    {
        endAnimationAction.Invoke();
    }
}
