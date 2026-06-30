using UnityEngine;

public class PauseScreen : BaseUI
{
    public static bool GameIsPaused = false;

    public CanvasGroup PauseMenuUi;

    public PauseMenu PauseMenuFunctionObject;

    void Start()
    {
        UIHandler.Instance.OnPauseMenuToggled += SetVisible;
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
        PauseMenuUi.alpha = isVisible ? 1 : 0;
        if (isVisible)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Resume()
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
    }

    public void LoadOptions()
    {
        Debug.Log("Loading Options...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();

    }

}
