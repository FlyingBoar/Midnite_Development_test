using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static bool CellInMotion;

    [SerializeField] private float swipeDeadZone;
    private bool isDevice;
    private bool touchBegun;
    private bool inputInterruption;
    private bool dragCompleted;
    private Vector3 startPosition;

    Cell selectedCell;

    public void Setup()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            isDevice = true;
        else
            isDevice = false;

    }

    public void Unsetup()
    {
        CellInMotion = false;
    }

    public void Init()
    {
        
    }

    void LateUpdate()
    {
        if (!GameManager.I.IsGameplay())
            return;

        if (isDevice)
            DetectTouchInput();
        else
            DetectMouseInput();
    }

    //////////////////////////////////////////

    public void StopInput()
    {
        inputInterruption = true;
    }

    //////////////////////////////////////////
    #region Touch Input

    /// <summary>
    /// Detect dell'input su device
    /// </summary>
    void DetectTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        // Touch
        if (touch.phase == TouchPhase.Began)
            BegunInput(touch.position);
        else if (touchBegun == true)
        {
            // Drag
            if (touch.phase == TouchPhase.Moved)
            {
                //Vector2 axis = new Vector2(touch.position.x, touch.position.y) - startPosition;
                OnDrag(touch.position);
            }
            // Release
            else if (touch.phase == TouchPhase.Ended)
                InputEnded();
        }
    }

    #endregion
    //////////////////////////////////////////
    #region Mouse Input

    /// <summary>
    /// Detect dell'input su pc
    /// </summary>
    void DetectMouseInput()
    {
        // click
        if (!touchBegun && Input.GetMouseButtonDown(0))
            BegunInput(Input.mousePosition);
        else if (touchBegun)
        {
            // Drag
            if (Input.GetMouseButton(0) && !inputInterruption && !dragCompleted)
                OnDrag(Input.mousePosition);
            // Release
            else if (Input.GetMouseButtonUp(0))
                InputEnded();
        }
    }

    #endregion
    //////////////////////////////////////////


    void BegunInput(Vector3 _inputPos)
    {
        dragCompleted = false;
        touchBegun = true;
        startPosition = _inputPos;
        Ray ray = Camera.main.ScreenPointToRay(_inputPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300f))
        {
            Cell cellHit = hit.collider.GetComponent<Cell>();
            if (cellHit != null)
            {
                //Debug.Log(cellHit.GetGridPosition());
                selectedCell = cellHit;
            }
        }
    }

    //////////////////////////////////////////

    /// <summary>
    /// Funzione chiamata al drag
    /// </summary>
    /// <param name="_deltaPosition">la differenza fra la start position e la posizione corrente</param>
    void OnDrag(Vector3 _inputPosition)
    {
        if (selectedCell == null || CellInMotion)
            return;

        if (CheckSwipeDistance(_inputPosition))
        {
            if (IsVerticalSwipe(_inputPosition))
            {
                CellInMotion = GameManager.I.GetGridController().Swipe(selectedCell, _inputPosition.y - startPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
            }
            else
            {
                CellInMotion = GameManager.I.GetGridController().Swipe(selectedCell, _inputPosition.x - startPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
            }
            dragCompleted = true;
        }
    }

    //////////////////////////////////////////

    /// <summary>
    /// Una volta che l'input è terminato
    /// </summary>
    /// <param name="_inputPosition"></param>
    void InputEnded()
    {
        selectedCell = null;
        touchBegun = false;
        inputInterruption = false;
        startPosition = Vector3.zero;
    }


    bool CheckSwipeDistance(Vector3 _currentInputPosition)
    {
        return VerticalMovementSwipe(_currentInputPosition) > swipeDeadZone || HorizontalMovementSwipe(_currentInputPosition) > swipeDeadZone;
    }

    bool IsVerticalSwipe(Vector3 _currentInputPosition)
    {
        return VerticalMovementSwipe(_currentInputPosition) > HorizontalMovementSwipe(_currentInputPosition);
    }

    float VerticalMovementSwipe(Vector3 _currentInputPosition)
    {
        return Mathf.Abs(_currentInputPosition.y - startPosition.y);
    }

    float HorizontalMovementSwipe(Vector3 _currentInputPosition)
    {
        return Mathf.Abs(_currentInputPosition.x - startPosition.x);
    }


    public enum SwipeDirection
    {
        Left, Right, Up, Down
    }
}
