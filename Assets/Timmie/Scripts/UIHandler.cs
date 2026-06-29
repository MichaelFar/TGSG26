using System;
using UnityEngine;
using UnityEngine.InputSystem;


/*
Contributor(s): Timmie Xiong
Brief Description: Handles all UI displaying in the game. All UI elements talk to this script to display/hide.
Date: 6/25/26
*/
public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }
    public event Action<bool> OnTaskListToggled, OnPauseMenuToggled;
    private BaseUI currentOpenUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && currentOpenUI != null)
        {
            CloseUI();
        }
        //If tab is pressed evoke all functions associated with OnTaskListToggled (look at TaskListUI.cs) 
        if (Keyboard.current.tabKey.IsPressed())
        {
            OnTaskListToggled?.Invoke(true);
        }
        if (Keyboard.current.tabKey.wasReleasedThisFrame)
        {
            OnTaskListToggled?.Invoke(false);
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OnPauseMenuToggled?.Invoke(true);
        }
        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            OnPauseMenuToggled?.Invoke(false);
        }

    }

    public void ShowUI(BaseUI uiPanel)
    {
        if (currentOpenUI != null)
        {
            currentOpenUI.Hide();
        }
        currentOpenUI = uiPanel;
        uiPanel.Show();
    }

    public void CloseUI()
    {
        if (currentOpenUI == null)
        {
            return;
        }
        currentOpenUI.Hide();
        currentOpenUI = null;
    }
}
