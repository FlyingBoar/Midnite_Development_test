using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public bool RandomizeIngredientQuantity;
    [SerializeField]
    public int MaxIngredientQuantity = 5;
    [SerializeField]
    public int FixedIngredientQuantity = 4;

    /// <summary>
    /// Check victory conditions
    /// </summary>
    public void CheckVictory()
    {
        if (CheckGridCells())
        {
            // Game Won;
            GameManager.I.GetUIManager().GameWon();
            Debug.Log("Game Won!");
        }
        else
        {
            // Do nothing
        }
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Create again the current level
    /// </summary>
    public void RebuildSameLevel()
    {
        UnsetupComponents();
        GameManager.I.GetIngredientsController().RebuildLevel();
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Generate a new random level
    /// </summary>
    public void CreateNewLevel()
    {
        UnsetupComponents();
        GameManager.I.GetIngredientsController().CreateRandomLevel();
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Call the function to save the current level
    /// </summary>
    public void SaveLevel()
    {
        SaveManager.Save(GameManager.I.GetIngredientsController().GetIngredientsDisposition());
    }
    
    /////////////////////////////////////////////
    
    /// <summary>
    /// Call the function to load the last level saved
    /// </summary>
    public void LoadLastSave()
    {
        UnsetupComponents();
        GameManager.I.GetIngredientsController().LoadLevel(SaveManager.LoadLastSavedData());
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Return the quantity of ingredient for the level
    /// </summary>
    /// <returns></returns>
    public int GetIngredientAmount()
    {
        int value = FixedIngredientQuantity + 1;
        if (RandomizeIngredientQuantity)
            value = Random.Range(4, MaxIngredientQuantity + 1);

        return value;
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Clear the ingredients for all the cells in grid and clear the input controller if there was an ingredient moving
    /// </summary>
    void UnsetupComponents()
    {
        GameManager.I.GetGridController().ClearCellsChilds();
        GameManager.I.GetInputController().Unsetup();
        GameManager.I.GetUIManager().ResetGameplayUI();
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Check if there is more than one cell with ingredients, if there is only one cell with ingredients check if the bread is on top and bottom
    /// </summary>
    /// <returns></returns>
    bool CheckGridCells()
    {
        bool value = false;
        var cells = GameManager.I.GetGridController().GetCells();

        Cell notEmptyCells = null;

        for (int j = 0; j < cells.GetLength(0); j++)
        {
            for (int k = 0; k < cells.GetLength(1); k++)
            {
                if (cells[j, k].GetIngredients().Count != 0)
                {
                    if (notEmptyCells == null)
                        notEmptyCells = cells[j, k];
                    else
                        return value;
                }
            }
        }

        if (notEmptyCells != null)
            if (notEmptyCells.GetFirstIngredient().MyType == Ingredient.IngredientType.Bread && notEmptyCells.GetLastIngredient().MyType == Ingredient.IngredientType.Bread)
                value = true;

        return value;
    }
}
