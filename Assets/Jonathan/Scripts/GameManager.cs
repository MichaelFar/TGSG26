using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	//reference to new input system
	public static GameInput Input {get; private set; }
	public static bool GamePaused { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		//reference to new input system
		Input = new GameInput();
		Input.Enable();

	}

	private void OnDestroy()
	{
		//reference to new input system

		// if input is not equal null and Instancce == this 
		if (Input != null && Instance == this)
		{
			Input.Disable();
		}
	}

	private void Update()
	{// This is essenatially Input.GetKeyDown(KeyCode.Escape)) but using the new input system
		if (Input.Menus.PauseGame.triggered)
		SetGamePaused(!GamePaused);
		
	}

	public static void SetGamePaused(bool paused)
	{
		GamePaused = paused;
		Debug.Log("Pause");

		SetCursorState(GamePaused);
		Time.timeScale = GamePaused ? 0 : 1;
	}

	public static void SetCursorState(bool enabled)
	{
		// "Cursor.lockState = enabled ?" | This is now an if statement
		// Cursor.lockState = if enabled then CursorLockMode.None | else CursorLockMode.locked
		Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
		// Cursor will be visible if enabled
		Cursor.visible = enabled; 
	}

}
