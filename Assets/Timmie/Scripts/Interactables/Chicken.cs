using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Chicken : MonoBehaviour, IInteractable
{
    public UnityEvent itemPickUp, itemDrop;
    private NavMeshAgent chickenNavMeshAgent;
    private WanderAI2 chickenAI;
    private Rigidbody chickenRigidBody;
    private bool wasHeld, waitingToLand;
    private Collider chickenLeg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        chickenNavMeshAgent = GetComponent<NavMeshAgent>();
        chickenAI = GetComponent<WanderAI2>();
        chickenRigidBody = GetComponent<Rigidbody>();
        chickenRigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        chickenLeg = GetComponentInChildren<CapsuleCollider>();
    }

    public void OnInteract(GameObject object_Interacting = null)
    {
        itemPickUp.Invoke();
        StartEvent();
    }

    public bool CanInteract()
    {
        return true;
    }

    public void DisableAIComponents()
    {
        chickenNavMeshAgent.enabled = false;
        chickenAI.enabled = false;
        chickenLeg.isTrigger = true;
    }

    public void ResetChickenAI()
    {
        chickenAI.enabled = true;
        chickenLeg.isTrigger = false;
    }

    private void StartEvent()
    {
        //this counts how many chickens have already been collected prior to actually picking up the chicken
        print("StartEvent number of chickens collected prior to picking up chicken: " + ChickenCounter.Instance.GetChickensCollected());
        switch (ChickenCounter.Instance.GetChickensCollected())
        {
            case 1:
                print("Spawning chasing demon");
                PlayerGlobal.Instance.GetComponent<DemonSpawner>().SpawnDemon();
                break;
            case 2:
                for (int i = 0; i <= 5; i++)
                {
                    print("spawning eyeballs");
                    PlayerGlobal.Instance.GetComponent<DemonSpawner>().SpawnStalker();
                }
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print("HIT THE GROUND SON:" + collision.gameObject.name);
        if (waitingToLand)
        {
            chickenNavMeshAgent.enabled = true;
            waitingToLand = false;
            chickenRigidBody.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //is held is set to the opposite of if it's using gravity, yes gravity = not held, no gravity = yes held
        bool isHeld = !chickenRigidBody.useGravity;
        if (wasHeld && !isHeld)
        {
            chickenRigidBody.isKinematic = false;
            itemDrop.Invoke();
            waitingToLand = true;
        }
        wasHeld = isHeld;
    }
}
