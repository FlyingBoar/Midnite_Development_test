using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    /// <summary>
    /// Reference to events container
    /// </summary>
    private EventsContainer eventsContainer;
    /// <summary>
    /// Reference to InputController
    /// </summary>
    private InputController inputCtrl;
    /// <summary>
    /// Reference to grid controller
    /// </summary>
    private GridController gridCtrl;
    
    /// <summary>
    /// Reference to the Ingredient controller
    /// </summary>
    private IngredientsController ingredientsCtrl;
    /// <summary>
    /// Reference to the Number controller
    /// </summary>
    private NumbersController numbersCtrl;

    /// <summary>
    /// Reference to Pool manager
    /// </summary>
    private PoolManager poolMng;
    /// <summary>
    /// Reference to Pool manager
    /// </summary>
    private GameController gameCtrl;
    /// <summary>
    /// Reference to UI manager
    /// </summary>
    private UIManager UIMng;

    /// <summary>
    /// Reference to the game state machine
    /// </summary>
    private Animator SM;

    // Start is called before the first frame update
    void Start()
    {
        if (I == null)
            InternalSetup();
        else
            DestroyImmediate(gameObject);
    }

    /// <summary>
    /// Inizializza in singleton, prende riferimento della state machine e la fa partire
    /// </summary>
    void InternalSetup()
    {
        I = this;
        SM = GetComponent<Animator>();
        StartSM();
    }

    #region Get

    /// <summary>
    /// Return the refenrece for the Events container
    /// </summary>
    /// <returns></returns>
    public EventsContainer GetEventsContainer()
    {
        return I.eventsContainer;
    }

    /// <summary>
    /// Return the refenrece for the input controller
    /// </summary>
    /// <returns></returns>
    public InputController GetInputController()
    {
        return I.inputCtrl;
    }

    /// <summary>
    /// Return the refenrece for the grid controller
    /// </summary>
    /// <returns></returns>
    public GridController GetGridController()
    {
        return I.gridCtrl;
    }

    /// <summary>
    /// Get Reference for the Ingredients controller
    /// </summary>
    /// <returns></returns>
    public IngredientsController GetIngredientsController()
    {
        return I.ingredientsCtrl;
    }

    /// <summary>
    /// Get reference for the numbers controller
    /// </summary>
    /// <returns></returns>
    public NumbersController GetNumbersController()
    {
        return I.numbersCtrl;
    }

    /// <summary>
    /// Get reference for the pool manager
    /// </summary>
    /// <returns></returns>
    public PoolManager GetPoolManager()
    {
        return I.poolMng;
    }

    /// <summary>
    /// Get reference for the game controller
    /// </summary>
    /// <returns></returns>
    public GameController GetGameController()
    {
        return I.gameCtrl;
    }
    
    /// <summary>
    /// Get reference for the UI manager
    /// </summary>
    /// <returns></returns>
    public UIManager GetUIManager()
    {
        return I.UIMng;
    }

    /// <summary>
    /// Ritorna se la state machine è in gameplay
    /// </summary>
    /// <returns></returns>
    public bool IsGameplay()
    {
        return SM.GetCurrentAnimatorStateInfo(0).IsName("Gameplay") ? true : false;
    }
    #endregion

    #region Set
    /// <summary>
    /// Set the reference for the Input container
    /// </summary>
    /// <param name="_eventsContainer"></param>
    public void SetEventsContainer(EventsContainer _eventsContainer)
    {
        I.eventsContainer = _eventsContainer;
    }

    /// <summary>
    /// Set the reference for the input controller
    /// </summary>
    /// <param name="_inputController"></param>
    public void SetInputController(InputController _inputController)
    {
        I.inputCtrl = _inputController;
    }

    /// <summary>
    /// Set the reference for the grid controller
    /// </summary>
    /// <param name="_gridController"></param>
    public void SetGridController(GridController _gridController)
    {
        I.gridCtrl = _gridController;
    }

    /// <summary>
    /// Set the reference for the Ingredients controller
    /// </summary>
    /// <param name="_ingredientsController"></param>
    public void SetIngredientsController(IngredientsController _ingredientsController)
    {
        I.ingredientsCtrl = _ingredientsController;
    }

    /// <summary>
    /// Set the reference for the numbers controller
    /// </summary>
    /// <param name="NumbersController"></param>
    public void SetNumbersController(NumbersController _numbersController)
    {
        I.numbersCtrl = _numbersController;
    }

    /// <summary>
    /// Set the reference for the Pool manager
    /// </summary>
    /// <param name="_poolManager"></param>
    public void SetPoolManager(PoolManager _poolManager)
    {
        I.poolMng = _poolManager;
    }

    /// <summary>
    /// Set the reference for the Game controller
    /// </summary>
    /// <param name="_gameController"></param>
    public void SetGameController(GameController _gameController)
    {
        I.gameCtrl = _gameController;
    }

    /// <summary>
    /// Set the reference for the UI manager
    /// </summary>
    /// <param name="_uiManager"></param>
    public void SetUIManager(UIManager _uiManager)
    {
        I.UIMng = _uiManager;
    }

    #endregion


    #region SM triggers
    /// <summary>
    /// Triggera la partenza della State machine
    /// </summary>
    private void StartSM()
    {
        SM.SetTrigger("StartSM");
    }
    /// <summary>
    /// Triggera il cambio di stato per andare nel Setup
    /// </summary>
    public void GoToNext()
    {
        SM.SetTrigger("GoToNext");
    }

    #endregion

}
