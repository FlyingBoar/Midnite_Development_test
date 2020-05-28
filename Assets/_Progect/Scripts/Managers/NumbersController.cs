using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersController : MonoBehaviour
{
    int numbersForLevel => GameManager.I.GetGameController().GetIngredientAmount();
    List<Tile> tilesInLevel = new List<Tile>();
    List<int> instantiatedNumbers = new List<int>();
    public void Setup()
    {
        CreateRandomLevel();
    }

    ////////////////////////////////////////////////////

    void CreateRandomLevel()
    {
        List<Cell> freeCells = new List<Cell>();
        instantiatedNumbers.Clear();

        Cell firstCell = GameManager.I.GetGridController().GetRandomCell();
        freeCells.Add(firstCell);

        //for (int i = 0; i < numbersForLevel; i++)
        //{
        //    int currentIndex = i;
        //    Cell neighbourCell = null;
        //    do
        //    {
        //        neighbourCell = GameManager.I.GetGridController().GetFreeCellFromNeighbours(freeCells[i]);
        //        if (neighbourCell != null)
        //        {
        //            AddElemetToList(neighbourCell, GetRandomNum());
        //            break;
        //        }
        //        else
        //            currentIndex--;
        //    } while (neighbourCell == null);
        //}

        InstantiateTile(GetCell(freeCells[0]), 2);
        InstantiateTile(GetCell(freeCells[1]), 2);
        InstantiateTile(GetCell(freeCells[2]), 4);
        InstantiateTile(GetCell(freeCells[3]), 8);
        InstantiateTile(GetCell(freeCells[4]), 8);
        InstantiateTile(GetCell(freeCells[5]), 8);

        Cell GetCell(Cell _neighbour)
        {
            Cell value = null;
            do
            {
                value = GameManager.I.GetGridController().GetFreeCellFromNeighbours(_neighbour);
                if (value != null)
                    break;
            } while (value == null);
            return value;
        }

        ///Function to instantiate the ingredient on the given cell
        void InstantiateTile(Cell _cellToAdd, int _number)
        {
            freeCells.Add(_cellToAdd);
            InstantiateNumber(_cellToAdd, _number);
        }
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Give each ingredient to the Pool manager and reset the rotation
    /// </summary>
    void RetrieveAllNumbers()
    {
        if (tilesInLevel != null && tilesInLevel.Count > 0)
            for (int i = 0; i < tilesInLevel.Count; i++)
            {
                Quaternion resetPosition = Quaternion.Euler(Vector3.zero);
                tilesInLevel[i].transform.rotation = resetPosition;
                GameManager.I.GetPoolManager().RetrievePoollable(tilesInLevel[i]);
            }
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Instantiate the tile on the given cell with the given number
    /// </summary>
    /// <param name="_cell">Where place the ingredient</param>
    /// <param name="_type">The type of ingredient</param>
    void InstantiateNumber(Cell _cell, int _number)
    {
        Tile instantiatedTile = GameManager.I.GetPoolManager().GetFirstAvaiableObject<Tile>(_cell.transform, _cell.GetWorldPosition());
        instantiatedTile.TileSetup(_number);
        tilesInLevel.Add(instantiatedTile);
        _cell.AddChild(instantiatedTile);
    }

    ////////////////////////////////////////////////////

    int GetRandomNum()
    {
        return 2;
    }

}
