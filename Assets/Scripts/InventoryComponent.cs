using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
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

    void AddItemToArray(ItemPickup this_item)
    {
        
        bool is_two_sized = false;

        is_two_sized = this_item.itemData.numSlotsUsed == 2;

        
        if(is_two_sized)
        {
            foreach (InventorySlot this_slot in inventorySlotList)
            {
                if(!this_slot.SetSlotOccupied(this_item))
                {

                }
            }
        }
        foreach (InventorySlot this_slot in inventorySlotList)
        {
                
            if (this_slot.SetSlotOccupied(this_item) && this_item.itemData.numSlotsUsed == 1)
            {
                PickupItem(this_item);
                this_item.SetObjectToFollow(this_slot.gameObject);
                //incoming_item.

                break;
            }

        }
        
    }

    void SwitchHands()
    {

    }

    void InitializeSlots()
    {

    }
    void PickupItem(ItemPickup item_to_pickup)
    {
        item_to_pickup.transform.parent = transform;
    }
}
