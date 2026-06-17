using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
public interface ISaveable
{
    public void SaveData(string identifier);
    public void InitializeSaveData(string identifier);
}
