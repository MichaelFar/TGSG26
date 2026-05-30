using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionTester : MonoBehaviour
{
    public GameObject testTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            IInteractable interactable = testTarget.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
}
