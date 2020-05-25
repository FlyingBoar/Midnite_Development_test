using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    
    /// <summary>
    /// Reference to grid controller
    /// </summary>
    private GridController gridCtrl;

    private IngredientsController ingredientsCtrl;

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

    #endregion

    #region Set
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
