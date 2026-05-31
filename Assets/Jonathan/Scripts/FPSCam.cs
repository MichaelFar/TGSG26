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
	}



}
