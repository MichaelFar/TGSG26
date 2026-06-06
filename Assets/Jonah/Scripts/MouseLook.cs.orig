using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

   // float smoothMouseX;

    //float smoothMod = 10.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //smoothMouseX = Mathf.Lerp(smoothMouseX, mouseX, smoothMod * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
