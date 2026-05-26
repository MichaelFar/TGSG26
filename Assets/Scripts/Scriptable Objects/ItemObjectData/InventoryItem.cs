using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    UnityEvent ev_UsedItem;

    public int numSlotsUsed = 1;

    public void UseItem()
    {
        ev_UsedItem.Invoke();
    }
}
