using UnityEngine;

using System.Collections.Generic;

public interface ISerializer
{
    string Serialize<T>(T obj);
    T Deserialize<T>(string json);
    
}
