using System.Collections;
using UnityEngine;
using UnityEngine.AI;
 
/// <summary>
/// Wandering AI using Unity's NavMesh — respects walls and stays inside a defined zone.
///
/// SETUP STEPS:
///  1. Bake a NavMesh in your scene:  Window > AI > Navigation > Bake
///  2. Add a NavMeshAgent component to this GameObject.
///  3. Attach this script to the same GameObject.
///  4. Set 'homePoint' to a Transform marking the centre of the wander zone,
///     OR leave it empty to use this object's starting position.
///  5. Adjust 'wanderRadius' to define how far the AI can roam from that centre.
///
/// The AI will never walk through walls and will always stay within the zone.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class WonderAI2 : MonoBehaviour
{
    [Header("Wander Zone")]
    [Tooltip("Centre of the allowed wander area. Leave empty to use this object's start position.")]
    public Transform homePoint;
 
    [Tooltip("How far from the home point the AI is allowed to wander.")]
    public float wanderRadius = 10f;
 
    [Header("Timing")]
    [Tooltip("How long the AI waits after reaching a destination before picking the next one.")]
    public float pauseDuration = 1.5f;
 
    [Tooltip("Extra random seconds added to the pause so movement feels less robotic.")]
    public float pauseVariance = 0.5f;
 
    // ---------------------------------------------------------------
 
    private NavMeshAgent agent;
    private Vector3 homePosition;
 
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
 
    private void Start()
    {
        // Lock in the home position once at startup
        homePosition = homePoint != null ? homePoint.position : transform.position;
        StartCoroutine(WanderLoop());
    }
 
    private IEnumerator WanderLoop()
    {
        while (true)
        {
            // Pick a valid NavMesh point inside the wander zone
            Vector3 destination;
            if (TryGetRandomNavMeshPoint(out destination))
            {
                agent.SetDestination(destination);
 
                // Wait until the agent gets close enough to the destination
                yield return new WaitUntil(() =>
                    !agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance);
            }
 
            // Pause before the next move (with a little random variance)
            float waitTime = pauseDuration + Random.Range(-pauseVariance, pauseVariance);
            yield return new WaitForSeconds(Mathf.Max(0f, waitTime));
        }
    }
 
    /// <summary>
    /// Tries to find a random point on the NavMesh within wanderRadius of homePosition.
    /// Retries a few times in case the random point lands off the mesh.
    /// </summary>
    private bool TryGetRandomNavMeshPoint(out Vector3 result)
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            // Random point inside a circle on the XZ plane
            Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
            Vector3 candidate = homePosition + new Vector3(randomCircle.x, 0f, randomCircle.y);
 
            NavMeshHit hit;
            // Sample the nearest NavMesh position within a small search radius
            if (NavMesh.SamplePosition(candidate, out hit, 2f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
 
        result = transform.position; // fallback: stay put
        return false;
    }
 
    // ---------------------------------------------------------------
    // Draw the wander zone as a yellow wire sphere in the Scene view
    // so you can see and adjust the radius without running the game.
    private void OnDrawGizmosSelected()
    {
        Vector3 centre = (homePoint != null) ? homePoint.position
                       : (Application.isPlaying ? homePosition : transform.position);
 
        Gizmos.color = new Color(1f, 0.9f, 0f, 0.4f);
        Gizmos.DrawSphere(centre, wanderRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centre, wanderRadius);
    }
}