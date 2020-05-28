using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : TileBase
{
    public int MyValue { get; private set; }

    public override void TileSetup(object _myValue)
    {
        base.TileSetup(_myValue);
        MyValue = (int)_myValue;
    }

    public void UpdateGraphic(int _newValue)
    {
        MyValue = _newValue;
        RetrieveGraphic();
        TileSetup(_newValue);
    }
}
