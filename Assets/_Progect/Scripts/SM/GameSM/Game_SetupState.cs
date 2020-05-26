using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Game_SetupState : StateMachineBehaviour
{
    GameManager GM;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GM == null)
            GM = GameManager.I;


        /// Get references to components
        GM.SetPoolManager(GM.GetComponentInChildren<PoolManager>());
        GM.SetGridController(GM.GetComponentInChildren<GridController>());
        GM.SetIngredientsController(GM.GetComponent<IngredientsController>());
        GM.SetGameController(GM.GetComponent<GameController>());
        DOTween.Init();

        /// Setup of the components
        GM.GetPoolManager().Setup();
        GM.GetGridController().Setup();
        GM.GetIngredientsController().Setup();

        /// Move to next state of the State machine
        GM.GoToNext();
    }

}
