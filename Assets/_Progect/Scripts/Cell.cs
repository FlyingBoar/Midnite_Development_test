using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    public const float ingredientOffset = 0.1f;

    [SerializeField] GameObject ObjectToMove;
    [SerializeField] IngredientRotationController ObjectToRotate;

    Vector3 WorldPosition;
    Vector2Int GridPosition;
    List<Cell> neighbours = new List<Cell>();
    List<Ingredient> cellIngredients;

    public void Setup(Vector3 _worldPosition, Vector2Int _gridPostion)
    {
        WorldPosition = _worldPosition;
        GridPosition = _gridPostion;
        cellIngredients = new List<Ingredient>();
        ObjectToRotate.Setup();
    }

    #region API

    public void AddIngredient(Ingredient _ingredientToAdd)
    {
        cellIngredients.Add(_ingredientToAdd);
        _ingredientToAdd.transform.parent = ObjectToMove.transform;
    }

    public void ClearIngredients()
    {
        cellIngredients.Clear();
    }

    #region Get
    /// <summary>
    /// Return the ingredient contained in this cell, can return null
    /// </summary>
    /// <returns></returns>
    public List<Ingredient> GetIngredients()
    {
        return cellIngredients;
    }

    /////////////////////////////////////////////

    public Ingredient GetFirstIngredient()
    {
        return cellIngredients[0];
    }

    /////////////////////////////////////////////

    public Ingredient GetLastIngredient()
    {
        return cellIngredients[cellIngredients.Count - 1];
    }

    /////////////////////////////////////////////

    public void RemoveLastIngredient()
    {
        cellIngredients.Remove(cellIngredients[cellIngredients.Count]);
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

    internal void TakeNewIngredients(List<Ingredient> _ingredients)
    {
        for (int i = _ingredients.Count - 1; i >= 0; i--)
            GetIngredients().Add(_ingredients[i]);
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

    internal void MoveIngredients(Vector3 newPosition, InputController.SwipeDirection _direction, TweenCallback callBack)
    {
        TweenCallback marge = () =>
        {
            callBack();
            ObjectToMove.transform.localPosition = Vector3.zero;
            ObjectToRotate.ResetRotation();
            InputController.CellInMotion = false;
        };
        float ingredients = GetIngredients().Count;
        ObjectToRotate.transform.localPosition = new Vector3(ObjectToMove.transform.localPosition.x, ingredients / 2 * 0.1f, ObjectToMove.transform.localPosition.z);

        for (int i = 0; i < GetIngredients().Count; i++)
            GetIngredients()[i].transform.parent = ObjectToRotate.transform;

        ObjectToMove.transform.DOJump(newPosition, 0.5f, 1, 0.4f).OnComplete(marge);
        ObjectToRotate.SetRotation(_direction);
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
