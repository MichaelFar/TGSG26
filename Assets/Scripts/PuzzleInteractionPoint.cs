/*
Contributor(s): Michael Farrar
Brief Description: This object handles puzzle logic for how the player can use items in their inventory to solve and trigger things
Date: 6/5/2026
*/
using UnityEngine;

using GlobalDataTypes;
using UnityEngine.Events;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
public class PuzzleInteractionPoint : MonoBehaviour, IInteractable
{

    public UnityEvent ev_SolvedPuzzle;

    //public List<InventoryItemData> RequiredItemList;

    public List<SolveObject> currentlyRequiredItemList;
    public List<SolveObject> radiantTaskList;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PopulateRequirements();
        foreach (SolveObject i in currentlyRequiredItemList)
        {
            i.ResetDataToDefault();
        }
        foreach (SolveObject i in radiantTaskList)
        {
            i.ResetDataToDefault();
        }
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
            CheckForThenRemoveItemFromList(inventory.GetActiveSlot());
            CheckForThenRemoveItemFromList(inventory.GetOffHandSlot());
            CheckForThenRemoveItemFromList(inventory.GetTwoHandedSlot());
            
        }
    }
    public void CheckForThenRemoveItemFromList(InventorySlot slot_to_check)
    {
        bool all_requirements_met = true;
        //Check for required to solve items
        foreach (SolveObject i in currentlyRequiredItemList)
        {
            if (slot_to_check.isOccupied)
            {
                if (i)
                {
                    i.SetSlotToAffect(slot_to_check);
                    i.CheckIfCanSolve(slot_to_check.GetHeldItem().itemData);
                    
                }
            }
            if (!i.requirementsMetToSolve)
            {
                all_requirements_met = false;
            }
        }
        //Solve puzzle if all requirements met
        if(all_requirements_met)
        {
            ev_SolvedPuzzle.Invoke();
        }
        foreach (SolveObject i in radiantTaskList)
        {
            if (slot_to_check.isOccupied)
            {
                if (i)
                {
                    i.SetSlotToAffect(slot_to_check);
                    i.CheckIfCanSolve(slot_to_check.GetHeldItem().itemData);
                        
                }
            }
        }
            
        
    }
    public void DebugPrintSuccess()
    {
        print("Solved puzzle");
    }
    
}
