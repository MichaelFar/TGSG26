using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using GlobalDataTypes;
namespace System.Persistence
{
    public interface ISaveable
    {
        public void SaveData(GameData data, bool should_overwrite);
        GameData LoadAndSetData(string identifier);

        public void DeleteSave(string identifier);
    }

}