using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;
using System.Collections;

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

    [SerializeField]
    private bool spawnEnabled = false;

    private Coroutine activeTimerCoroutine;
    
    public UnityEvent ev_CheckForSpawn;


    private void Start()
    {

        SetSpawnEnabled(spawnEnabled);
        
    }
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

            //Determine the angle of spawn location by finding a point on a unit circle then multiply it by distance to actually set the distance
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
            Vector3 spawnLocation = Player.position + offset;

            Vector3 dirToSpawnLocation = (spawnLocation - Player.position).normalized;
            float dot = Vector3.Dot(Player.forward, dirToSpawnLocation);

            // checks if the spawn location is not within the player's POV
            if (dot < 0.3f)
            {
                return spawnLocation;
            }
        }
        return Player.position + Player.forward * -MaxSpawnDist;
    }

    void Update()
    {
        /*
        Timer += Time.deltaTime;
        if (Timer >= Interval)
        {
            Timer = 0f;
            if(spawnEnabled)
            {
                ev_CheckForSpawn.Invoke();
            }
                
        }
        */
    }

    public void SetSpawnEnabled(bool new_value)
    {
        spawnEnabled = new_value;
        if (spawnEnabled)
        {
            if (activeTimerCoroutine != null)
            {
                StopCoroutine(activeTimerCoroutine);
            }
            activeTimerCoroutine = StartCoroutine(SpawnTimerCoroutine());
        }
        else
        {
            if (activeTimerCoroutine != null)
            {
                StopCoroutine(activeTimerCoroutine);
            }
        }
    }

    IEnumerator SpawnTimerCoroutine()
    {
        yield return new WaitForSeconds(Interval);
        if (spawnEnabled)
        {
            ev_CheckForSpawn.Invoke();
            activeTimerCoroutine = StartCoroutine(SpawnTimerCoroutine());
        }
    }
}
