using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;

public class SaveDataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string endDataSymbol = "---";
    public static SaveDataManager Instance { get { return _instance; } }
    private static SaveDataManager _instance;

    private List<ObjectDataSaver> saveDataObjectList = new List<ObjectDataSaver>();

    private string masterID = "MasterSave";

    private bool isFrameOne = false;

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

    private void Start()
    {
        
    }
    private void Update()
    {
        if (!isFrameOne)
        {
            isFrameOne = true;
            if(!SaveGame.Exists(masterID))
            {
                print("MasterList doesn't exist, creating it now");
                SaveMasterList();
                SaveAllSaveObjects();
            }
            else
            {
                print("MasterList exists");
                print(SaveGame.SavePath);
            }
                
        }
        if(Input.GetKeyUp(KeyCode.Space ))
        {
            LoadAllSaveObjects();
        }
    }
    public void AddObjectToList(ObjectDataSaver new_data_saver)
    {
        if(saveDataObjectList.Contains(new_data_saver))
        {
            saveDataObjectList[saveDataObjectList.IndexOf(new_data_saver)] = new_data_saver;
        }
        else
        {
            saveDataObjectList.Add(new_data_saver);
        }
        
    }
    public void SaveMasterList()
    {
        SaveGame.Save<List<ObjectDataSaver>>(masterID, saveDataObjectList);
    }
    public void LoadMasterList()
    {
        saveDataObjectList = SaveGame.Load<List<ObjectDataSaver>>(masterID, saveDataObjectList);
    }

    public void SaveAllSaveObjects()
    {
        foreach(ObjectDataSaver i in saveDataObjectList)
        {
            i.RunSave();
        }
    }
    public void LoadAllSaveObjects()
    {
        foreach (ObjectDataSaver i in saveDataObjectList)
        {
            i.RunLoad();
        }
    }


}
