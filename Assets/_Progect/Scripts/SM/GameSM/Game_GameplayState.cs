using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_GameplayState : StateMachineBehaviour
{
    GameManager GM;
 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GM == null)
            GM = GameManager.I;

        GM.GetUIManager().SetCurrentMenu(UIManager.MenuType.Gameplay);
    }
}
