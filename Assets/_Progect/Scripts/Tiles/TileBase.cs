using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : PoolObjectBase
{
    [SerializeField] private GameObject GraphicContainer;
    private PoolObjectBase graphic;

    public virtual void TileSetup(object _myValue)
    {
        graphic = GameManager.I.GetPoolManager().GetFirstAvaiableObject<PoolObjectBase>(_myValue.ToString(), GraphicContainer.transform, GraphicContainer.transform.position);
    }

    public override void OnRetrieve()
    {
        RetrieveGraphic();
    }

    internal void RetrieveGraphic()
    {
        if (graphic != null)
        {
            Quaternion resetPosition = Quaternion.Euler(0, 180, 0);
            graphic.transform.rotation = resetPosition;
            GameManager.I.GetPoolManager().RetrievePoollable(graphic.ID, graphic);
            graphic = null;
        }
    }
}
