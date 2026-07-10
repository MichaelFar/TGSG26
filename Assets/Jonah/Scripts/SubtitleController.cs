using UnityEngine;

public class SubtitleController : MonoBehaviour
{

    public HideHUd hideHudObject;
    public ChangeText changeTextObject;

    private float timeTarget = 0;

    private float timeTracker = 0;

    private bool timerRunning = false;
    void Start()
    {

    }

    void Update()
    {
        if(timerRunning)
        {
            timeTracker += Time.deltaTime;
            if(timeTracker >= timeTarget)
            {
                timeTarget = 0.0f;
                ToggleSubtitleVisibility();
                timerRunning = false;
                timeTracker = 0.0f;
            }
        }
    }
    public void SetSubtitleText(string text)
    {
        changeTextObject.SetText(text);
    }
   
    public void ToggleSubtitleVisibility()
    {
        hideHudObject.ToggleHudVisibility();
    }

    public void DisplaySubtitlesWithTimer(string text, float duration)
    {
        if(!timerRunning)
        {
            ToggleSubtitleVisibility();
            SetSubtitleText(text);
            timeTarget = duration;
            timerRunning = true;
        }
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    
}
