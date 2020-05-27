using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public void CheckVictory()
    {
        if (CheckGridCells())
        {
            // Game Won;
            Debug.Log("Game Won!");
        }
        else
        {
            // Do nothing
        }
    }

    /////////////////////////////////////////////

    public bool CheckGridCells()
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

    /////////////////////////////////////////////

    public void RebuildSameLevel()
    {
        UnsetupComponents();
        GameManager.I.GetIngredientsController().RebuildLevel();
    }

    /////////////////////////////////////////////

    public void CreateNewLevel()
    {
        UnsetupComponents();
        GameManager.I.GetIngredientsController().CreateRandomLevel();
    }

    /////////////////////////////////////////////

    public void SaveLevel()
    {
        SaveManager.Save(GameManager.I.GetIngredientsController().GetIngredientsDisposition());
    }
    
    /////////////////////////////////////////////
    
    public void LoadLastSave()
    {
        UnsetupComponents();
        GameManager.I.GetIngredientsController().LoadLevel(SaveManager.LoadLastSavedData());
    }

    /////////////////////////////////////////////

    void UnsetupComponents()
    {
        GameManager.I.GetGridController().ClearCellsChilds();
        GameManager.I.GetInputController().Unsetup();
    }
}
