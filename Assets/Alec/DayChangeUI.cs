using UnityEngine;
using TMPro;

public class DayChangeUI : MonoBehaviour
{
    private CanvasGroup dayChangeUIGroup;
    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayChangeUIGroup = GetComponent<CanvasGroup>();
        dayChangeUIGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (isOpen = true)
        {
            dayChangeUIGroup.alpha = 1;
        }
        else        
        {
            dayChangeUIGroup.alpha = 0;
        }
    }
}
