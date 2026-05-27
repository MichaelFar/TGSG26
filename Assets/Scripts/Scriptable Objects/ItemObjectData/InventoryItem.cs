using UnityEngine;
using UnityEngine.Events;
using GlobalDataTypes;
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItemData : ScriptableObject
{

    public e_ItemTypes thisItemType = e_ItemTypes.DebugType;

    public int numSlotsUsed = 1;

    public UnityEvent ev_DroppedItem;//Invoked by inventory slot class
    
}
