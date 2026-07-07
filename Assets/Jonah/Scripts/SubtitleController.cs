using UnityEngine;

public class SubtitleController : MonoBehaviour
{

    public HideHUd hideHudObject;
    public ChangeText changeTextObject;

    public void SetSubtitleText(string text)
    {
        changeTextObject.SetText(text);
    }
   
    public void ToggleSubtitleVisibility()
    {
        hideHudObject.ToggleHudVisibility();
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
