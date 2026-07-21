using UnityEngine;
using UnityEngine.Events;

public class rotateIsActive : MonoBehaviour
{
    public Fishing FishingActivated;
    public float spinSpeed = 20;
    public float startLocation = 0;
    public float currentLocation = 0;
    public float minimumThreshold = 235;
    public float maximumThreshold = 275;
    
    public int finishedMinigameTimer = 0;
    public bool finishedMinigame = false;
    [HideInInspector]
    public bool canInputAgain = true;

    public UnityEvent onSuccess;
    public UnityEvent onFailure;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedMinigameTimer >= 1)
        {
            FishingActivated.StopFishing();
            canInputAgain = true;
            finishedMinigameTimer = 0;
            finishedMinigame = false;
        }
        
        if (finishedMinigame == true)
        {
            finishedMinigameTimer += 1;
            canInputAgain = false;
        }

        if (FishingActivated.isFishing)
        {
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
            currentLocation = transform.localEulerAngles.z;    

            if (Input.GetMouseButtonDown(0))
                {
                    if (currentLocation > minimumThreshold && currentLocation < maximumThreshold)
                    {
                        //print("Success");
                        onSuccess.Invoke();
                        transform.rotation = Quaternion.Euler(0, 0, startLocation);
                        finishedMinigame = true;

                    }
                    else
                    {
                        //print("Fail");
                        onFailure.Invoke();
                        transform.rotation = Quaternion.Euler(0, 0, startLocation);
                        finishedMinigame = true;
                        
                    }
               
                }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, startLocation);
        }
        
        
        /* if (isFishing.FishingQTE == true)
         {
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
            currentLocation = transform.localEulerAngles.z;    
            if (Input.GetMouseButtonDown(0))
                {
                    if (currentLocation > 235 && currentLocation < 275)
                     {
                        print("Success");
                        
                    }
                    else
                    {
                        print("Fail");
                        
                    }
               
                }
        }
        */
    }
}
