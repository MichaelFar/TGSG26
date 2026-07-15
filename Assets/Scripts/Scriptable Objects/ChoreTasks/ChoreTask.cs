using FullSerializer;
using UnityEngine;
using UnityEngine.Events;
/*
Contributor(s): Michael Farrar
Brief Description: This class contains the description the player will read for the task as well as basic data like if its a demon task 
                   This object is used by the ChoreManager singleton and most of the functionality is there
Date: 6/27/2026
*/

[CreateAssetMenu(fileName = "ChoreTask", menuName = "Scriptable Objects/ChoreTask")]
public class ChoreTask : ScriptableObject
{
    
    public string baseDescription = "Tasks Solved";

    protected string _task_ratio = "";
    protected string taskRatio { get { return CalculateTaskRatio(); } }
    //Each day will have 1 or more chore tasks, these tasks are made of 1 or more solve objects
    public SolveObject[] requiredSolveObjectList;

    protected bool isComplete = false;

    public bool isDemon = false;

    protected int numSolved = 0;

    public UnityEvent ev_ChoreCompleted;

    public UnityEvent ev_ChoreStepCompleted;

    public UnityEvent ev_ChoreFailed;
    protected string CalculateTaskRatio()
    {
        
        return (numSolved + " / " + requiredSolveObjectList.Length);
    }

    public void IncrementNumSolved()
    {
        MonoBehaviour.print("Incrementing task " + this.name);
        numSolved = Mathf.Clamp(numSolved + 1, 0, requiredSolveObjectList.Length);
        if(numSolved >= requiredSolveObjectList.Length)
        {
            CompleteChore();
        }
        MonoBehaviour.print(GetDescription());
        ev_ChoreStepCompleted.Invoke();
    }
    public string GetDescription()
    {
        return baseDescription + ": " + taskRatio;
    }

    public void ResetData()
    {
        numSolved = 0;
        isComplete = false;
    }

    public void CompleteChore()
    {

        ev_ChoreCompleted.Invoke();
        isComplete = true;
    }

    public void FailChore()
    {
        ev_ChoreFailed.Invoke();
    }
    public bool GetIsComplete()
    {
        return isComplete;
    }
    public bool GetIsDemon()
    {
        return isDemon;
    }


}
