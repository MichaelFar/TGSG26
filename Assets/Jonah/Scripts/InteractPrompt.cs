using UnityEngine;

public class InteractPrompt : MonoBehaviour, IInteractable
{
    private PromptController promptController;

    private Canvas promptCanvas;
    [SerializeField]
    private string description;

    private int frameCount = 0;

    public void SetNotVisible(bool new_value)
    {
        print("prompt should hide");
        promptCanvas.gameObject.SetActive(!new_value);
    }
    public bool CanInteract()
    {
        if (!PauseMenu.Instance.GetGamePaused())
        {
            promptCanvas.gameObject.SetActive(true);
            promptController.SetText(description);
        }
        
        
        
        return true;
    }

    public void OnInteract(GameObject object_interacting = null)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        promptController = PlayerGlobal.Instance.promptController;
        promptCanvas = promptController.GetComponent<Canvas>();
        //PauseMenu.Instance.ev_GamePaused.AddListener(HidePrompt);
        UIHandler.Instance.OnPauseMenuToggled += SetNotVisible;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount += 1;

        if(frameCount% 2 == 0)
        {
            promptCanvas.gameObject.SetActive(false);
           
        }
    }
}
