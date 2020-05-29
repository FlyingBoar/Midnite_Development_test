using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    public const float ingredientOffset = 0.1f;

    [SerializeField] GameObject ObjectToMove;
    [SerializeField] CellRotationController ObjectToRotate;

    Vector3 WorldPosition;
    Vector2Int GridPosition;
    List<Cell> neighbours = new List<Cell>();
    List<PoolObjectBase> cellChildrens;

    public void Setup(Vector3 _worldPosition, Vector2Int _gridPostion)
    {
        WorldPosition = _worldPosition;
        GridPosition = _gridPostion;
        cellChildrens = new List<PoolObjectBase>();
        ObjectToRotate.Setup();
    }

    #region API

    public void AddChild(PoolObjectBase _childToAdd)
    {
        cellChildrens.Add(_childToAdd);
        _childToAdd.transform.parent = ObjectToMove.transform;
    }

    /////////////////////////////////////////////

    public void ClearIngredients()
    {
        cellChildrens.Clear();
    }

    /////////////////////////////////////////////

    public void RemoveLastIngredient()
    {
        cellChildrens.Remove(cellChildrens[cellChildrens.Count]);
    }

    /////////////////////////////////////////////

    internal void TakeNewObject(List<PoolObjectBase> _ingredients)
    {
        for (int i = _ingredients.Count - 1; i >= 0; i--)
            GetChildrens().Add(_ingredients[i]);
    }

    /////////////////////////////////////////////

    /// <summary>
    /// Set the ingredient contained in the cell
    /// </summary>
    /// <param name="_ingredient"></param>
    public void SetChildrens(List<PoolObjectBase> _ingredient)
    {
        cellChildrens = _ingredient;
    }

    internal void MoveObjects(Vector3 newPosition, InputController.SwipeDirection _direction, TweenCallback callBack)
    {
        TweenCallback margeCallback = () =>
        {
            callBack();
            ObjectToMove.transform.localPosition = Vector3.zero;
            ObjectToRotate.ResetRotation();
            InputController.CellInMotion = false;
        };
        float ingredients = GetChildrens().Count;
        ObjectToRotate.transform.localPosition = new Vector3(ObjectToMove.transform.localPosition.x, ingredients / 2 * 0.1f, ObjectToMove.transform.localPosition.z);

        for (int i = 0; i < GetChildrens().Count; i++)
            GetChildrens()[i].transform.parent = ObjectToRotate.transform;

        ObjectToMove.transform.DOJump(newPosition, 0.5f, 1, 0.4f).OnComplete(margeCallback);
        ObjectToRotate.SetRotation(_direction);
    }

    #region Get
    /// <summary>
    /// Return the ingredient contained in this cell, can return null
    /// </summary>
    /// <returns></returns>
    public List<PoolObjectBase> GetChildrens()
    {
        return cellChildrens;
    }

    /////////////////////////////////////////////

    public PoolObjectBase GetFirstChild()
    {
        return cellChildrens[0];
    }

    /////////////////////////////////////////////

    public PoolObjectBase GetLastIngredient()
    {
        return cellChildrens[cellChildrens.Count - 1];
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
    public Vector2Int GetGridPosition()
    {
        return GridPosition;
    }

    /////////////////////////////////////////////

    public Transform GetGraphicContainer()
    {
        return ObjectToMove.transform;
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

    public void CheckCombinations()
    {
        if (GetChildrens().Count > 1)
        {
            bool noMoreCombinationAvaiable = false;

            while (!noMoreCombinationAvaiable)
            {
                noMoreCombinationAvaiable = CompleteCicle();
            }
        }

        bool CompleteCicle()
        {
            bool noMoreInteraction = true;
            int previousChildNumber = -1;
            for (int i = 0; i < GetChildrens().Count; i++)
            {
                Tile currentTile = (GetChildrens()[i] as Tile);
                if (currentTile.MyValue != previousChildNumber)
                    previousChildNumber = currentTile.MyValue;
                else
                {
                    currentTile.UpdateGraphic(currentTile.MyValue * 2);
                    GameManager.I.GetPoolManager().RetrievePoollable(GetChildrens()[i - 1] as Tile);
                    GetChildrens().RemoveAt(i - 1);
                    i--;

                    noMoreInteraction = false;
                    if (GetChildrens().Count > 1)
                        previousChildNumber = currentTile.MyValue;
                }
            }

            if (!noMoreInteraction)
                for (int i = 0; i < GetChildrens().Count; i++)
                {
                    Tile currentTile = (GetChildrens()[i] as Tile);
                    currentTile.transform.position = new Vector3(currentTile.transform.position.x, i * GetChildrens().Count * ingredientOffset, currentTile.transform.position.z);
                }

            return noMoreInteraction;
        }
    }

}
