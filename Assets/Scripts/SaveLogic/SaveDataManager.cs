using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using UnityEditor.Overlays;

public class SaveDataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string endDataSymbol = "---";
    public static SaveDataManager Instance { get { return _instance; } }
    private static SaveDataManager _instance;

    private List<ObjectDataSaver> saveDataObjectList = new List<ObjectDataSaver>();

    private List<MasterSaveStruct> saveDataStructList = new List<MasterSaveStruct>();

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
            SaveGame.Save<string>("stringtest", "Hello I have saved");
            isFrameOne = true;
            
            if(!SaveGame.Exists(masterID))
            {
                print("MasterList doesn't exist, creating it now");
                SaveMasterList();
                //SaveAllSaveObjects();
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
        if(Input.GetKeyUp(KeyCode.Keypad5))
        {
            SaveAllSaveObjects();
        }
    }
    public void AddObjectToList(ObjectDataSaver new_data_saver)
    {
        MasterSaveStruct new_save_object = new MasterSaveStruct(new_data_saver.GetObjectID(), new_data_saver.GetPathToAssociatedPrefab());
        if(saveDataObjectList.Contains(new_data_saver))
        {
            saveDataObjectList[saveDataObjectList.IndexOf(new_data_saver)] = new_data_saver;
        }
        else
        {
            saveDataObjectList.Add(new_data_saver);
        }

        if (saveDataStructList.Contains(new_save_object))
        {
            saveDataStructList[saveDataStructList.IndexOf(new_save_object)] = new_save_object;
        }
        else
        {
            saveDataStructList.Add(new_save_object);
        }
        SaveMasterList();
    }
    public void SaveMasterList()
    {
        foreach(MasterSaveStruct i in saveDataStructList)
        {
            SaveGame.Save<string>(masterID, i.assetPath);
        }
        
    }
    public void LoadMasterList()
    {
        saveDataStructList = SaveGame.Load<List<MasterSaveStruct>>(masterID, saveDataStructList);
        foreach(MasterSaveStruct i in saveDataStructList)
        {

        }
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

    struct MasterSaveStruct
    {
        public MasterSaveStruct(string id, string path)
        {
            objectID = id;
            assetPath = path;
        }
        public string objectID;
        public string assetPath;
    }
    

}
