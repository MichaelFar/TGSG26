/*
Contributor(s): Michael farrar
Brief Description: Component that attaches to the player object, handles picking up, dropping, swapping, and using items
Date: 6/2/2026
*/


using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Events;
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

    public UnityEvent ev_SwitchedHands;
    public UnityEvent ev_RightHandPickedUp;
    public UnityEvent ev_LeftHandPickedUp;
    public UnityEvent ev_TwoHandedItemPickedUp;
    public UnityEvent ev_DroppedItem;

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

    public void DetermineItemPickup(ItemPickup this_item)
    {
        
        bool is_two_sized = false;

        is_two_sized = this_item.itemData.numSlotsUsed == 2;

        print("Item size is " + this_item.itemData.numSlotsUsed);

        if (is_two_sized && activeSlot != twoHandedSlot)
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
            ev_TwoHandedItemPickedUp.Invoke();
        }
        else if (!twoHandedSlot.GetSlotIsOccupied())
        {
            if(!activeSlot.isOccupied)
            {
                PickupItem(this_item, activeSlot);
                ev_RightHandPickedUp.Invoke();
            }
            else if(!dropSlot.isOccupied)
            {
                PickupItem(this_item, dropSlot);
                ev_LeftHandPickedUp.Invoke();
            }
            
        }
        
    }
    //Swaps the slot positions, does not swap anything if the two handed slot is occupied or the hands are empty
    void SwitchHands()
    {
        if (twoHandedSlot.GetSlotIsOccupied() || CheckIfHandsEmpty())
        {
            return;
        }

        ev_SwitchedHands.Invoke();

        Vector3 stored_pos = Vector3.zero;

        stored_pos = inventorySlotList[0].transform.position;
        inventorySlotList[0].transform.position = inventorySlotList[1].transform.position;
        inventorySlotList[1].transform.position = stored_pos;

        ChangeActiveSlotIndex();
        
        dropSlot = inventorySlotList[1 - activeSlotIndex];
    }

    
    void PickupItem(ItemPickup item_to_pickup, InventorySlot slot_to_attach_to)
    {
       
        item_to_pickup.transform.forward = transform.forward;
        slot_to_attach_to.AddItemToSlot(item_to_pickup);
        item_to_pickup.SetObjectToFollow(slot_to_attach_to.gameObject);
        item_to_pickup.SetUseGravity(false);
        item_to_pickup.SetColliderEnabled(false);

    }

    void DropItem()
    {
        if(twoHandedSlot.GetSlotIsOccupied())
        {
            
            twoHandedSlot.DropItemFromSlot();
            foreach(InventorySlot i in inventorySlotList)
            {
                if(GetWhichHand(i) == e_Hands.RightHand)
                {
                    activeSlot = i;
                    ev_DroppedItem.Invoke();
                    return;
                }
            }
            
        }
        else
        {
            //Drops from the left hand if it has an item, but if the active hand is the only one that has one it drops one from there
            if (!dropSlot.isOccupied)
            {
                activeSlot.DropItemFromSlot();
                ev_DroppedItem.Invoke();
            }
            else
            {
                dropSlot.DropItemFromSlot();
                ev_DroppedItem.Invoke();
            }

        }
        
    }
    //This ensures that the right hand is always the active slot
    private void ChangeActiveSlotIndex()
    {
        activeSlotIndex = 1 - activeSlotIndex;
        activeSlot = inventorySlotList[activeSlotIndex];
    }
    //Returns true if the hands are empty, this does not check for the two handed slot
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
    //Determines if the given slot is the left or right hand
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
    

}
