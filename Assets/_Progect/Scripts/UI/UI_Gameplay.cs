using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gameplay : UI_MenuBase
{
    [SerializeField] Animator GameWonAnim;
    bool textActivationStatus;

    public override void OnSetup()
    {
        FadeText(false, true);
    }

    public void FadeText(bool _status, bool _forceAnimation = false)
    {
        GameWonAnim.ResetTrigger("FadeIn");
        GameWonAnim.ResetTrigger("FadeOut");

        if (textActivationStatus != _status || _forceAnimation)
        {
            GameWonAnim.SetTrigger(_status ? "FadeIn" : "FadeOut");
            textActivationStatus = _status;
        }
    }

}
