using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AnimatePositionObject : MonoBehaviour
{
    public Bool3 Axis;

    public float AnimationDelay;

    public UnityEvent endAnimationAction;

    private Vector3 originalPosition;
    private Vector3 offScreenPosition;
    private bool initialized = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        originalPosition = transform.position;

        //what if element is on the left/down?
        float X = Axis.X ? Screen.width + this.GetComponent<RectTransform>().rect.width : transform.position.x;
        float Y = Axis.Y ? Screen.height + this.GetComponent<RectTransform>().rect.height : transform.position.y;
        offScreenPosition = new Vector3(X, Y, transform.position.z);
        transform.position = offScreenPosition;
        initialized = true;
    }


    private IEnumerator Action(Vector3 start, Vector3 end, bool wait, UnityAction action)
    {
        
        if(wait) yield return new WaitForSeconds(AnimationDelay);
        yield return StartCoroutine(LerpPosition(start, end));
        if(action != null) action.Invoke();

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

    public void Show()
    {
        if (!initialized) Init();
        
        StartCoroutine(Action(offScreenPosition, originalPosition, true, null));
    }

    public void HideNoReaction()
    {
        StartCoroutine(Action(originalPosition, offScreenPosition, false, null));
    }

    public void Hide()
    {
        StartCoroutine(Action(originalPosition, offScreenPosition, false, PlayUnityAction));
    }


    public void PlayUnityAction()
    {
        endAnimationAction.Invoke();
    }
}
