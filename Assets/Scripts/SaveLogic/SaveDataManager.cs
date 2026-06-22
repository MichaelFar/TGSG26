using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using UnityEditor.Overlays;
using GlobalDataTypes;
using System.Persistence;
using UnityEngine.SceneManagement;
public class SaveDataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public GameData gameData;
    FileDataService dataService;

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
        dataService = new FileDataService(new JsonSerializer());

    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        /*
        if (!isFrameOne)
        {
            SaveGame.Save<string>("stringtest", "Hello I have saved");
            isFrameOne = true;
            
            if(!SaveGame.Exists(masterID))
            {
                print("MasterList doesn't exist, creating it now");
                //SaveMasterList();
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
        */
    }
    public void NewGame()
    {
        gameData = new GameData { identifier = "New game", currentLevelName = "DemoScene"};
        SaveGame();
        //SceneManager.LoadScene(gameData.currentLevelName);
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
        //SaveMasterList();
    }
    /*
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
    */
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
    
    public void SaveGame()
    {
        dataService.SaveData(gameData);
    }    
    public void LoadGame(string game_name)
    {
        gameData = dataService.LoadAndSetData(game_name);
        if(string.IsNullOrEmpty(gameData.currentLevelName))
        {
            gameData.currentLevelName = "DemoScene";
        }
        SceneManager.LoadScene(gameData.currentLevelName);
    }
    public void DeleteGame(string game_name)
    {
        dataService.DeleteSave(game_name);
    }

}
