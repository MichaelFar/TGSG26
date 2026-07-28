using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        Debug.Log("Starting New Game...");
        SceneManager.LoadScene(1);
    }

    public void LoadingOptions()
    {
        Debug.Log("Loading Options...");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

}
