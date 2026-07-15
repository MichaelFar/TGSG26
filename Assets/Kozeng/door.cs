using UnityEngine;

public class door : MonoBehaviour
{
    bool toggle;
    public Animator anim;

    public void openClose()
    {
        toggle = !toggle;
        if (toggle == false)
        {
            anim.ResetTrigger("open door");
            anim.SetTrigger("close door");
        }
        if (toggle == true)
        {
            anim.ResetTrigger("close door");
            anim.SetTrigger("open door");
        }
    }
}

