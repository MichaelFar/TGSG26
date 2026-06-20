using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUi;

    public PauseMenu PauseMenuFunctionObject;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {

        PauseMenuFunctionObject.SetGamePaused(false);

        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
        Time.timeScale = 1f;
        */
        PauseMenuUi.SetActive(false);
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUi.SetActive(true);

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
