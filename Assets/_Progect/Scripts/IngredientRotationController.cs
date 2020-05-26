using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientRotationController : MonoBehaviour
{
    Animator anim;

    public void Setup()
    {
        anim = GetComponent<Animator>();
    }

    //public void SetRotation(InputController.SwipeDirection _direction)
    //{
    //    Vector3 rotation = new Vector3();
    //    switch (_direction)
    //    {
    //        case InputController.SwipeDirection.Left:
    //            rotation = new Vector3(0, 0, 180f);
    //            break;
    //        case InputController.SwipeDirection.Right:
    //            rotation = new Vector3(0, 0, -180);
    //            break;
    //        case InputController.SwipeDirection.Up:
    //            rotation = new Vector3(179.99f, 0, 0);
    //            break;
    //        case InputController.SwipeDirection.Down:
    //            rotation = new Vector3(-180, 0, 0);
    //            break;
    //        default:
    //            break;
    //    }
    //    SetRotation(rotation, 1);
    //}

    void SetRotation(string _axis, int _direction)
    {
        anim.SetInteger(_axis, _direction);
        //transform.DOLocalRotate(_finalRotation, _time);
    }

    public void SetRotation(InputController.SwipeDirection? _direction)
    {
        string direction = string.Empty;
        int value = 0;
        switch (_direction)
        {
            case InputController.SwipeDirection.Left:
                direction = "Horizontal";
                value = -1;
                break;
            case InputController.SwipeDirection.Right:
                direction = "Horizontal";
                value = 1;
                break;
            case InputController.SwipeDirection.Up:
                direction = "Vertical";
                value = 1;
                break;
            case InputController.SwipeDirection.Down:
                direction = "Vertical";
                value = -1;
                break;
            default:
                break;
        }
        if (direction == string.Empty)
        {
            SetRotation("Horizontal", value);
            SetRotation("Vertical", value);
        }
        else
            SetRotation(direction, value);
    }

    public void ResetRotation()
    {
        SetRotation(null);
        //Quaternion rotation = Quaternion.Euler(Vector3.zero);
        //transform.rotation = rotation;
    }
}
