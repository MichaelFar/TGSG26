using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BayatGames.SaveGameFree;
using System;
using System.Linq;
public class ObjectDataSaver : MonoBehaviour
{

    private string objectID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        objectID = name;
    }
    void Start()
    {
        SaveDataManager.Instance.AddObjectToList(this);
        foreach (Component i in GetAllSaveableComponents())
        {
            print(i.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetObjectID()
    {
        return objectID;
    }

    public List<Component> GetAllSaveableComponents()
    {
        List<Component> list_to_check = GetComponents<Component>().ToList<Component>();
        List<Component> list_to_return = new List<Component>();
        foreach (Component i in list_to_check)
        {
            if(i is ISaveable)
            {
                list_to_return.Add(i);
            }
        }

        return list_to_return;
        
    }
    
}
