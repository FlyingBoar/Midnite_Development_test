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
        choosedIngredientsForLevel = new List<Ingredient.IngredientType>();
        CreateRandomLevel();
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Create a random level
    /// </summary>
    public void CreateRandomLevel()
    {
        RetrieveAllIngredients();
        ingredientsInLevel = new List<Ingredient>();
        levelDisposition = new List<IngredientDisposition>();
        List<Cell> freeCells = new List<Cell>();

        /// Take the index for the bread
        choosedIngredientsForLevel.Clear();

        Cell firstBreadCell = GameManager.I.GetGridController().GetRandomCell();

        /// Place the bread in position
        AddElemetToList(firstBreadCell, Ingredient.IngredientType.Bread);
        AddElemetToList(firstBreadCell.GetNeighbours()[Random.Range(0, firstBreadCell.GetNeighbours().Count)], Ingredient.IngredientType.Bread);

        /// Place the other ingredients random next to each others
        for (int i = 0; i < ingredientsForLevel; i++)
        {
            int currentIndex = i;
            Cell neighbourCell = null;
            do
            {
                neighbourCell = GameManager.I.GetGridController().GetFreeCellFromNeighbours(freeCells[Random.Range(0, freeCells.Count)]);
                if (neighbourCell != null)
                {
                    AddElemetToList(neighbourCell, GetRandomType());
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

    /// <summary>
    /// Repositionate the ingredients with the orger given by the Ingredient disposition parameter
    /// </summary>
    /// <param name="_ingredientsDisposition">The list of type of ingredients and position in grid</param>
    internal void LoadLevel(List<IngredientDisposition> _ingredientsDisposition)
    {
        if (_ingredientsDisposition != null && _ingredientsDisposition.Count > 0)
        {
            levelDisposition = _ingredientsDisposition;
            RepositionateIngredients(_ingredientsDisposition);
        }
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Repositionate the ingredients with the start positions of the current level
    /// </summary>
    public void RebuildLevel()
    {
        RepositionateIngredients(levelDisposition);
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Return the list of ingredients position
    /// </summary>
    /// <returns></returns>
    public List<IngredientDisposition> GetIngredientsDisposition()
    {
        return levelDisposition;
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Instantiate the ingredient on the given cell with the given type
    /// </summary>
    /// <param name="_cell">Where place the ingredient</param>
    /// <param name="_type">The type of ingredient</param>
    void InstantiateIngredient(Cell _cell, Ingredient.IngredientType _type)
    {
        Ingredient instantiatedIngredient = GameManager.I.GetPoolManager().GetFirstAvaiableObject<Ingredient>(_cell.transform, _cell.GetWorldPosition());
        instantiatedIngredient.Setup(_type);
        ingredientsInLevel.Add(instantiatedIngredient);
        _cell.AddIngredient(instantiatedIngredient);
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Retrieve all the ingredients to the pool manager and positionate them again with the datas of the ingredient disposition
    /// </summary>
    /// <param name="_disposition">Data of ingredient disposition</param>
    private void RepositionateIngredients(List<IngredientDisposition> _disposition)
    {
        RetrieveAllIngredients();

        foreach (IngredientDisposition disposition in _disposition)
            InstantiateIngredient(GameManager.I.GetGridController().GetCellFromPosition(disposition.CellIndex), disposition.Type);
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Give each ingredient to the Pool manager and reset the rotation
    /// </summary>
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

    /// <summary>
    /// Check if there are at least 2 different ingredient in level than choose random
    /// </summary>
    /// <returns></returns>
    Ingredient.IngredientType GetRandomType()
    {
        Ingredient.IngredientType selectedType;
        if (choosedIngredientsForLevel.Count < 2)
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
