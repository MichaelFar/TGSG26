using UnityEngine;

using GlobalDataTypes;
using UnityEngine.Events;
using NUnit.Framework;
using System.Collections.Generic;
public class PuzzleInteractionPoint : MonoBehaviour, IInteractable
{

    UnityEvent ev_SolvedPuzzle;

    public List<InventoryItemData> RequiredItemList;

    private List<InventoryItemData> UsedItems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnInteract(GameObject object_interacting = null)
    {
        InventoryComponent inventory = object_interacting.GetComponent<InventoryComponent>();
        
        if(inventory)
        {
            foreach (InventoryItemData i in RequiredItemList)
            {
                if(i == inventory.GetActiveSlot().itemData)
                {

                }
            }
        }
    }
    //public bool 
}
