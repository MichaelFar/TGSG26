using System;
using System.Collections;
using UnityEngine;


public class DoorInteraction : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 3f;
    public bool isOpen = false;

    [SerializeField]
    private bool isLocked = true;

    private Quaternion _closedRotation; 
    private Quaternion _openRotation; 
    private Coroutine _currentCoroutine;



    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        //_currentCoroutine = StartCoroutine(ToggleDoor());
    }



    
    public void OnInteract(GameObject object_interacting = null)
    {
        if  (!isLocked)
        {

            if (_currentCoroutine != null) 
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(ToggleDoor());
        }
        print("DoorToggling");
        
    }

    public bool CanInteract()
    {
        return true;
    }

    public void LookedAway()
    {
        return;
    }
    
    

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation; 
        isOpen = !isOpen; 


        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null; 
        }

        transform.rotation = targetRotation;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

}
