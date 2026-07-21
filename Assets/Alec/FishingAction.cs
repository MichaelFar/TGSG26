using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{
    public CanvasGroup FishingQTE;
    public ItemPickup FishingRod;
    public rotateIsActive RotateIsActive;
    public bool isHoldingFishingRod { get { return FishingRod.isHeld; } }
    public bool isFishing = false;
    public float deltaTimer = 0;
    public float timeToHold = 4;
    public bool timeRunning = false; 

    void Start()
    {
        FishingQTE.alpha = 0;
    }

    void Update()
    {
        if (FishingRod.isHeld == true)
        {
            if (timeRunning == true)
            {
                deltaTimer += Time.deltaTime;

                if (deltaTimer >= timeToHold)
                {
                    FishingQTE.alpha = 1;
                    isFishing = true;
                    timeRunning = false;
                    deltaTimer = 0;
                }
            }

            //isHoldingFishingRod = true;
            if (RotateIsActive.canInputAgain == true)
            {
                if (Input.GetMouseButtonDown(0) && isFishing == false)
                {
                 print ("Started Fishing");
                 timeRunning = true;
                 RotateIsActive.canInputAgain = false;
                }
                else if (Input.GetMouseButtonDown(1) && isFishing == true)
                {
                StopFishing();
                timeRunning = false;
                deltaTimer = 0;
                }
            }
        }
    }

    public void StopFishing()
    {
        FishingQTE.alpha = 0;
        isFishing = false;
    }
}
