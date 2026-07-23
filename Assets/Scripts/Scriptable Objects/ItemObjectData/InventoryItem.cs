/*
Contributor(s): Michael Farrar
Brief Description: Item data that goes onto item pickups, used for various item pickups
Date: 6/2/2026
*/

using UnityEngine;
using UnityEngine.Events;
using GlobalDataTypes;
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItemData : ScriptableObject
{

    //public e_ItemTypes thisItemType = e_ItemTypes.DebugType;

    public int numSlotsUsed = 1;

    public UnityEvent ev_DroppedItem;

    public string itemName = "";
}
