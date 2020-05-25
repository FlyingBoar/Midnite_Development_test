using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private float cellOffset;
    [SerializeField] private float celldimensions;
    [SerializeField] private int gridDimension_x, gridDimension_y;

    Cell[,] cells;

    private void Start()
    {
        Setup();
    }

    /////////////////////////////////////////////

    public void Setup()
    {
        CreateGrid();
        SetNeighboursForCells();
    }

    /////////////////////////////////////////////
    public Cell GetFreeCellFromNeighbours(Cell _cellToCheck)
    {
        Cell value = null;
        List<Cell> _neighbours = _cellToCheck.GetNeighbours();
        foreach (Cell c in _neighbours)
        {
            if(c.GetIngredient().Count == 0)
            {
                value = c;
                break;
            }
        }
        return value;
    }


    //public List<Cell> GetFreeCellFromNeighbours(int _cellRequired)
    //{
    //    List<Cell> valueToReturn = new List<Cell>();

    //    valueToReturn.Add(GetRandomCell());

    //    for (int i = 0; i < _cellRequired; i++)
    //    {
    //        Cell freeCell = null;
    //        List<Cell> _neighbours = valueToReturn[i].GetNeighbours();
    //        int randomIndex = 0;

    //        do
    //        {
    //            if (_neighbours.Count > 0)
    //            {
    //                randomIndex = Random.Range(0, _neighbours.Count);
    //                Cell cellToCheck = _neighbours[randomIndex];
    //                if (!valueToReturn.Contains(cellToCheck))
    //                    break;
    //                else
    //                    _neighbours.Remove(cellToCheck);
    //            }
    //            else
    //                _neighbours = valueToReturn[i - 1].GetNeighbours();
    //        }
    //        while (true);

    //        freeCell = _neighbours[randomIndex];

    //        valueToReturn.Add(freeCell);

    //    }

    //    return valueToReturn;
    //}


    /////////////////////////////////////////////

    /// <summary>
    /// Return a random Cell
    /// </summary>
    /// <returns></returns>
    public Cell GetRandomCell()
    {
        return cells[Random.Range(0, gridDimension_x - 1), Random.Range(0, gridDimension_y - 1)];
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Create the grid
    /// </summary>
    /// <param name="_center"></param>
    void CreateGrid()
    {
        Vector2 gridCenter = new Vector2(gridDimension_x / 2, gridDimension_y / 2);
        cells = new Cell[gridDimension_x, gridDimension_y];
        for (int i = 0; i < gridDimension_x; i++)
        {
            for (int j = 0; j < gridDimension_y; j++)
            {
                Vector3 cellPosition = new Vector3(-(gridCenter.x - celldimensions / 2) + celldimensions * (i % gridDimension_x), 0, -(gridCenter.y) + (celldimensions + (j % gridDimension_y)));
                cells[i, j] = new Cell(cellPosition, new Vector2(i, j));
            }
        }
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Populate the list of neighbours foreach cell
    /// </summary>
    /// <param name="_cells"></param>
    void SetNeighboursForCells()
    {
        for (int i = 0; i < gridDimension_x; i++)
        {
            for (int j = 0; j < gridDimension_y; j++)
            {
                List<Cell> neighbours = new List<Cell>();

                if (i > 0)
                    neighbours.Add(cells[i - 1, j]);
                if (i < gridDimension_x - 1)
                    neighbours.Add(cells[i + 1, j]);
                if (j > 0)
                    neighbours.Add(cells[i, j - 1]);
                if (j < gridDimension_y - 1)
                    neighbours.Add(cells[i, j + 1]);

                cells[i, j].SetNeighbours(neighbours);
            }
        }
    }

    public Cell[,] GetCells()
    {
        return cells;
    }
}

public class Cell
{
    Vector3 WorldPosition;
    Vector2 GridPosition;
    List<Cell> neighbours = new List<Cell>();
    List<Ingredient> cellIngredients;

    public Cell(Vector3 _worldPosition, Vector2 _gridPostion)
    {
        WorldPosition = _worldPosition;
        GridPosition = _gridPostion;
        cellIngredients = new List<Ingredient>();
    }

    #region API

    public void AddIngredient(Ingredient _ingredientToAdd)
    {
        cellIngredients.Add(_ingredientToAdd);
    }

    #region Get
    /// <summary>
    /// Return the ingredient contained in this cell, can return null
    /// </summary>
    /// <returns></returns>
    public List<Ingredient> GetIngredient()
    {
        return cellIngredients;
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Return the position in world space
    /// </summary>
    /// <returns></returns>
    public Vector3 GetWorldPosition()
    {
        return WorldPosition;
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Return the position inside the grid list
    /// </summary>
    /// <returns></returns>
    public Vector2 GetGridPosition()
    {
        return GridPosition;
    }

    /////////////////////////////////////////////

    /// <summary>
    /// return the list of neighbours
    /// </summary>
    /// <returns></returns>
    public List<Cell> GetNeighbours()
    {
        return neighbours;
    }

    #endregion

    #region Set
    /////////////////////////////////////////////
    ///////////////    SET    ///////////////

    /// <summary>
    /// Set the ingredient contained in the cell
    /// </summary>
    /// <param name="_ingredient"></param>
    public void SetIngredient(List<Ingredient> _ingredient)
    {
        cellIngredients = _ingredient;
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Set the list of neighbours of the cell
    /// </summary>
    /// <param name="_neighbours"></param>
    public void SetNeighbours(List<Cell> _neighbours)
    {
        neighbours = _neighbours;
    }
    #endregion
    #endregion
}