using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    public IngredientType MyType
    {
        get { return _myType; }
        private set { _myType = value; }
    }
    private IngredientType _myType;


    [SerializeField] private GameObject GraphicContainer;
    private PoolObjectBase graphic;

    internal void Setup(IngredientType _ingredientType)
    {
        graphic = GameManager.I.GetPoolManager().GetFirstAvaiableObject<PoolObjectBase>(_ingredientType.ToString(), GraphicContainer.transform, GraphicContainer.transform.position);
        graphic.transform.parent = GraphicContainer.transform;
        MyType = _ingredientType;
    }


    public enum IngredientType
    {
        Bread = 0, 
        Bacon = 1, 
        Cheese = 2, 
        Egg = 3, 
        Ham = 4, 
        Onion = 5, 
        Salad = 6, 
        Salami = 7, 
        Tomato = 8
    }
}
