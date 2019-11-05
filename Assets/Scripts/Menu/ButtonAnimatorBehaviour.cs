using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonAnimatorBehaviour : MonoBehaviour
{
    public float AnimationDelay;
    public bool PlayAction = false;
    public UnityEvent endAnimationAction;

    Animator anim;

    private IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("AnimationSpeed", 0f);
        yield return new WaitForSeconds(AnimationDelay);
        anim.SetFloat("AnimationSpeed", 1f);
    }

    

    public void Play()
    {
        bool x = anim.GetBool("AnotherAnimation");
        anim.SetBool("AnotherAnimation", !x);
        PlayAction = true;
    }

    public void PlayAnimationOnly()
    {
        bool x = anim.GetBool("AnotherAnimation");
        anim.SetBool("AnotherAnimation", !x);
    }

    public void PlayUnityAction()
    {
        if (PlayAction)
        {
            PlayAction = false;
            endAnimationAction.Invoke();
        }
    }
}
