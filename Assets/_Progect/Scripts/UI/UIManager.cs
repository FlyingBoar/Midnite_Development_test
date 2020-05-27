using System;
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

    /// <summary>
    /// Set the current active menu
    /// </summary>
    /// <param name="_type"></param>
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

    /// <summary>
    /// Action when the game is won
    /// </summary>
    internal void GameWon()
    {
        gameplayPanel.FadeText(true);
    }

    /// <summary>
    /// Fade out the gamewon text
    /// </summary>
    internal void ResetGameplayUI()
    {
        gameplayPanel.FadeText(false);
    }

    /// <summary>
    /// Called by the button to go in gameplay state
    /// </summary>
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
