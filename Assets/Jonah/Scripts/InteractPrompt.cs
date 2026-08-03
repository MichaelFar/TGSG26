using UnityEngine;

public class InteractPrompt : MonoBehaviour, IInteractable
{
    
    public bool CanInteract()
    {
        return true;
    }

    public void OnInteract(GameObject object_interacting = null)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
