using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseAI : MonoBehaviour
{

    private NavMeshAgent agent;

    private GameObject playerGameObject;

    [SerializeField]
    private float stopDistance = 4.0f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        playerGameObject = PlayerGlobal.Instance.playerRootObject;
        agent.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerGameObject.transform.position);
    }

}