using UnityEngine;
using UnityEngine.Events;

/*
Contributor(s): Timmie Xiong
Brief Description: A door
Date: 5/25/26
*/

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent doorOpen;
    public int openSpeed = 2;
    private Vector3 closedPosition, openedPosition;
    public Vector3 moveOffset;
    private Vector3 targetLocation;
    private bool isTriggered, isOpen, reachedTarget;
    public void OnInteract(GameObject interacting_object)
    {
        print("Interacted with Door");
        doorOpen.Invoke();
    }
    public void OpenDoor()
    {
        isTriggered = true;
        isOpen = !isOpen;
        if (isOpen)
        {
            targetLocation = openedPosition;
        }
        else
        {
            targetLocation = closedPosition;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpen = false;
        targetLocation = openedPosition;
        closedPosition = transform.position;
        openedPosition = closedPosition + moveOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current_location = transform.position;
        reachedTarget = VectorEquals.Equals(current_location, targetLocation, 0.001f);
        if (isTriggered && !reachedTarget)
        {
            transform.position = Vector3.Lerp(current_location, targetLocation, openSpeed * Time.deltaTime);
        }
        if (reachedTarget)
        {
            isTriggered = false;
        }
    }

    public bool CanInteract()
    {
        return true;
    }

    public void LookedAway()
    {
        throw new System.NotImplementedException();
    }
}
