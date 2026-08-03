using UnityEngine;
using UnityEngine.Events;

/*
Contributor(s): Timmie Xiong
Brief Description: Determines an area around the player that is safe to spawn in the demon. Will periodically choose a random spot within the bounds
to spawn it in.
Date: 7/31/2026
*/
public class DemonSpawner : MonoBehaviour
{
    public GameObject DemonPrefab;
    private GameObject ActiveDemon;
    public Transform Player;
    public float MinSpawnDist = 20.0f;
    public float MaxSpawnDist = 50.0f;
    public LayerMask GroundLayer;
    private float Timer = 0.0f;
    public float Interval = 10.0f;
    public UnityEvent ev_CheckForSpawn;

    public void SpawnDemon()
    {
        if (ActiveDemon != null)
        {
            print("Demon is already spawned");
            return;
        }
        Vector3 spawnPos = GetValidSpawnPosition();
        ActiveDemon = Instantiate(DemonPrefab, spawnPos, Quaternion.identity);
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(MinSpawnDist, MaxSpawnDist);

            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
            Vector3 candidate = Player.position + offset;

            Vector3 dirToCandidate = (candidate - Player.position).normalized;
            float dot = Vector3.Dot(Player.forward, dirToCandidate);

            if (dot < 0.3f)
            {
                return candidate;
            }
        }
        return Player.position + Player.forward * -MaxSpawnDist;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= Interval)
        {
            Timer = 0f;
            ev_CheckForSpawn.Invoke();
        }
    }
}
