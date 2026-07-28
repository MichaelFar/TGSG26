using BayatGames.SaveGameFree;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Persistence;
public class ObjectDataSaver : MonoBehaviour
{
    /*
    private string objectID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<SavePackage> managedComponents;

    private string filePathToHeldObjectPrefab = "";
    private void Awake()
    {
        objectID = name;
        

    }
    void Start()
    {
        SaveDataManager.Instance.AddObjectToList(this);
        if (SaveGame.Exists(objectID))
        {
            managedComponents = SaveGame.Load<List<SavePackage>>(objectID);
        }
        else
        {
            managedComponents = GetAllSaveableComponents();
        }
        
        filePathToHeldObjectPrefab = AssetDatabase.GetAssetPath(gameObject);//PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
        print("My asset path is " + filePathToHeldObjectPrefab);
        //SaveGame.Save<List<SavePackage>>(objectID, managedComponents);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetObjectID()
    {
        return objectID;
    }

    public List<SavePackage> GetAllSaveableComponents()
    {
        List<Component> list_to_check = GetComponents<Component>().ToList<Component>();
        List<SavePackage> list_to_return = new List<SavePackage>();
        foreach (Component i in list_to_check)
        {
            if(i is ISaveable)
            {
                SavePackage new_package = new SavePackage(i.GetComponent<ISaveable>(), i, i.name);
                
                list_to_return.Add(new_package);
                
            }
        }

        return list_to_return;
        
    }

    public void RunSave()
    {
        foreach(SavePackage i in managedComponents)
        {
            //i.saveableComponent?.SaveData(i.identifier);
        }
    }
    public void RunLoad()
    {
        foreach (SavePackage i in managedComponents)
        {
            //i.saveableComponent?.LoadAndSetData(i.identifier);
        }
    }
    
    
    public void UpdateSaveList()
    {
        managedComponents = GetAllSaveableComponents();
    }
    public string GetPathToAssociatedPrefab()
    {
        return filePathToHeldObjectPrefab;
    }
    public struct SavePackage
    {
        public SavePackage(ISaveable new_saveable, Component new_component, string new_identifier)
        {
            saveableComponent = new_saveable;
            relevantComponent = new_component;
            identifier = new_identifier;
        }
        public ISaveable saveableComponent;
        public Component relevantComponent;
        public string identifier;
    }
    */

}
