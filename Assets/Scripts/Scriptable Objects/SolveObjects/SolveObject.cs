/*
Contributor(s): Michael Farrar
Brief Description: This object handles the conditions and reactions for when a particular item is used to solve
Date: 6/5/2026
*/
using FullSerializer;
using System;
using UnityEditor;
using UnityEngine;

using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;
[CreateAssetMenu(fileName = "SolveObject", menuName = "Scriptable Objects/SolveObject")]

public class SolveObject : ScriptableObject
{
    public InventoryItemData requiredData;

    protected InventorySlot slotToAffect;

    public bool triggerOnce = true;

    

    private bool canBeTriggered = true;
    public UnityEvent ev_OnSolve;

    private bool _has_been_triggered = false;

    public bool triggerOncePerDay = false;

    [HideInInspector]
    public bool hasBeenTriggered = false;
    [HideInInspector]
    //This is used to determine if the requirements met to solve this object are true and will be checked by puzzle interaction point
    public bool requirementsMetToSolve = false;

    protected bool originalTriggerOnce = false;

    public bool activateOnEmptyHand = false;
    public virtual bool OnSolve()
    {
        
        if (canBeTriggered)
        {
            hasBeenTriggered = true;
            requirementsMetToSolve = true;
            UnityEngine.MonoBehaviour.print("Solve object requirement met");
            canBeTriggered = !triggerOnce;
            if(triggerOncePerDay)
            {
                canBeTriggered = false;
            }
            ev_OnSolve.Invoke();
            return true;
        }
        MonoBehaviour.print("Cannot be solved");
        return false;
        
    }

    public void CheckIfCanSolve(InventoryItemData data_to_check)
    {
        if(requiredData == data_to_check)
        {
            OnSolve();
            
        }
    }
    
    public void SetSlotToAffect(InventorySlot affected_slot)
    {
        slotToAffect = affected_slot;
    }

    public virtual void ResetDataToDefault()
    {
        slotToAffect = null;
        canBeTriggered = true;
        hasBeenTriggered = false;
        requirementsMetToSolve = false;
        if(triggerOncePerDay)
        {
            TimeManager.Instance.ev_dayHasChanged.AddListener(SetCanBeTriggeredToTrue);
            //canBeTriggered = true;
        }
        else
        {
            TimeManager.Instance.ev_dayHasChanged.RemoveListener(SetCanBeTriggeredToTrue);
            
        }
        //return Action;
    }
    private void SetCanBeTriggeredToTrue()
    {
        canBeTriggered = true;
        MonoBehaviour.print("Resetting can be triggered from listener call");
    }
}
