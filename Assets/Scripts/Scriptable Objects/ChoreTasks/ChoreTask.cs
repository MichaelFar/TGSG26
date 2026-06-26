using UnityEngine;

[CreateAssetMenu(fileName = "ChoreTask", menuName = "Scriptable Objects/ChoreTask")]
public class ChoreTask : ScriptableObject
{
    
    public string baseDescription = "Tasks Solved";

    protected string _task_ratio = "";
    protected string taskRatio { get { return CalculateTaskRatio(); } }
    //Each day will have 1 or more chore tasks, these tasks are made of 1 or more solve objects
    public SolveObject[] requiredSolveObjectList;

    protected bool isComplete = false;

    protected bool isDemon = false;

    protected int numSolved = 0;
    protected string CalculateTaskRatio()
    {
        int solved_count = 0;
        foreach(SolveObject i in requiredSolveObjectList)
        {
           if(i.requirementsMetToSolve)
           {
                solved_count += 1;
           }

        }
        return (solved_count + " / " + requiredSolveObjectList.Length);
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
}
