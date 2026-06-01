using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/*
Contributor(s): Timmie Xiong
Brief Description: Just a testing script to test interaction with objects
Date: 5/25/26
*/

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
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            IInteractable interactable = testTarget.GetComponent<IInteractable>();
            //checks if interacted item is not null
            interactable?.OnInteract();
        }
    }
}
