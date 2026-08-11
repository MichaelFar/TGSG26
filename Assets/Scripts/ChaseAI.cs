using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ChaseAI : MonoBehaviour
{

    private NavMeshAgent agent;

    private GameObject playerGameObject;

    [SerializeField]
    private float stopDistance = 4.0f;

    private float reachedPlayerThreshold = 0.0f;

    private bool hasReachedPlayer = false;

    public UnityEvent ev_ReachedPlayer;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        reachedPlayerThreshold = stopDistance += 0.5f;
    }

    private void Start()
    {
        playerGameObject = PlayerGlobal.Instance.playerRootObject;
        agent.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        ChasePlayer();
        if(Vector3.Distance(gameObject.transform.position, playerGameObject.transform.position) <= reachedPlayerThreshold)
        {
            if (!hasReachedPlayer)
            {
                hasReachedPlayer = true;
                ev_ReachedPlayer.Invoke();
                
            }
            
        }
        else
        {
            hasReachedPlayer = false;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerGameObject.transform.position);
    }

    
}