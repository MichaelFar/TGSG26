using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;

    public UnityEvent onInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        DisabledOutline();

    }

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void DisabledOutline()
    {
        outline.enabled = false;
    }

    public void EnabledOutline()
    {
        outline.enabled = true;
    }


}
