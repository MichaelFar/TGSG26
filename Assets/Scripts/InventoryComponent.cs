using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
public class InventoryComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<InventorySlot> inventorySlotList;

    public InventorySlot twoHandedSlot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Drop Item"))
        {
           // print("Dropping item");
            DropItem();
        }
    }

    public void AddItemToArray(ItemPickup this_item)
    {
        
        bool is_two_sized = false;

        is_two_sized = this_item.itemData.numSlotsUsed == 2;


        if (is_two_sized)
        {
            foreach (InventorySlot this_slot in inventorySlotList)
            {
                if (this_slot.GetSlotIsOccupied())
                {
                    return;
                }
            }
            foreach (InventorySlot this_slot in inventorySlotList)
            {
                //this_slot.SetSlotOccupied(this_item);

                //this_item.SetObjectToFollow(twoHandedSlot.gameObject);
            }
            PickupItem(this_item, twoHandedSlot);
        }
        else if (!twoHandedSlot.GetSlotIsOccupied())
        {
            foreach (InventorySlot this_slot in inventorySlotList)
            {

                if (!this_slot.GetSlotIsOccupied())
                {
                    //this_slot.SetSlotOccupied(this_item);
                    PickupItem(this_item, this_slot);
                    print("Adding item to slot " + this_slot);
                    break;
                }

            }
        }
        
    }

    void SwitchHands()
    {

    }

    
    void PickupItem(ItemPickup item_to_pickup, InventorySlot slot_to_attach_to)
    {
       // item_to_pickup.transform.parent = slot_to_attach_to.transform;
        item_to_pickup.transform.forward = transform.forward;
        slot_to_attach_to.SetSlotOccupied(item_to_pickup);
        item_to_pickup.SetObjectToFollow(slot_to_attach_to.gameObject);
        item_to_pickup.isHeld = true;
    }

    void DropItem()
    {
        if(twoHandedSlot.GetSlotIsOccupied())
        {
            twoHandedSlot.RemoveItemFromSlot();
        }
        else
        {
            foreach (InventorySlot this_slot in inventorySlotList)
            {
                if(this_slot.GetSlotIsOccupied())
                {
                    this_slot.RemoveItemFromSlot();
                    print("Dropping item");
                    return;
                }
                
            }
        }    
    }
}
