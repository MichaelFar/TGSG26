using UnityEngine;
using UnityEngine.InputSystem;

public class WalkAndIdleAnimator : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [SerializeField]
    private InventoryItemData itemData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created*
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayIdleToWalk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalkToIdle()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            animator.SetTrigger("WalkToIdle");
        
    }

    public void PlayIdleToWalk()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            animator.SetTrigger("IdleToWalk");
    }

    public string GetActiveAnimation()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return "Idle";
        }
        else
        {
            return "Walk";
        }
    }
    public InventoryItemData GetItemData()
    {
        return itemData;
    }
}
