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
        List<Cell> freeCells = new List<Cell>();
        GameObject parent = new GameObject("IngredientsContainer");

        AddElelemtToList(GameManager.I.GetGridController().GetRandomCell());

        for (int i = 0; i < ingredientsForLevel; i++)
        {
            int currentIndex = i;

            Cell neighbourCell = null;
            do
            {
                neighbourCell = GameManager.I.GetGridController().GetFreeCellFromNeighbours(freeCells[currentIndex]);
                if (neighbourCell != null)
                {
                    AddElelemtToList(neighbourCell);
                    break;
                }
                else
                    currentIndex--;
            } while (neighbourCell == null);
        }

        void AddElelemtToList(Cell _cellToAdd)
        {
            freeCells.Add(_cellToAdd);
            Ingredient instantiatedIngredient = Instantiate(tempObj, _cellToAdd.GetWorldPosition(), Quaternion.identity, parent.transform);
            _cellToAdd.AddIngredient(instantiatedIngredient);
        }
    }



    void InstantiateIngredient(Vector3 _position)
    {
        GameObject ingredientsContainer = new GameObject("Ingredients Container");
        Instantiate(tempObj, _position, Quaternion.identity, ingredientsContainer.transform);
    }
}
