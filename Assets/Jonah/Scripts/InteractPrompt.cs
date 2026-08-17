using UnityEngine;
using System.Collections;
public class InteractPrompt : MonoBehaviour, IInteractable
{
    private PromptController promptController;

    private Canvas promptCanvas;
    [SerializeField]
    private string description;

    private int frameCount = 0;

    private bool ableToDisplayPrompt = true;
    
    public void SetNotVisible(bool new_value)
    {
        if (new_value)
        {
            print("prompt should hide");
            if(!ableToDisplayPrompt)
            {
                return;
            }
        }
        else
        {
            print("prompt should show");
        }
        
        promptCanvas.gameObject.SetActive(new_value);
    }
    

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        promptController = PlayerGlobal.Instance.promptController;
        promptCanvas = promptController.GetComponent<Canvas>();
        //PauseMenu.Instance.ev_GamePaused.AddListener(HidePrompt);
        UIHandler.Instance.OnPauseMenuToggled += SetNotVisible;
        SetNotVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    

    

    public void OnInteract(GameObject object_interacting = null)
    {
        return;
    }

    public bool CanInteract()
    {
        if (!PauseMenu.Instance.GetGamePaused())
        {

            SetNotVisible(true);
            promptController.SetText(description);
            print("Setting prompt active to true");
            //StartCoroutine(HidePrompt());

        }
        return true;
    }

    public void LookedAway()
    {
        SetNotVisible(false);
    }

    public void SetAbleToShowPrompt(bool new_value)
    {
        ableToDisplayPrompt = new_value;
    }
}
