using System.Collections;
using UnityEngine;
 
/// <summary>
/// Simple wandering AI: moves forward, pauses, picks a new direction, repeat.
/// Attach this script to any GameObject with a Rigidbody (or use transform-based movement).
/// </summary>
public class WonderAI : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("How fast the AI moves forward.")]
    public float moveSpeed = 3f;
 
    [Tooltip("How long (seconds) the AI moves before stopping.")]
    public float moveDuration = 1.5f;
 
    [Header("Turning")]
    [Tooltip("How long (seconds) the AI pauses before picking a new direction.")]
    public float pauseDuration = 1f;
 
    [Tooltip("Maximum angle (degrees) the AI can turn each wander cycle.")]
    public float maxTurnAngle = 90f;
 
    [Tooltip("How fast the AI rotates toward its new direction.")]
    public float turnSpeed = 5f;
 
    // ---------------------------------------------------------------
 
    private void Start()
    {
        StartCoroutine(WanderLoop());
    }
 
    private IEnumerator WanderLoop()
    {
        while (true)
        {
            // 1. Pick a random new direction
            float randomAngle = Random.Range(-maxTurnAngle, maxTurnAngle);
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, randomAngle, 0f);
 
            // 2. Smoothly rotate to face that direction
            float elapsed = 0f;
            while (elapsed < pauseDuration)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                elapsed += Time.deltaTime;
                yield return null;
            }
 
            // Snap to exact rotation at the end of the turn
            transform.rotation = targetRotation;
 
            // 3. Move forward for moveDuration seconds
            elapsed = 0f;
            while (elapsed < moveDuration)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
 
            // 4. Loop back — the AI will now pick a new direction
        }
    }
}