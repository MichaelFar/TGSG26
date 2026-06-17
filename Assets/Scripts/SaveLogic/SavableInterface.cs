using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
public interface ISaveable
{
    public Dictionary<string, string> GetSaveData();
    public void InitializeSaveData();
}
