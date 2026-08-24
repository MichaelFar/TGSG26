using UnityEngine;

[CreateAssetMenu(fileName = "SubtitleSequence", menuName = "Scriptable Objects/SubtitleSequence")]
public class SubtitleSequence : ScriptableObject
{
    public string[] subtitleArray;

    public float GetDurationOfLine(int index)
    {
        float time_val = 0.8f;
        
        return Mathf.Clamp(subtitleArray[index].Length * time_val, .75f, 10.0f);
    }
}
