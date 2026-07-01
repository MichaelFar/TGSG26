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
using UnityEngine.SceneManagement;
public class PuzzleInteractionPoint : MonoBehaviour, IInteractable
{

    public UnityEvent ev_SolvedPuzzle;
    public UnityEvent ev_CompletedAllRadiantTasks;
    //public List<InventoryItemData> RequiredItemList;

    public List<SolveObject> currentlyRequiredItemList;
    public List<SolveObject> radiantTaskList;
    
    private List<SolveObject> nonPersistentCurrentlyRequiredItemList = new List<SolveObject>();
    private List<SolveObject> nonPersistentRadiantTaskList = new List<SolveObject>();

    
    //public string mainLevelName;
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
        foreach (SolveObject i in currentlyRequiredItemList)
        {
            nonPersistentCurrentlyRequiredItemList.Add(Instantiate(i));
        }
        foreach (SolveObject i in radiantTaskList)
        {

            nonPersistentRadiantTaskList.Add(Instantiate(i));
            
        }
        
        //ChoreManager.Instance.PopulateEventDict();
        foreach (SolveObject i in nonPersistentCurrentlyRequiredItemList)
        {
            print(i.name);
            ChoreManager.Instance.ConnectSolveObjectToEventDict(i);
            i.ResetDataToDefault();
        }
        foreach (SolveObject i in nonPersistentRadiantTaskList)
        {
            print(i.name);
            ChoreManager.Instance.ConnectSolveObjectToEventDict(i);
            i.ResetDataToDefault();
        }
        
        /*
        foreach(ChoreTask i in ChoreManager.Instance.GetAllCurrentChores())
        {
            i.ev_ChoreStepCompleted.AddListener(SomeFuncYouWrote);
        }
        */
        //SceneManager.LoadScene(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Runs when player inputs interact key and the raycast hits an interactable
    //This one checks to see if the sending object had an inventory component
    public void OnInteract(GameObject object_interacting = null)
    {
        InventoryComponent inventory = object_interacting.GetComponent<InventoryComponent>();
        
        if(inventory)
        {
            CheckForRequiredItemsThenSolve(inventory.GetActiveSlot());
            CheckForRequiredItemsThenSolve(inventory.GetOffHandSlot());
            CheckForRequiredItemsThenSolve(inventory.GetTwoHandedSlot());
            
        }
    }
    /// <summary>
    /// Given a slot, checks to see if the required item is within that slot, also performs behavior based on the SolveObject that uses it
    /// This also checks for "radiant" solve objects which could represent mundane tasks that do not necessarily result in a solved puzzle
    /// </summary>
    /// <param name="slot_to_check"></param>
    public void CheckForRequiredItemsThenSolve(InventorySlot slot_to_check)
    {
        bool all_requirements_met = true;
        bool all_radiant_tasks_met = true;
        //Check for required to solve items
        foreach (SolveObject i in nonPersistentCurrentlyRequiredItemList)
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
        //Check for the radiant tasks requirements
        foreach (SolveObject i in nonPersistentRadiantTaskList)
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
                all_radiant_tasks_met = false;
            }
        }
        if(all_radiant_tasks_met)
        {
            ev_CompletedAllRadiantTasks.Invoke();
        }
            
        
    }
    public void DebugPrintSuccess()
    {
        print("Solved puzzle");
    }
    
}
