using DG.Tweening;
using System.Collections;
using UnityEngine;
public class DoorController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
    public GameObject rotationParent;

    [SerializeField]
    private float rotationStrength = 90.0f;
    [SerializeField]
    private float rotateTime = 1.0f;

    private bool isOpen = false;

    [SerializeField]
    private bool isLocked = false;

    private Vector3 openRotation;
    private Vector3 closeRotation;

    private float currentTimeOnCoroutine;
    private float initialTimeOnCoroutine;

    private Coroutine activeCoroutine;
    void Start()
    {
        
        if(!rotationParent)
        {
            rotationParent = gameObject;
        }
        openRotation = rotationParent.transform.rotation.eulerAngles + (rotationAxis * rotationStrength);
        closeRotation = rotationParent.transform.rotation.eulerAngles + (-1 * rotationAxis * rotationStrength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        if(!isLocked && !isOpen)
        {
            if (activeCoroutine == null)
            {
                activeCoroutine = StartCoroutine(DoorActionCoroutine(1, rotateTime));
                initialTimeOnCoroutine = rotateTime;
            }
            else
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = StartCoroutine(DoorActionCoroutine(1, 1.0f - currentTimeOnCoroutine));
            }
            isOpen = true;
        }
            
        
    }
    public void CloseDoor()
    {
        if(isOpen)
        {
            if (activeCoroutine == null)
            {
                activeCoroutine = StartCoroutine(DoorActionCoroutine(-1, rotateTime));
                initialTimeOnCoroutine = rotateTime;
            }
            else
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = StartCoroutine(DoorActionCoroutine(-1, 1.0f - currentTimeOnCoroutine));
            }
            isOpen = false;
        }

            
    }
    IEnumerator DoorActionCoroutine(float direction, float time)
    {
        currentTimeOnCoroutine = time;
        rotationParent.transform.Rotate(rotationAxis * rotationStrength * direction * Time.deltaTime);
        yield return new WaitForEndOfFrame();
        if (time > 0)
        {
           StartCoroutine(DoorActionCoroutine(direction, time -= Time.deltaTime));
        }
        
    }

    public void SetIsLocked(bool new_value)
    {
        isLocked = new_value;
    }
}
