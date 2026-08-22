using DG.Tweening;
using UnityEngine;

public class BillBoardUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera currentCamera;

    public bool lookAtCamera = false;
    public bool maintainLevelToGround = false;

    public bool lookAtPlayer = false;

    public float maxDistance = 2.0f;

    private Vector3 currentAngle;
    void Start()
    {
        if(currentCamera == null)
        {
            currentCamera = Camera.allCameras[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        FaceCamera();
        currentAngle = transform.eulerAngles;
    }
    void FaceCamera()
    {


        if (lookAtCamera)
        {
            transform.LookAt(currentCamera.transform, Vector3.zero);
        }
        else
        {
            transform.forward = currentCamera.transform.forward;
        }
        Vector3 targetAngle = currentCamera.transform.eulerAngles;
        if (lookAtPlayer)
        {
            targetAngle = PlayerGlobal.Instance.playerRootObject.transform.eulerAngles;
                

        }
        else
        {
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, 0, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

            transform.eulerAngles = currentAngle;
                
                
                    
        }
            
        
        
        //transform.rotation = Quaternion.Euler(currentCamera.transform.rotation.x, transform.rotation.y, 0);
        //transform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, playerTransform.rotation.z);
    }
}
