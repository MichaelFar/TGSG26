using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SubtitleController : MonoBehaviour
{

    public HideHUd hideHudObject;
    public ChangeText changeTextObject;

    private float timeTarget = 0;

    private float timeTracker = 0;

    private bool timerRunning = false;

    public UnityEvent ev_SubtitlesEnded;
    void Awake()
    {
        PlayerGlobal.Instance.subController = this;
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
            
        }
        SetSubtitleText(text);
        timeTarget = duration;
        timerRunning = true;
        timeTracker = 0.0f;

    }

    public IEnumerator SubtitleTimerCoroutine()
    {


        yield return new WaitForSeconds(timeTarget);
            
        ToggleSubtitleVisibility();
            
            
        
        
    }
    public IEnumerator ProcessSubtitleList(SubtitleSequence this_sequence)
    {
        if(timerRunning)
        {
            timerRunning = false;
            timeTracker = 0;
        }
        for (int i = 0; i < this_sequence.subtitleArray.Length; i++)
        {
            print("Subtitle text is: " + this_sequence.subtitleArray[i]);
            hideHudObject.SetVisibility(true);
            SetSubtitleText(this_sequence.subtitleArray[i]);
            yield return new WaitForSeconds(this_sequence.GetDurationOfLine(i));
        }
        ev_SubtitlesEnded.Invoke();
        hideHudObject.SetVisibility(false);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame

}
