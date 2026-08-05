using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction: MonoBehaviour
{
    private Camera playerCam;
    [SerializeField]
    private float interactionRange = 10f;
    [SerializeField]
    private float passiveInteractionRange = 50f;

    [SerializeField]
    private float viewRaycastInterval = 1.0f;

    [SerializeField]
    private LayerMask passiveInteractionLayer;

    [SerializeField]
    private LayerMask interactionLayer;


    //Jonah added
    /*public Text interactionText;

    public GameObject interactionUI;

    string GetDescription()
    {
        return interactionText.text;
    }
    */

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        Vector3 ray_origin = playerCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));

        RaycastHit[] hits = Physics.RaycastAll(ray_origin, playerCam.transform.forward, interactionRange);
        
        
        //Jonah added
        //bool hitSomething = false;

        Debug.DrawRay(ray_origin, playerCam.transform.forward * interactionRange, Color.red, 2, false);
        foreach (RaycastHit hit in hits)
        {
            IInteractable[] interactables = hit.collider.GetComponents<IInteractable>();
            //checks if interacted item is not null
            foreach(IInteractable interactable in interactables) 
            {
                if (gameObject)
                {
                    if (interactable != null)
                    {
                        if (!interactable.CanInteract())
                        {

                            continue;
                        }
                        else
                        {
                            print("Interactable is able to interact");
                        }
                    }



                }
            }

            //Jonah Added
            /*if (interactables != null)
            {
                hitSomething = true;
                interactionText.text = GetDescription();
            }
            */
        }
        
        //interactionUI.SetActive(hitSomething);
    }
   
}