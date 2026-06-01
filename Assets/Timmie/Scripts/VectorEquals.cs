using UnityEngine;

public class VectorEquals
{
    public static bool Equals(Vector3 a, Vector3 b, float tolerance)
    {
        return Mathf.Abs(a.x - b.x) < tolerance &&
        Mathf.Abs(a.y - b.y) < tolerance &&
        Mathf.Abs(a.z - b.z) < tolerance;
    }
}
