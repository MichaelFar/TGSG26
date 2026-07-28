/*
Contributor(s): Michael Farrar
Brief Description: name space for custom data types as well as a static class for helper functions
Date: 6/2/2026
*/

using System;
using UnityEngine;



namespace GlobalDataTypes
{
    public enum e_ItemTypes { DebugType, OtherDebugType, NoType };
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj, true);
        }
    }
    [Serializable] public class GameData
    {
        public string identifier;
        public string currentLevelName;
    }
    public static class HelperFunctions
    {
        public static T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }
    }
    

    
}
    

public static class GlobalHelperFunctions
{
    
}
