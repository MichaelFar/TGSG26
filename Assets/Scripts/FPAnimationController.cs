using UnityEngine;

public class FPAnimationController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private InventoryComponent inventoryController;

    [SerializeField]
    private FPArmsAnimator[] armsList;

    private ItemPickup lastHeldItem;

    private PlayerMovement playerController;

    
    void Start()
    {
        playerController = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        bool is_moving = playerController.isMoving;
        if(is_moving)
        {
            AllIdleToWalk();
            
        }
        else
        {
            AllWalkToIdle();
            
        }
        
    }

    private void AllWalkToIdle()
    {
        
        foreach (FPArmsAnimator i in armsList)
        {
            i.PlayWalkToIdle();
        }
    }
    private void AllIdleToWalk()
    {
        foreach (FPArmsAnimator i in armsList)
        {
            i.PlayIdleToWalk();
        }
    }

    public void CheckArmsAndHide()
    {
        InventoryItemData data_to_check = null;
        if (inventoryController.GetTwoHandedSlot().GetHeldItem() != null)
        {
            data_to_check = inventoryController.GetTwoHandedSlot().GetHeldItem().itemData;
            lastHeldItem = inventoryController.GetTwoHandedSlot().GetHeldItem();
        }

        if(data_to_check == null)
        {
            lastHeldItem.GetComponent<ColliderRendererController>().EnableCollidersAndHideRenderers();
            foreach (FPArmsAnimator i in armsList)
            {
                 
                i.GetComponent<ColliderRendererController>().DisableCollidersAndHideRenderers();
                print("Hiding arms");
            }
        }
        else
        {
            lastHeldItem.GetComponent<ColliderRendererController>().DisableCollidersAndHideRenderers();
            foreach (FPArmsAnimator i in armsList)
            {
                if (i.GetItemData() == data_to_check)
                {
                    i.GetComponent<ColliderRendererController>().EnableCollidersAndHideRenderers();
                    print("Showing arms");
                }
            }
        }
            
    }
    
}
