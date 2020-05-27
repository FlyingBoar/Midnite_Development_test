using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_MenuBase : MonoBehaviour
{
    Animator anim;
    string fadeIn = "FadeIn";
    string fadeOut = "FadeOut";

    public void Setup()
    {
        anim = GetComponent<Animator>();
        OnSetup();
    }

    public virtual void OnSetup() { }

    public void SetStatus(bool _status)
    {
        anim.ResetTrigger(fadeIn);
        anim.ResetTrigger(fadeOut);

        anim.SetTrigger(_status ? fadeIn : fadeOut);
    }
}
