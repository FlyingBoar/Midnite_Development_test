using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UI_Gameplay gameplayPanel;
    [SerializeField] UI_Menu menuPanel;

    public MenuType CurrentMenu { get; private set; }

    public void SetCurrentMenu(MenuType _type)
    {
        switch (_type)
        {
            case MenuType.Menu:

                break;
            case MenuType.Gameplay:
                break;
            default:
                break;
        }
    }

    public enum MenuType
    {
        Menu,
        Gameplay
    }
}
