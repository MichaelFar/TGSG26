using UnityEngine;

public class InspectObjects : MonoBehaviour, IInteractable
{
    private Transform objectToInespect;

    public float rotationSpeed = 100f;

    private Vector3 perviousMousePosition;

    private bool isBeingInteractWith = false;

    private void Start()
    {
        objectToInespect = GetComponent<Transform>();
    }
    public bool CanInteract()
    {
        return !isBeingInteractWith;
    }

    public void OnInteract(GameObject object_interacting = null)
    {
        print("Interative with inecptable object");
        isBeingInteractWith = true;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            print("Holding down left click");
        }
        if(isBeingInteractWith && Input.GetMouseButton(0))
        {
            print("rotating object");
    
            Vector3 deltaMousePosition =  new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            print(deltaMousePosition);

            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            print("rotation X is " + rotationX + " rotation Y is " + rotationY);

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInespect.rotation = rotation * objectToInespect.rotation;

        }
        if(Input.GetMouseButtonUp(1))
        {
            isBeingInteractWith = false;
        }
    }

    public void LookedAway()
    {
        throw new System.NotImplementedException();
    }
}
