using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : TileBase
{
    public IngredientType MyType { get; private set; }

    public override void TileSetup(object _myValue)
    {
        base.TileSetup(_myValue);
        MyType = (IngredientType)_myValue;
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
