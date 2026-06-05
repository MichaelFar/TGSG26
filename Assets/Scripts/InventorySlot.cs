/*
Contributor(s): Michael Farrar
Brief Description: Code for inventory slots, used by the inventory component
Date:
*/

using UnityEngine;


public class InventorySlot : MonoBehaviour
{
    public bool isOccupied = false;
    public InventoryItemData itemData;

    private ItemPickup itemHeld;
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public bool AddItemToSlot(ItemPickup incoming_item)
    {
        
        
        isOccupied = true;
        itemData = incoming_item.itemData;
        itemHeld = incoming_item;
        return true;
        
    }

    public void RemoveItemFromSlot()
    {
        //itemData.ev_DroppedItem.Invoke();
        itemHeld.DropItemBehavior();
        isOccupied = false;
        //itemHeld.DropItemBehavior();
        
        itemHeld = null;
        
        
        itemData = null;
    }

    public void DeleteItemFromSlot()
    {
        isOccupied = false;
        //itemHeld.DropItemBehavior();

        Destroy(itemHeld.gameObject);

        itemHeld = null;


        itemData = null;

    }
    public bool GetSlotIsOccupied()
    {
        return isOccupied;
    }
    public ItemPickup GetHeldItem()
    {
        return itemHeld;
    }
}
