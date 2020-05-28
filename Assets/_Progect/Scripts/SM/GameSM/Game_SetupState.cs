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
        GM.SetInputController(GM.GetComponent<InputController>());
        GM.SetEventsContainer(new EventsContainer());
        GM.SetPoolManager(GM.GetComponentInChildren<PoolManager>());
        GM.SetGridController(GM.GetComponentInChildren<GridController>());
        GM.SetIngredientsController(GM.GetComponentInChildren<IngredientsController>());
        GM.SetNumbersController(GM.GetComponentInChildren<NumbersController>());
        GM.SetGameController(GM.GetComponent<GameController>());
        GM.SetUIManager(FindObjectOfType<UIManager>());
        DOTween.Init();

        /// Setup of the components
        GM.GetInputController().Setup();
        GM.GetPoolManager().Setup();
        GM.GetGridController().Setup();

        if (GM.GetGameController().CurrentGameType == GameController.GameType.Ingredients)
            GM.GetIngredientsController().Setup();
        else
            GM.GetNumbersController().Setup();
        
        GM.GetUIManager().Setup();

        /// Move to next state of the State machine
        GM.GoToNext();
    }

}
