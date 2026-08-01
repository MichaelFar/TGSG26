using UnityEngine;

/// <summary>
/// Jonathan Aguilar | 7/31/26 |
/// ScaredSense — attach this directly to the Player GameObject.
/// It scans for nearby WanderAI objects and logs when the player
/// enters any of their wander radius.
///
/// SETUP:
///  1. Attach this script to your Player GameObject.
/// </summary>

// I apalogize for all the notes, its all to help not only u the reader but for me in the future since I'm forgetful

public class ScaredSense : MonoBehaviour
{
    // Tracks which WanderAI objects the player is already inside of
    // so we only log on enter/exit rather than every frame
    private System.Collections.Generic.HashSet<WanderAI2> currentlyInRange
        = new System.Collections.Generic.HashSet<WanderAI2>();
        // as long as we use the same wandering AI2 script then this should work or if we make a specail AI that also needs 
        // to work with this then we can make some tweaks

    private void Update()
    {   // an array or basically list of wander ai objs | allWanderers is just the name I gave it 
        // the FindObjectsByType<WanderAI2> | is only going to grab every wanderAI2 component in the entire scene then puts them all in that list
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
            }
            else if (!isInRange && currentlyInRange.Contains(wanderer))
            {
                // player left range of wanderer
                currentlyInRange.Remove(wanderer);
                Debug.Log($"[ScaredSense] The feeling fades near | leaving range {wanderer.gameObject.name}.");
            }
        }
    }
}