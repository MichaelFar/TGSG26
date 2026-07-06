using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{
    private Canvas FishingQTE;
    
    void Start()
    {
        FishingQTE = GetComponent<Canvas>();
        FishingQTE.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FishingQTE.enabled = true;
        }
    }
}
