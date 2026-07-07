using Unity.VisualScripting;
using UnityEngine;

public class GateInteraction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    public string doorOpenAniName, doorCloseAniName;

    bool toggle;
    public Animator anim;


/*    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.gameObject.tag == "door")
            {
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();
                intText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAniName))
                    {
                        doorAnim.ResetTrigger("open");
                        doorAnim.ResetTrigger("close");
                    }
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAniName))
                    {
                        doorAnim.ResetTrigger("open");
                        doorAnim.ResetTrigger("close");
                    }
                }
            }
        }
        else
        {
            intText.SetActive(false);
        }
    }*/
}
