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

    private bool requiredListEmitted = false;
    private bool radiantListEmitted = false;

    //public bool resetRequiredDaily = false;

    //public bool resetRadiantDaily = true;
    //public List<InventoryItemData> RequiredItemList;

    public List<SolveObject> currentlyRequiredItemList;
    public List<SolveObject> radiantTaskList;
    
    private List<SolveObject> nonPersistentCurrentlyRequiredItemList = new List<SolveObject>();
    private List<SolveObject> nonPersistentRadiantTaskList = new List<SolveObject>();

    private UnityEvent[] uniqueSOEventArray;

    public List<SolveObject> parallelSOList;// = new List<SolveObject>();
    private List<SolveObject> nonPersistentParallelSOList = new List<SolveObject>();

    public UnityEvent ev_AllListsInitialized;
    //public string mainLevelName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        uniqueSOEventArray = HelperFunctions.InitializeArray<UnityEvent>(parallelSOList.Count);
        
    }
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
        foreach (SolveObject i in parallelSOList)
        {
            i.ResetDataToDefault();
        }
        foreach (SolveObject i in parallelSOList)
        {
            nonPersistentParallelSOList.Add(Instantiate(i));
        }
        foreach (SolveObject i in currentlyRequiredItemList)
        {
            nonPersistentCurrentlyRequiredItemList.Add(Instantiate(i));
        }
        foreach (SolveObject i in radiantTaskList)
        {

            nonPersistentRadiantTaskList.Add(Instantiate(i));

        }
        ChoreManager.Instance.ev_NewDayDataInitialized.AddListener(ConnectSolveObjectsToChoreCalls);
        ConnectSolveObjectsToChoreCalls();
        TimeManager.Instance.ev_dayHasChanged.AddListener(DailyReset);
        //ChoreManager.Instance.PopulateEventDict();

        ev_SolvedPuzzle.AddListener(DebugRequiredSuccess);
        ev_CompletedAllRadiantTasks.AddListener(DebugRadiantSuccess);
        /*
        foreach(ChoreTask i in ChoreManager.Instance.GetAllCurrentChores())
        {
            i.ev_ChoreStepCompleted.AddListener(SomeFuncYouWrote);
        }
        */
        //SceneManager.LoadScene(0);
        ev_AllListsInitialized.Invoke();
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
            if (slot_to_check.isOccupied || i.activateOnEmptyHand)
            {
                if (i)
                {
                    if (!i.CheckCanBeTriggered())
                    {
                        all_requirements_met = false;
                        break;
                    }
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
            requiredListEmitted = true;
        }
        //Check for the radiant tasks requirements
        

        foreach (SolveObject i in nonPersistentRadiantTaskList)
        {
            if (slot_to_check.isOccupied || i.activateOnEmptyHand)
            {
                if (i)
                {
                    if (!i.CheckCanBeTriggered())
                    {
                        all_radiant_tasks_met = false;
                        break;
                    }
                    i.SetSlotToAffect(slot_to_check);
                    i.CheckIfCanSolve(slot_to_check.GetHeldItem().itemData);
                }
            }
            if (!i.requirementsMetToSolve)
            {
                all_radiant_tasks_met = false;
            }
            
        }
        foreach (SolveObject i in nonPersistentParallelSOList)
        {
            if (slot_to_check.isOccupied || i.activateOnEmptyHand)
            {
                if (i)
                {
                    if(!i.CheckCanBeTriggered())
                    {
                        break;
                    }
                    i.SetSlotToAffect(slot_to_check);
                    i.CheckIfCanSolve(slot_to_check.GetHeldItem().itemData);
                    uniqueSOEventArray[GetIndexOfParallelEvent(i)].Invoke();
                    print("Parallel event is firing");
                }
                /*
                if(i.requirementsMetToSolve && i.CheckCanBeTriggered())
                {
                    print("Parallel event is firing");
                    uniqueSOEventArray[GetIndexOfParallelEvent(i)].Invoke();
                    //i.SetCanBeTriggered(true);
                }
                */
            }
            
        }

        if (all_radiant_tasks_met && !radiantListEmitted)
        {
            ev_CompletedAllRadiantTasks.Invoke();
            radiantListEmitted = true;
        }
            
        
    }
    public void DebugPrintSuccess()
    {
        print("Solved puzzle");
    }

    public void DebugRadiantSuccess()
    {
        print("Completed all radiant tasks");
    }

    public void DebugRequiredSuccess()
    {
        print("Completed all required tasks");
    }

    public void ConnectSolveObjectsToChoreCalls()
    {
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
        foreach (SolveObject i in nonPersistentParallelSOList)
        {
            print(i.name);
            ChoreManager.Instance.ConnectSolveObjectToEventDict(i);
            i.ResetDataToDefault();
        }
    }

    //Handled via solve objects instead to be more in line with programatic design
    public void DailyReset()
    {
        //requiredListEmitted = !resetRequiredDaily;
        //radiantListEmitted = !resetRadiantDaily;
    }

    public void ConnectToSOParallelEvent(SolveObject object_to_check, UnityAction action_to_connect)
    {
        int index_of_so = GetIndexOfParallelEvent(object_to_check);
        if (index_of_so >= 0)
        {
            print("Checking if SO object is in list");
            
            print("Index of SO object in list" + index_of_so);
            
            uniqueSOEventArray[index_of_so].AddListener(action_to_connect);
            
                
            //taskLabelList.FindIndex(p => p.text == i.text);
        }
    }
    private int GetIndexOfParallelEvent(SolveObject object_to_check)
    {
        print("Solve object to connect name is " + object_to_check.name);
        string object_name = object_to_check.name.Replace("(Clone)", "");
        int index_of_so = parallelSOList.FindIndex(p => p.name == object_name);
        return index_of_so;
    }

}
