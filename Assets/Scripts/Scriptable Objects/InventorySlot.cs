using UnityEngine;

[CreateAssetMenu(fileName = "InventorySlot", menuName = "Scriptable Objects/InventorySlot")]
public class InventorySlot : ScriptableObject
{
    public bool isOccupied = false;
    public InventoryItemData itemData;
}
