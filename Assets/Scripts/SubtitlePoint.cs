using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SubtitlePoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public SubtitleSequence subSequence;

    public bool triggerOnce = false;

    private SubtitleController subController;

    private Coroutine activeCoroutine;

    public UnityEvent ev_SubtitlesFinished;

    public UnityEvent ev_StartedSubtitles;

    public bool interruptCurrentSubtitles = false;

    private bool hasTriggered = false;
    
    void Start()
    {
        subController = PlayerGlobal.Instance.subController;
        subController.ev_SubtitlesEnded.AddListener(InvokeSubtitleEnded);

    }

    private void Update()
    {
        
    }
    private void InvokeSubtitleEnded()
    {
        ev_SubtitlesFinished.Invoke();
        ev_SubtitlesFinished.RemoveAllListeners();

    }
    // Update is called once per frame
    public void StartSubtitleSequence()
    {
        //subController.ProcessSubtitleList(subSequence);
        if(!hasTriggered && activeCoroutine == null)
        {
            ev_StartedSubtitles.Invoke();
            activeCoroutine = subController.StartCoroutine(nameof(subController.ProcessSubtitleList), subSequence);
            
        }
        else if(!hasTriggered && interruptCurrentSubtitles)
        {
            subController.StopAllCoroutines();
            activeCoroutine = subController.StartCoroutine(nameof(subController.ProcessSubtitleList), subSequence);
        }

            hasTriggered = !triggerOnce;
        
    }
    
}
