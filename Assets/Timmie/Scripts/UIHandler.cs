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

    public PauseMenu PauseMenuFunctionObject;
    [SerializeField] private NoteUI noteUI;
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
        if (PauseScreen.GameIsPaused && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            //true = resume game
            OnPauseMenuToggled?.Invoke(true);

        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            //false = pause game
            OnPauseMenuToggled?.Invoke(false);
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && currentOpenUI != null)
        {
            CloseUI();
        }
        if (PauseScreen.GameIsPaused)
        {
            return;
        }
        if (currentOpenUI != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            CloseUI();
        }

        if (Keyboard.current.tabKey.wasReleasedThisFrame)
        {
            OnTaskListToggled?.Invoke(false);
        }
        if (currentOpenUI != null) return;

        //If tab is held evoke all functions associated with OnTaskListToggled (look at TaskListUI.cs) 
        if (Keyboard.current.tabKey.IsPressed())
        {
            OnTaskListToggled?.Invoke(true);
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
        if (PauseMenu.Instance.GetGamePaused())
        {
            PauseMenuFunctionObject.SetGamePaused(false);
            PauseScreen.GameIsPaused = false;
        }
    }

    public void ShowNoteUI(NoteData note)
    {
        noteUI.LoadContent(note);
        ShowUI(noteUI);
    }
}
