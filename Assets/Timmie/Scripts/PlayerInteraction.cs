using UnityEngine;
using UnityEngine.InputSystem;


/*
Contributor(s): Timmie Xiong
Brief Description: Handles player interaction using raycasting to check if the hit object has an IInteractable interface
Date: 6/1/26
*/
public class PlayerInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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


    private float viewRaycastTimeTracker = 0.0f;
    void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ray_origin = playerCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));
        if (Keyboard.current.fKey.wasPressedThisFrame && !PauseMenu.Instance.GetGamePaused())
        {
            RaycastHit[] hits = Physics.RaycastAll(ray_origin, playerCam.transform.forward, interactionRange);

            
            Debug.DrawRay(ray_origin, playerCam.transform.forward * interactionRange, Color.red, 2, false);
            foreach (RaycastHit hit in hits)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                //checks if interacted item is not null
                if (gameObject)
                {
                    if(interactable != null)
                    {
                        if(!interactable.CanInteract())
                        {
                            
                            continue;
                        }
                        else
                        {
                            print("Interactable is able to interact");
                        }
                    }

                    interactable?.OnInteract(gameObject);
                    
                }
            }


        }

        viewRaycastTimeTracker += Time.deltaTime;
        if(viewRaycastTimeTracker >= viewRaycastInterval)
        {
            viewRaycastTimeTracker = 0.0f;
            RaycastHit[] hits = Physics.SphereCastAll(playerCam.transform.position, 3.0f, playerCam.transform.forward, passiveInteractionRange, passiveInteractionLayer);

            Debug.DrawRay(ray_origin, playerCam.transform.forward * passiveInteractionRange, Color.red, 2, false);
            foreach (RaycastHit hit in hits)
            {
                IViewable viewable = hit.collider.GetComponent<IViewable>();
                //checks if interacted item is not null
                if (gameObject)
                {

                    viewable?.OnView();

                }
            }
        }

    }
}
