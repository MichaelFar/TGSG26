/*
Contributor(s): Michael Farrar
Brief Description: Chore days are a list of chore tasks, they are used by the chore manager
Date: 6/27/2026
*/
using UnityEngine;

[CreateAssetMenu(fileName = "ChoreDay", menuName = "Scriptable Objects/ChoreDay")]
public class ChoreDay : ScriptableObject
{
    public ChoreTask[] choreList;
}
