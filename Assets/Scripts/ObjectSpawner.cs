using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject spawnPointObject;

    public GameObject prefabToSpawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SpawnObject()
    {
        GameObject object_to_spawn = Instantiate(prefabToSpawn);

        
        
        object_to_spawn.transform.position = spawnPointObject.transform.position;
        
        
        
    }
}
