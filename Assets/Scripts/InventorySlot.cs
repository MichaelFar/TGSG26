using UnityEngine;


public class InventorySlot : MonoBehaviour
{
    public bool isOccupied = false;
    public InventoryItemData itemData;


    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public bool SetSlotOccupied(ItemPickup incoming_item)
    {
        if(isOccupied)
        {
            return false;
        }
        else
        {
            isOccupied = true;
            itemData = incoming_item.itemData;
            return true;
        }
    }

    public void RemoveItemFromSlot()
    {
        isOccupied = false;
        itemData.ev_DroppedItem.Invoke();
    }
}
