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
        
    }

    public void AddItemToArray(ItemPickup this_item)
    {
        
        bool is_two_sized = false;

        is_two_sized = this_item.itemData.numSlotsUsed == 2;

        
        if(is_two_sized)
        {
            foreach (InventorySlot this_slot in inventorySlotList)
            {
                if(this_slot.GetSlotIsOccupied())
                {
                    return;
                }
            }
            foreach (InventorySlot this_slot in inventorySlotList)
            {
                this_slot.SetSlotOccupied(this_item);
                PickupItem(this_item, twoHandedSlot);
                //this_item.SetObjectToFollow(twoHandedSlot.gameObject);
            }
        }
        else 
        foreach (InventorySlot this_slot in inventorySlotList)
        {
                
            if (this_slot.SetSlotOccupied(this_item) && this_item.itemData.numSlotsUsed == 1)
            {
                PickupItem(this_item, this_slot);
                
                //incoming_item.

                break;
            }

        }
        
    }

    void SwitchHands()
    {

    }

    
    void PickupItem(ItemPickup item_to_pickup, InventorySlot slot_to_attach_to)
    {
        item_to_pickup.transform.parent = slot_to_attach_to.transform;
        item_to_pickup.transform.forward = transform.forward;
        item_to_pickup.SetObjectToFollow(slot_to_attach_to.gameObject);
    }
}
