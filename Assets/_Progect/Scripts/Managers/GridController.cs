using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private int gridDimension_x, gridDimension_y;
    [SerializeField] private Cell cellPrefab;
    private float spaceFromCellCenters = 1;

    Cell[,] cells;

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
            if (c.GetIngredients().Count == 0)
            {
                value = c;
                break;
            }
        }
        return value;
    }

    /////////////////////////////////////////////

    public void Swipe(Cell _selectedCell, InputController.SwipeDirection _direction)
    {
        Cell c = GetCellByDirection(_selectedCell, _direction);

        if (_selectedCell == null || c == null)
            return;

        if (_selectedCell.GetIngredients().Count > 0 && c.GetIngredients().Count > 0)
        {
            Vector3 newPos = new Vector3(c.GetWorldPosition().x, c.GetWorldPosition().y + c.GetIngredients().Count * Cell.ingredientOffset, c.GetWorldPosition().z);

            _selectedCell.MoveIngredients(newPos, _direction, () =>
            {
                if (_selectedCell.GetIngredients().Count > 1)
                {
                    for (int i = _selectedCell.GetIngredients().Count - 1; i >= 0; i--)
                        c.AddIngredient(_selectedCell.GetIngredients()[i]);
                }
                else
                    c.AddIngredient(_selectedCell.GetIngredients()[0]);

                _selectedCell.ClearIngredients();
                GameManager.I.GetGameController().CheckVictory();
            });
        }
    }
    
    /////////////////////////////////////////////

    internal void ClearCellsChilds()
    {
        for (int j = 0; j < cells.GetLength(0); j++)
            for (int k = 0; k < cells.GetLength(1); k++)
                cells[j, k].ClearIngredients();
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Return a random Cell
    /// </summary>
    /// <returns></returns>
    public Cell GetRandomCell()
    {
        return cells[UnityEngine.Random.Range(0, gridDimension_x - 1), UnityEngine.Random.Range(0, gridDimension_y - 1)];
    }

    /////////////////////////////////////////////

    public Cell[,] GetCells()
    {
        return cells;
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
        for (int i = 0; i < gridDimension_y; i++)
        {
            for (int j = 0; j < gridDimension_x; j++)
            {
                Vector3 cellPosition = new Vector3(-(gridCenter.x - spaceFromCellCenters / 2) + spaceFromCellCenters * (j % gridDimension_x), 0, -(gridCenter.y - spaceFromCellCenters / 2) + (spaceFromCellCenters * (i % gridDimension_y)));
                Cell newCell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform);
                newCell.gameObject.name = j + ":" + i;
                newCell.Setup(cellPosition, new Vector2Int(j, i));
                cells[j, i] = newCell;
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

    /////////////////////////////////////////////

    /// <summary>
    /// Return the cell next to the start cell given by direction 
    /// </summary>
    /// <param name="_startCell"></param>
    /// <param name="_direction"></param>
    /// <returns></returns>
    Cell GetCellByDirection(Cell _startCell, InputController.SwipeDirection _direction)
    {
        Cell value = null;
        switch (_direction)
        {
            case InputController.SwipeDirection.Left:
                if (_startCell.GetGridPosition().x > 0)
                {
                    value = cells[_startCell.GetGridPosition().x - 1, _startCell.GetGridPosition().y];
                }
                break;
            case InputController.SwipeDirection.Right:
                if (_startCell.GetGridPosition().x < gridDimension_x - 1)
                {
                    value = cells[_startCell.GetGridPosition().x + 1, _startCell.GetGridPosition().y];
                }
                break;
            case InputController.SwipeDirection.Up:
                if (_startCell.GetGridPosition().y < gridDimension_y - 1)
                {
                    value = cells[_startCell.GetGridPosition().x, _startCell.GetGridPosition().y + 1];
                }
                break;
            case InputController.SwipeDirection.Down:
                if (_startCell.GetGridPosition().y > 0)
                {
                    value = cells[_startCell.GetGridPosition().x, _startCell.GetGridPosition().y - 1];
                }
                break;
            default:
                break;
        }
        return value;
    }
}