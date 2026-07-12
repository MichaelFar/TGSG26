using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{
    public Canvas FishingQTE;
    public ItemPickup FishingRod;
    public bool isHoldingFishingRod { get { return FishingRod.isHeld; } }

    void Start()
    {
        FishingQTE.enabled = false;
    }

    void Update()
    {
        if (FishingRod.isHeld == true)
        {
            //isHoldingFishingRod = true;
            if (Input.GetMouseButtonDown(0))
            {
                FishingQTE.enabled = true;
            }
        }
    }
}
