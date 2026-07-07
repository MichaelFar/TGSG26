using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using GlobalDataTypes;
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}
namespace System.Persistence
{
    public interface ISaveable
    {
        SerializableGuid Id { get; init; }
        public void SaveData(GameData data, bool should_overwrite);
        GameData LoadAndSetData(string identifier);

        
        public void DeleteSave(string identifier);
    }
    public interface IBind<TData> where TData : ISaveable
    {
        SerializableGuid Id { get; set; }
        void Bind(TData data);
    }

}