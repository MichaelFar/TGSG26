using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Jonathan Aguilar, Michael Farrar | 7/31/26 |
/// Description modified by Michael: Add this to any object that should produce the musical sting and the vignette
/// Uses IViewable to accomplish the trigger and detection, hook events into the SetCanTrigger functions to control retriggering
/// Otherwise, use the public bool triggerOnce in the inspector to control how often this event can trigger
/// 
/// 
/// </summary>

// I apalogize for all the notes, its all to help not only u the reader but for me in the future since I'm forgetful

public class SpookyOnViewComponent : MonoBehaviour, IViewable
{
    
    public Image vignetteImage;

    [Tooltip("How dark/opaque the vignette gets at full intensity (0 = invisible, 1 = fully black).")]
    [Range(0f, 1f)]
    public float maxVignetteAlpha = 0.6f;

    [Tooltip("How many seconds it takes to fade the vignette in or out.")]
    public float fadeDuration = 1f;

    


    // Tracks which WanderAI objects the player is already inside of
    // so we only log on enter/exit rather than every frame
    private System.Collections.Generic.HashSet<WanderAI2> currentlyInRange
    = new System.Collections.Generic.HashSet<WanderAI2>();
    // as long as we use the same wandering AI2 script then this should work or if we make a specail AI that also needs 
    // to work with this then we can make some tweaks

    private Coroutine fadeCoroutine;
    private AudioSource audioSource;

    public float effectLength = 5.0f;

    private float effectTimer = 0.0f;

    private bool effectActive = false;

    public bool triggerOnce = true;

    private bool canTrigger = true;
    private void Start()
    {
        
        /*
        // Make sure vignette starts invisible
        if (vignetteImage != null)
            SetVignetteAlpha(0f);

        // Add an AudioSource to the player automatically so we don't need one manually
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.clip = suspenseClip;
        */
                
    }


    private void Update()
    {    
        if(effectActive)
        {
            effectTimer += Time.deltaTime;
            print("Vignette effect timer running");
            if(effectTimer >= effectLength)
            {
                effectTimer = 0.0f;
                effectActive = false;
                StopEffect();
                print("Stopping effect");
                canTrigger = !triggerOnce;
            }
        }
    }


   
    

    

    public void OnView()
    {
        if(canTrigger)
        {
            print("Vignette effect started");
            PlayerGlobal.Instance.scareStingController.FadeVignette(maxVignetteAlpha);
            PlayerGlobal.Instance.scareStingController.PlaySuspenseAudio();
            effectActive = true;
        }
            
    }
    public void StopEffect()
    {
        print("Ending vignette effect");
        PlayerGlobal.Instance.scareStingController.FadeVignette(0f);
        PlayerGlobal.Instance.scareStingController.StopSuspenseAudio();
    }
    public void SetCanTriggerToTrue()
    {
        canTrigger = true;
    }
    public void SetCanTriggerToFalse()
    {
        canTrigger = false;
    }
}