using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellRotationController : MonoBehaviour
{
    Animator anim;

    public void Setup()
    {
        anim = GetComponent<Animator>();
    }

    void SetRotation(string _axis, int _direction)
    {
        anim.SetInteger(_axis, _direction);
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
    }
}
