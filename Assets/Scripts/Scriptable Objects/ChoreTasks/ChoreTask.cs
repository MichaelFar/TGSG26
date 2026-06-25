using UnityEngine;

[CreateAssetMenu(fileName = "ChoreTask", menuName = "Scriptable Objects/ChoreTask")]
public class ChoreTask : ScriptableObject
{
    public string description = "You must do something";
    //Each day will have 1 or more chore tasks, these tasks are made of 1 or more solve objects
    public SolveObject[] requiredSolveObjectList;

    protected bool isComplete = false;
    
    //public void 


}
