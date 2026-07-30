using UnityEngine;

public class ChaseSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject chaserPrefab;

    [SerializeField]
    private GameObject spawnPoint;

    private GameObject chaserInstance;

    void Start()
    {
        if(!spawnPoint)
        {
            spawnPoint = gameObject;
        }
        foreach (MeshRenderer i in GetComponentsInChildren<MeshRenderer>())
        {
            i.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnChaser(Vector3 spawn_point_override = new Vector3())
    {
        if (!chaserInstance)
        {

            chaserInstance = Instantiate(chaserPrefab);

            if (spawn_point_override != new Vector3())
            {
                chaserInstance.transform.position = spawnPoint.transform.position;
                return;
            }

            chaserInstance.transform.position = spawn_point_override;
        }
    }

    public void DespawnChaser()
    {
        if(chaserInstance)
        {
            Destroy(chaserInstance);
        }
    }
}
