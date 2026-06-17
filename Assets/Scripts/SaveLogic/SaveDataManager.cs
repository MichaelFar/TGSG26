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
        
        
        //print(new_data_saver.GetObjectID());
        
    }


}
