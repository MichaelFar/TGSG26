using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : BaseUI
{
    public static bool GameIsPaused = false;

    public CanvasGroup PauseMenuUi;

    public PauseMenu PauseMenuFunctionObject;
    private BaseUI SettingsMenu, NoteInventoryMenu;

    void Start()
    {
        UIHandler.Instance.OnPauseMenuToggled += SetVisible;
        SettingsMenu = transform.parent.Find("OptionsMenu").GetComponent<BaseUI>();
        NoteInventoryMenu = transform.parent.Find("NotesInventory").GetComponent<BaseUI>();
        print("Found NoteInventoryMenu BaseUI: " + NoteInventoryMenu);
    }

    void OnDestroy()
    {
        if (UIHandler.Instance != null)
        {
            UIHandler.Instance.OnPauseMenuToggled -= SetVisible;
        }
    }
    private void SetVisible(bool isVisible)
    {

        if (isVisible)
        {
            Resume();
            UIHandler.Instance.CloseUI();
        }
        else
        {
            UIHandler.Instance.ShowUI(this);
            Pause();
        }
    }

    public void Resume()
    {

        PauseMenuFunctionObject.SetGamePaused(false);

        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
        Time.timeScale = 1f;
        */
        PauseMenuUi.alpha = 0;
        PauseMenuUi.interactable = false;
        PauseMenuUi.blocksRaycasts = false;
        GameIsPaused = false;
    }

    private void Pause()
    {
        PauseMenuUi.alpha = 1;
        PauseMenuUi.interactable = true;
        PauseMenuUi.blocksRaycasts = true;

        PauseMenuFunctionObject.SetGamePaused(true);
        GameIsPaused = true;

        /*Time.timeScale = 0f;
        
       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        */
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene(0);
    }

    public void ViewNotes()
    {
        UIHandler.Instance.ShowUI(NoteInventoryMenu);
    }
    public void LoadOptions()
    {
        UIHandler.Instance.ShowUI(SettingsMenu);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();

    }

}
