using UnityEngine;

public class AIAnimator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ChaseAI ai;

    public WalkAndIdleAnimator walkAndIdleAnimator;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool is_moving = ai.GetIsMoving();
        if (is_moving)
        {
            print("Ai is moving");
            walkAndIdleAnimator.PlayIdleToWalk();
            print("Animation state is " + walkAndIdleAnimator.GetActiveAnimation());
        }
        else
        {
            walkAndIdleAnimator.PlayWalkToIdle();
            print("Animation state is " + walkAndIdleAnimator.GetActiveAnimation());
        }
    }
}
