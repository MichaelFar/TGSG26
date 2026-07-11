/*
Contributor(s): Michael Farrar
Brief Description: ChoreManager is a singleton that handles the connection between SolveObjects that are on PuzzleInteractionPoints
                   How to use: Add a chore day to the list in the inspector, chore days have a list for chore tasks that can both be created from scriptable object menu
Date: 6/27/2026
*/
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System;

public class ChoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static ChoreManager Instance { get { return _instance; } }
    private static ChoreManager _instance;

    public ChoreDay[] choreWeekList;
    public int currentChoreDayIndex = 0;

    public TaskListUI listUI;

    public UnityEvent ev_ChoreUpdated;

    public UnityEvent ev_NewDayDataInitialized;

    Dictionary<string, ChorePackageStruct> solveObjectEventDict = new Dictionary<string, ChorePackageStruct>();
    
    private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        PopulateEventDict();

        foreach (ChoreTask i in GetAllCurrentChores())
        {
            i.ResetData();
        }
    }
    void Start()
    {
        TimeManager.Instance.ev_dayHasChanged.AddListener(InitializeNextDay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<ChoreTask> GetAllCurrentChores()
    {
        List<ChoreTask> list_to_return = new List<ChoreTask>();

        ChoreDay current_day = choreWeekList[currentChoreDayIndex];
        print("Current day is " + current_day.name);
        foreach(ChoreTask task in current_day.choreList)
        {
            list_to_return.Add(task);
        }
        
        return list_to_return;
    }

    public List<SolveObject> GetAllCurrentChoreSolveObjects()
    {
        List<SolveObject> list_to_return = new List<SolveObject>();

        List<ChoreTask> current_chores = GetAllCurrentChores();

        foreach (ChoreTask i in current_chores)
        {
            //print("GetAllCurrentChoreSolveObjects()");
            foreach (SolveObject j in i.requiredSolveObjectList)
            {
                list_to_return.Add(j);
            }
        }


        return list_to_return;
    }
    public void PopulateEventDict()
    {
        List<SolveObject> current_solve_object_list = GetAllCurrentChoreSolveObjects();

        List<ChoreTask> current_chores = GetAllCurrentChores();

        solveObjectEventDict.Clear();

        foreach (ChoreTask task in current_chores)
        {
            task.ev_ChoreStepCompleted.AddListener(InvokeChoreUpdated);
            print("Connecting task " + task.name + " to Invoke Chore Updated on day" + choreWeekList[currentChoreDayIndex]);
            foreach (SolveObject so in task.requiredSolveObjectList)
            {
                if (!solveObjectEventDict.ContainsKey(so.name))
                {
                    solveObjectEventDict.Add(so.name, new ChorePackageStruct(task));
                    
                    solveObjectEventDict[so.name].ev_ThisEvent.AddListener(task.IncrementNumSolved);
                    print("Adding key " + so.name + " from " + choreWeekList[currentChoreDayIndex].name);
                    
                }
            }
            
            

        }
        foreach (string i in solveObjectEventDict.Keys)
        {
            print("Dict has " + i + " after populating");
        }
        //print("Dictionary after adding keys is " + solveObjectEventDict.Keys);


    }

    public void ConnectSolveObjectToEventDict(SolveObject object_to_connect)
    {
        string object_name = object_to_connect.name.Replace("(Clone)", "");
        print("Modified object name is " + object_name);
        if(solveObjectEventDict.ContainsKey(object_name))
        {
            object_to_connect.ev_OnSolve.RemoveAllListeners();//Listener(solveObjectEventDict[object_name].ev_ThisEvent.Invoke);
            object_to_connect.ev_OnSolve.AddListener(solveObjectEventDict[object_name].ev_ThisEvent.Invoke);
            
        }
        
    }

    public void InitializeNextDay()
    {
        foreach (ChoreTask i in GetAllCurrentChores())
        {
            //Here would also be code to invoke the fail event
            i.ResetData();
        }
        print("Initializing new day");
        SetDayIndex(TimeManager.Instance.GetDay() - 1);
        print("Current chore day index is now " + currentChoreDayIndex);
        
        PopulateEventDict();
        foreach (ChoreTask i in GetAllCurrentChores())
        {
            
            i.ResetData();
        }
        listUI.PopulateTextLabelList();
        listUI.UpdateTextLabel();
        ev_NewDayDataInitialized.Invoke();
    }

    public void SetDayIndex(int new_index)
    {
        currentChoreDayIndex = Mathf.Clamp(new_index, 0, choreWeekList.Length - 1);

    }
    
    struct ChorePackageStruct
    {
        public ChorePackageStruct(ChoreTask new_task)
        {
            chore = new_task;
            ev_ThisEvent = new UnityEvent();
        }
        public ChoreTask chore;
        public UnityEvent ev_ThisEvent;
    }

    public List<string> GetAllChoreDescriptions()
    {
        List<string> list_to_return = new List<string>();

        foreach(ChoreTask i in GetAllCurrentChores())
        {
            list_to_return.Add(i.GetDescription());
        }
        return list_to_return;

    }

    public void InvokeChoreUpdated()
    {
        ev_ChoreUpdated.Invoke();
    }
}
