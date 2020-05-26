using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UI_Gameplay gameplayPanel;
    [SerializeField] UI_Menu menuPanel;

    public MenuType CurrentMenu { get; private set; }

    public void Setup()
    {
        gameplayPanel.Setup();
        menuPanel.Setup();
    }

    public void SetCurrentMenu(MenuType _type)
    {
        switch (_type)
        {
            case MenuType.Menu:
                menuPanel.SetStatus(true);
                gameplayPanel.SetStatus(false);
                break;
            case MenuType.Gameplay:
                menuPanel.SetStatus(false);
                gameplayPanel.SetStatus(true);
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        GameManager.I.GoToNext();
    }

    public enum MenuType
    {
        Menu,
        Gameplay
    }
}
