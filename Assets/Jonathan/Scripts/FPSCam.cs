using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
	public float sensX;
	public float sensY; 

	public Transform Rotation;

	float xRotation;
	float yRotation;

	private void Start()
	{
		// just makes sure the cursor starts in the middle of the screen and is invisible
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible =false; 
	}

	private void Update()
	{
		//Collects the input from the mouse
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		yRotation += mouseX;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		// In Unity to apply rotation u have to apply a Quaternion
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		Rotation.rotation = Quaternion.Euler(0, yRotation, 0);
	}



}
