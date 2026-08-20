using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
                var found = hit.collider.GetComponents<IInteractable>();
                print($"Found {found.Length} interactables: " + string.Join(", ", System.Array.ConvertAll(found, i => i.GetType().Name)));
                foreach (IInteractable interactable in hit.collider.GetComponents<IInteractable>())
                {
                    //checks if interacted item is not null
                    print("Loop iteration: " + interactable.GetType().Name);
                    if (gameObject)
                    {
                        if (interactable != null)
                        {
                            if (!interactable.CanInteract())
                            {
                                print(interactable.GetType().Name + " CanInteract() returned false, skipping");
                                continue;
                            }
                            else
                            {
                                print(interactable.GetType().Name + " is able to interact");
                            }
                        }
                        print("About to call OnInteract on " + interactable.GetType().Name);
                        try
                        {
                            interactable?.OnInteract(gameObject);

                        }
                        catch (System.Exception e)
                        {
                            print("EXCEPTION in " + interactable.GetType().Name + ": " + e.Message + "\n" + e.StackTrace);
                        }
                        print("Finished OnInteract on " + interactable.GetType().Name);
                    }
                }
            }


        }

        viewRaycastTimeTracker += Time.deltaTime;
        if (viewRaycastTimeTracker >= viewRaycastInterval && !PauseMenu.Instance.GetGamePaused())
        {
            viewRaycastTimeTracker = 0.0f;
            RaycastHit[] hits = Physics.SphereCastAll(playerCam.transform.position, 3.0f, playerCam.transform.forward, passiveInteractionRange, passiveInteractionLayer);

            Debug.DrawRay(ray_origin, playerCam.transform.forward * passiveInteractionRange, Color.blue, 2, false);
            foreach (RaycastHit hit in hits)
            {
                IViewable[] viewables = hit.collider.GetComponents<IViewable>();



                //checks if interacted item is not null
                foreach (IViewable viewable in viewables)
                {

                    if (gameObject)
                    {

                        viewable?.OnView();

                    }
                }
            }
        }

    }
}
