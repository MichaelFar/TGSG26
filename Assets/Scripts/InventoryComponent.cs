/*
Contributor(s): Michael farrar
Brief Description: Component that attaches to the player object, handles picking up, dropping, swapping, and using items
Date: 6/2/2026
*/


using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using GlobalDataTypes;
public class InventoryComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<InventorySlot> inventorySlotList;

    public InventorySlot twoHandedSlot;

    private InventorySlot activeSlot;
    private InventorySlot dropSlot;


    private Vector3 leftSlotPosition;
    private Vector3 rightSlotPosition;
    private int activeSlotIndex = 1;

    private enum e_Hands {LeftHand, RightHand};
    void Start()
    {
        activeSlot = inventorySlotList[activeSlotIndex];
        dropSlot = inventorySlotList[1 - activeSlotIndex];

        rightSlotPosition = activeSlot.transform.localPosition;
        leftSlotPosition = dropSlot.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Drop Item"))
        {
           
            DropItem();
        }
        if(Input.GetButtonUp("Switch Hands"))
        {
            SwitchHands();
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
            
            PickupItem(this_item, twoHandedSlot);
            activeSlot = twoHandedSlot;
        }
        else if (!twoHandedSlot.GetSlotIsOccupied())
        {
            if(!activeSlot.isOccupied)
            {
                PickupItem(this_item, activeSlot);
            }
            else if(!dropSlot.isOccupied)
            {
                PickupItem(this_item, dropSlot);
            }
            
        }
        
    }
    //Swaps the slot positions
    void SwitchHands()
    {
        if (twoHandedSlot.GetSlotIsOccupied() || CheckIfHandsEmpty())
        {
            return;
        }

        Vector3 stored_pos = Vector3.zero;

        stored_pos = inventorySlotList[0].transform.position;
        inventorySlotList[0].transform.position = inventorySlotList[1].transform.position;
        inventorySlotList[1].transform.position = stored_pos;

        ChangeActiveSlotIndex();
        
        dropSlot = inventorySlotList[1 - activeSlotIndex];
    }

    
    void PickupItem(ItemPickup item_to_pickup, InventorySlot slot_to_attach_to)
    {
       // item_to_pickup.transform.parent = slot_to_attach_to.transform;
        item_to_pickup.transform.forward = transform.forward;
        slot_to_attach_to.AddItemToSlot(item_to_pickup);
        item_to_pickup.SetObjectToFollow(slot_to_attach_to.gameObject);
        //item_to_pickup.isHeld = true;
    }

    void DropItem()
    {
        if(twoHandedSlot.GetSlotIsOccupied())
        {
            twoHandedSlot.RemoveItemFromSlot();
            foreach(InventorySlot i in inventorySlotList)
            {
                if(GetWhichHand(i) == e_Hands.RightHand)
                {
                    activeSlot = i;
                    return;
                }
            }
        }
        else if(CheckIfHandsEmpty())
        {
            SwitchHands();
        }
        else
        {
            if (!dropSlot.isOccupied)
            {
                activeSlot.RemoveItemFromSlot();
            }
            else
            {
                dropSlot.RemoveItemFromSlot();
            }

        }
        
    }
    private void ChangeActiveSlotIndex()
    {
        activeSlotIndex = 1 - activeSlotIndex;
        activeSlot = inventorySlotList[activeSlotIndex];
    }
    private bool CheckIfHandsEmpty()
    {
        foreach (InventorySlot i in inventorySlotList)
        {
            if(i.isOccupied)
            {
                return false;
            }
        }
        return true;
    }
    
    private e_Hands GetWhichHand(InventorySlot slot_to_check)
    {
        if(slot_to_check.transform.localPosition == rightSlotPosition)
        {
            return e_Hands.RightHand;
        }
        return e_Hands.LeftHand;
    }

    public InventorySlot GetActiveSlot()
    {
        return activeSlot;
    }
    public InventorySlot GetOffHandSlot()
    {
        return dropSlot;
    }
    public InventorySlot GetTwoHandedSlot()
    {
        return twoHandedSlot;
    }
    public e_ItemTypes GetItemTypeInSlot(InventorySlot this_slot)
    {
        return this_slot.GetHeldItem().itemData.thisItemType;
    }
}
