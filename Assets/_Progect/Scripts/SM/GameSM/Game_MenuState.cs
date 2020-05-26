using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_MenuState : StateMachineBehaviour
{
    GameManager GM;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GM == null)
            GM = GameManager.I;

        GM.GetUIManager().SetCurrentMenu(UIManager.MenuType.Menu);
    }

}
