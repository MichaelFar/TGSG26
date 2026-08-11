using UnityEngine;
using UnityEngine.InputSystem;

public class TestAnimatorScript : MonoBehaviour
{
    private Animator testAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if(testAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                testAnimator.SetTrigger("WalkToIdle");
                print("Currently playing axe walk");
            }
            else
            {
                testAnimator.SetTrigger("IdleToWalk");
            }
        }
    }
}
