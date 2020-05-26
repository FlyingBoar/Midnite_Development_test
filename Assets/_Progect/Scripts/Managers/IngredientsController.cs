using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    [SerializeField] int breadQuantity;
    [SerializeField] Ingredient tempObj;
    [SerializeField] int ingredientsForLevel = 4;

    public void Setup()
    {
        // TODO: Temporaneo, da spostare
        Init(); 
    }

    ////////////////////////////////////////////////////

    public void Init()
    {
        ingredientsForLevel = Random.Range(4, 7);
        List<Cell> freeCells = new List<Cell>();

        /// Take the index for the bread
        int breadIndex_A = Random.Range(1, ingredientsForLevel);
        int breadIndex_B;
        breadIndex_B = breadIndex_A - 1 > 0 ? breadIndex_A - 1 : breadIndex_A + 1;

        /// take the cells where place the ingredients
        AddElemetToList(GameManager.I.GetGridController().GetRandomCell(), (breadIndex_A == 0 || breadIndex_B == 0) ? Ingredient.IngredientType.Bread : GetRandomType());
        for (int i = 0; i < ingredientsForLevel; i++)
        {
            int currentIndex = i;
            
            Cell neighbourCell = null;
            do
            {
                neighbourCell = GameManager.I.GetGridController().GetFreeCellFromNeighbours(freeCells[currentIndex]);
                if (neighbourCell != null)
                {
                    AddElemetToList(neighbourCell, (breadIndex_A == i || breadIndex_B == i) && i != 0 ? Ingredient.IngredientType.Bread : GetRandomType());
                    break;
                }
                else
                    currentIndex--;
            } while (neighbourCell == null);
        }

        

        /// Instantiate the ingredient child of the cell
        //for (int i = 0; i < freeCells.Count; i++)
        //{
        //    Ingredient.IngredientType type = Ingredient.IngredientType.Bread;
        //    if (i != breadIndex_A && i != breadIndex_B)
        //        type = GetRandomType();

        //    Ingredient instantiatedIngredient = Instantiate(tempObj, freeCells[i].GetWorldPosition(), Quaternion.identity, freeCells[i].GetGraphicContainer());
        //    instantiatedIngredient.Setup(type);
        //    freeCells[i].AddIngredient(instantiatedIngredient);
        //}

        void AddElemetToList(Cell _cellToAdd, Ingredient.IngredientType _type )
        {
            freeCells.Add(_cellToAdd);
            Ingredient instantiatedIngredient = Instantiate(tempObj, _cellToAdd.GetWorldPosition(), Quaternion.identity, _cellToAdd.GetGraphicContainer());
            instantiatedIngredient.Setup(_type);
            _cellToAdd.AddIngredient(instantiatedIngredient);
        }
    }

    ////////////////////////////////////////////////////

    Ingredient.IngredientType GetRandomType()
    {
        return (Ingredient.IngredientType)Random.Range(1, 9);
    }
}
