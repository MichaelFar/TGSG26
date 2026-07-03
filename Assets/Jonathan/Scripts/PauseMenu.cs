using UnityEngine;

//Info
/*Jonathan Aguilar | 6/1/26 | 
component version of my GameManager pause script, this is component now and GameManager is singleton version of this
notice all the static keywords are gone now, along with the if statement to destroy itself in the Awake()*/

public class PauseMenu : MonoBehaviour
{

    public static PauseMenu Instance { get; private set; }
    private GameInput input;
    private bool gamePaused;

    private void Awake()
    {
        Instance = this;
        input = new GameInput();
        input.Enable();
    }

    private void OnDestroy()
    {
        input.Disable();
    }

    /*private void Update()
    {
        if (input.Menus.PauseGame.triggered)
        {
            SetGamePaused(!gamePaused);

        }
    }
    */
    public void SetGamePaused(bool paused)
    {
        gamePaused = paused;
        Debug.Log("Pause");

        SetCursorState(gamePaused);
        Time.timeScale = gamePaused ? 0 : 1;
    }

    public void SetCursorState(bool enabled)
    {
        Cursor.lockState = enabled
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = enabled;
    }
}
