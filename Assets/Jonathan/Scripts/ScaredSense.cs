using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Jonathan Aguilar | 7/31/26 |
/// ScaredSense — attach this directly to the Player GameObject.
/// It scans for nearby WanderAI objects and logs + activates vignette when the player
/// enters any of their wander radius.
///
/// SETUP:
///  1. Attach this script to your Player GameObject.
///  2. Add Image to UI Canvas
///  3. Set image source to vignette sprite or not, the script should be able to handle its transparency
///  4. Grab and drop that image from the UI Canvas to the slot avaible in the inspector when you attach this script
///  5. Set the Image's RectTransform to stretch across the fullscreen( anchor to all corners )
/// 
/// </summary>

// I apalogize for all the notes, its all to help not only u the reader but for me in the future since I'm forgetful

public class ScaredSense : MonoBehaviour
{
    [Header("Vignette")]
    [Tooltip("A fullscreen UI Image, this will act as our vignette overlay")]
    public Image vignetteImage;

    [Tooltip("How dark/opaque the vignette gets at full intensity (0 = invisible, 1 = fully black).")]
    [Range(0f, 1f)]
    public float maxVignetteAlpha = 0.6f;

    [Tooltip("How many seconds it takes to fade the vignette in or out.")]
    public float fadeDuration = 1f;

    [Header("Audio")]
    [Tooltip("The suspense sound that plays when the player enters a wanderer's radius.")]
    public AudioClip suspenseClip;


        // Tracks which WanderAI objects the player is already inside of
        // so we only log on enter/exit rather than every frame
        private System.Collections.Generic.HashSet<WanderAI2> currentlyInRange
        = new System.Collections.Generic.HashSet<WanderAI2>();
        // as long as we use the same wandering AI2 script then this should work or if we make a specail AI that also needs 
        // to work with this then we can make some tweaks

        private Coroutine fadeCoroutine;
         private AudioSource audioSource;


        private void Start()
        {
            PlayerGlobal.Instance.scareStingController = this;
            // Make sure vignette starts invisible
            if (vignetteImage != null)
                SetVignetteAlpha(0f);

                // Add an AudioSource to the player automatically so we don't need one manually
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.clip = suspenseClip;
        }

    private void Update()
    {   // an array or basically list of wander ai objs | allWanderers is just the name I gave it 
        // the FindObjectsByType<WanderAI2> | is only going to grab every wanderAI2 component in the entire scene then puts them all in that list
        /*
        WanderAI2[] allWanderers = FindObjectsByType<WanderAI2>(FindObjectsSortMode.None);

            // the wanderer is just the NPCs that wanderAI2 script attached to it
        foreach (WanderAI2 wanderer in allWanderers)
        {
            float distance = Vector3.Distance(transform.position, wanderer.transform.position);
            bool isInRange = distance <= wanderer.wanderRadius;

            if (isInRange && !currentlyInRange.Contains(wanderer))
            {
                // player in range of wanderer 
                currentlyInRange.Add(wanderer);
                Debug.Log($"[ScaredSense] The player feels something is off near | in range {wanderer.gameObject.name}...");
                FadeVignette(maxVignetteAlpha);
                PlaySuspenseAudio();
            }
            else if (!isInRange && currentlyInRange.Contains(wanderer))
            {
                // player left range of wanderer
                currentlyInRange.Remove(wanderer);
                Debug.Log($"[ScaredSense] The feeling fades near | leaving range {wanderer.gameObject.name}.");
                if (currentlyInRange.Count == 0)
                {
                    FadeVignette(0f);
                    StopSuspenseAudio();
                }
            }
        }
        */
    }

    // ---------------------------------------------------------------
    // you can ignore
    // vignette function stuff below
 
    public void FadeVignette(float targetAlpha)
    {
        if (vignetteImage == null) return;
 
        // Cancel any fade already in progress before starting a new one
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
 
        fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));
    }
 
    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = vignetteImage.color.a;
        float elapsed = 0f;
 
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            SetVignetteAlpha(newAlpha);
            yield return null;
        }
 
        SetVignetteAlpha(targetAlpha);
    }
 
    public void SetVignetteAlpha(float alpha)
    {
        Color c = vignetteImage.color;
        c.a = alpha;
        vignetteImage.color = c;
    }

    // ---------------------------------------------------------------
    // audio stuff
 
    public void PlaySuspenseAudio()
    {
        if (audioSource == null || suspenseClip == null) return;
 
        // Only play if not already playing so entering two radii at once doesn't restart it
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
 
    public void StopSuspenseAudio()
    {
        if (audioSource == null) return;
        audioSource.Stop();
    }
}