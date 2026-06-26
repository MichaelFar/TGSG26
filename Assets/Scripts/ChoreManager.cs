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
    [HideInInspector]
    public ChoreTask[] currentChores;
    Dictionary<string, UnityEvent> solveObjectEventDict = new Dictionary<string, UnityEvent>();
    
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
            print("Adding task to task list");
        }
        

        return list_to_return;
    }

    public List<SolveObject> GetAllCurrentChoreSolveObjects()
    {
        List<SolveObject> list_to_return = new List<SolveObject>();

        List<ChoreTask> current_chores = GetAllCurrentChores();

        foreach (ChoreTask i in current_chores)
        {
            print("GetAllCurrentChoreSolveObjects()");
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

        foreach(SolveObject i in current_solve_object_list)
        {
            //print("Test");
            if(!solveObjectEventDict.ContainsKey(i.name))
            {
                solveObjectEventDict.Add(i.name, new UnityEvent());
                solveObjectEventDict[i.name].AddListener(TestInvokePrint);
                print("Adding key " + i.name);
            }
        }

    }

    public void ConnectSolveObjectToEventDict(SolveObject object_to_connect)
    {
        string object_name = object_to_connect.name.Replace("(Clone)", "");
        print("Modified object name is " + object_name);
        if(solveObjectEventDict.ContainsKey(object_name))
        {
            object_to_connect.ev_OnSolve.AddListener(solveObjectEventDict[object_name].Invoke);
            print("Connected " + object_to_connect.name + " to " + solveObjectEventDict[object_name]);
        }
    }
    private int numTestInvokes = 0;
    public void TestInvokePrint()
    {
        numTestInvokes += 1;
        print("This function has been invoked " + numTestInvokes + " time(s)");
    }
}
