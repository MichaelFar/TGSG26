/*
Contributor(s): Michael Farrar
Brief Description: ChoreManager is a singleton that handles the connection between SolveObjects that are on PuzzleInteractionPoints
                   How to use: Add a chore
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
    }
    void Start()
    {
        PopulateEventDict();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<ChoreTask> GetAllCurrentChores()
    {
        List<ChoreTask> list_to_return = new List<ChoreTask>();

        ChoreDay current_day = choreWeekList[currentChoreDayIndex];
        foreach(ChoreTask task in current_day.choreList)
        {
            list_to_return.Add(task);
           // print("Adding task to task list");
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

        foreach (ChoreTask task in current_chores)
        {
            foreach (SolveObject so in task.requiredSolveObjectList)
            {
                if (!solveObjectEventDict.ContainsKey(so.name))
                {
                    solveObjectEventDict.Add(so.name, new ChorePackageStruct(task));
                    solveObjectEventDict[so.name].ev_ThisEvent.AddListener(task.IncrementNumSolved);
                    //print("Adding key " + i.name);
                }
            }
        }

    }

    public void ConnectSolveObjectToEventDict(SolveObject object_to_connect)
    {
        string object_name = object_to_connect.name.Replace("(Clone)", "");
        print("Modified object name is " + object_name);
        if(solveObjectEventDict.ContainsKey(object_name))
        {
            object_to_connect.ev_OnSolve.AddListener(solveObjectEventDict[object_name].ev_ThisEvent.Invoke);
            print("Connected " + object_to_connect.name + " to " + solveObjectEventDict[object_name]);
        }
    }

    public void InitializeNextDay()
    {
        SetDayIndex(TimeManager.Instance.GetDay() - 1);
        PopulateEventDict();
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

}
