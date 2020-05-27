using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{

    int ingredientsForLevel => GameManager.I.GetGameController().GetIngredientAmount();
    List<Ingredient> ingredientsInLevel;

    List<IngredientDisposition> levelDisposition;
    
    List<Ingredient.IngredientType> choosedIngredientsForLevel;

    public void Setup()
    {
        CreateRandomLevel();
    }

    ////////////////////////////////////////////////////

    public void CreateRandomLevel()
    {
        RetrieveAllIngredients();
        ingredientsInLevel = new List<Ingredient>();
        levelDisposition = new List<IngredientDisposition>();
        List<Cell> freeCells = new List<Cell>();

        /// Take the index for the bread
        int breadIndex_A = UnityEngine.Random.Range(1, ingredientsForLevel);
        int breadIndex_B;
        breadIndex_B = breadIndex_A - 1;

        choosedIngredientsForLevel = new List<Ingredient.IngredientType>();

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

        ///Function to instantiate the ingredient on the given cell
        void AddElemetToList(Cell _cellToAdd, Ingredient.IngredientType _type)
        {
            freeCells.Add(_cellToAdd);
            InstantiateIngredient(_cellToAdd, _type);
            levelDisposition.Add(new IngredientDisposition(_type, _cellToAdd.GetGridPosition()));
        }
    }

    ////////////////////////////////////////////////////

    internal void LoadLevel(List<IngredientDisposition> _ingredientsDisposition)
    {
        if(_ingredientsDisposition != null && _ingredientsDisposition.Count > 0)
        {
            levelDisposition = _ingredientsDisposition;
            RepositionateIngredients(_ingredientsDisposition);
        }
    }

    ////////////////////////////////////////////////////

    public void RebuildLevel()
    {
        RepositionateIngredients(levelDisposition);
    }

    ////////////////////////////////////////////////////

    public List<IngredientDisposition> GetIngredientsDisposition()
    {
        return levelDisposition;
    }

    ////////////////////////////////////////////////////

    void InstantiateIngredient(Cell _cell, Ingredient.IngredientType _type)
    {
        Ingredient instantiatedIngredient = GameManager.I.GetPoolManager().GetFirstAvaiableObject<Ingredient>(_cell.transform, _cell.GetWorldPosition());
        instantiatedIngredient.Setup(_type);
        ingredientsInLevel.Add(instantiatedIngredient);
        _cell.AddIngredient(instantiatedIngredient);
    }

    ////////////////////////////////////////////////////

    private void RepositionateIngredients(List<IngredientDisposition> _disposition)
    {
        RetrieveAllIngredients();

        foreach (IngredientDisposition disposition in _disposition)
            InstantiateIngredient(GameManager.I.GetGridController().GetCellFromPosition(disposition.CellIndex), disposition.Type);
    }

    ////////////////////////////////////////////////////

    void RetrieveAllIngredients()
    {
        if (ingredientsInLevel != null && ingredientsInLevel.Count > 0)
            for (int i = 0; i < ingredientsInLevel.Count; i++)
            {
                Quaternion resetPosition = Quaternion.Euler(Vector3.zero);
                ingredientsInLevel[i].transform.rotation = resetPosition;
                GameManager.I.GetPoolManager().RetrievePoollable(ingredientsInLevel[i]);
            }
    }

    ////////////////////////////////////////////////////
    
    Ingredient.IngredientType GetRandomType()
    {
        Ingredient.IngredientType selectedType;
        if(choosedIngredientsForLevel.Count < 2)
        {
            do
            {
                selectedType = (Ingredient.IngredientType)UnityEngine.Random.Range(1, 9);
            } while (choosedIngredientsForLevel.Contains(selectedType));
        }
        else
            selectedType = (Ingredient.IngredientType)UnityEngine.Random.Range(1, 9);

        choosedIngredientsForLevel.Add(selectedType);
        return selectedType;
    }

    [System.Serializable]
    public struct IngredientDisposition
    {
        public Ingredient.IngredientType Type;
        public Vector2Int CellIndex;

        public IngredientDisposition(Ingredient.IngredientType _type, Vector2Int _cellIndex)
        {
            Type = _type;
            CellIndex = _cellIndex;
        }
    }
}
