using UnityEngine;

public class InspectObjects : MonoBehaviour
{
    public Transform objectToInespect;

    public float rotationSpeed = 100f;

    private Vector3 perviousMousePosition;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            perviousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) ;
        {
            Vector3 deltaMousePosition = Input.mousePosition - perviousMousePosition;
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInespect.rotation = rotation * objectToInespect.rotation;

            perviousMousePosition = Input.mousePosition;


        }
    }
}
