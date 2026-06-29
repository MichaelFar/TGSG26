using UnityEngine;

[CreateAssetMenu(fileName = "ChoreDay", menuName = "Scriptable Objects/ChoreDay")]
public class ChoreDay : ScriptableObject
{
    public ChoreTask[] choreList;
}
