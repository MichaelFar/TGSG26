using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;


    // Update is called once per frame
    void Update()
    {
        CheckInteraction(); // Calls Interraction
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
        
        void CheckInteraction()
        {
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); 
           
            if (Physics.Raycast(ray, out hit, playerReach)) 
            {
                if (hit.collider.tag == "Interactable") // looking at interatble object
                {
                    Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                    if (currentInteractable && newInteractable != currentInteractable)
                    {
                        currentInteractable.DisabledOutline();
                    }
                    if (newInteractable.enabled)
                    {
                        SetNewCureentInteractable(newInteractable);
                    }
                    else
                    {
                        DisabledCurrentInteractable();
                    }
                }
                else
                {
                    DisabledCurrentInteractable();
                }
            }
            else // nothing in reach
            {
                DisabledCurrentInteractable();
            }
        }
        
    }

    void SetNewCureentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnabledOutline();
    }

    void DisabledCurrentInteractable()
    {
        if(currentInteractable)
        {
            currentInteractable.DisabledOutline();
            currentInteractable = null;
        }
    }
}
