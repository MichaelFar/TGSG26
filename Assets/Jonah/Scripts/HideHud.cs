using UnityEngine;
using TMPro;

public class HideHUd : MonoBehaviour
{
    /*
Contributor(s): Jonah, Timmie, Michael
Brief Description: Changes visibility of the Hud
Date: 6/3/2026
*/

    private CanvasGroup JohnCanvasGroup;


    bool IsVisible = false;


    public void ToggleHudVisibility()
    {
        IsVisible = !IsVisible;

        if (IsVisible)
        {
            JohnCanvasGroup.alpha = 0f;
        }
        else if (!IsVisible)
        {
            JohnCanvasGroup.alpha = 1f;
        }
    }

    public void SetVisibility(bool new_value)
    {
        IsVisible = new_value;
        if (IsVisible)
        {
            JohnCanvasGroup.alpha = 1f;
        }
        else if (!IsVisible)
        {
            JohnCanvasGroup.alpha = 0f;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
       ToggleHudVisibility(); 
    }

    void Awake()
    {
        JohnCanvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
