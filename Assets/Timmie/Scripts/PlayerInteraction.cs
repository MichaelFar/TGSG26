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
    private float interactionRange = 10f;
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

            Debug.DrawRay(ray_origin, playerCam.transform.forward * 10, Color.red, 2, false);
            foreach (RaycastHit hit in hits)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                //checks if interacted item is not null
                if (gameObject)
                {


                    interactable?.OnInteract(gameObject);
                }
            }


        }
    }
}
