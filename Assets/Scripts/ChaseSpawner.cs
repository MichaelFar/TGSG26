using UnityEngine;
using UnityEngine.Events;
public class ChaseSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject chaserPrefab;

    [SerializeField]
    private GameObject spawnPoint;

    private GameObject chaserInstance;

    public TriggerVolume triggerVolumeA;
    public TriggerVolume triggerVolumeB;
    

    public enum e_TriggerTypes
    {
        [InspectorName("When looked at")]
        LookedAt,
        [InspectorName("When the player enters this")]
        Entered,
        [InspectorName("When the player exits this")]
        Exited
    }

    public enum e_TriggerVolumes
    {
        TriggerVolumeA,
        TriggerVolumeB
    }

    
    [Header("Which volume starts the chase?")]
    public e_TriggerVolumes startingVolume;

    [Header("How does it start the chase?")]
    public e_TriggerTypes startingTrigger;

    [Header("Which volume ends the chase?")]
    public e_TriggerVolumes endingVolume;

    [Header("How does it end the chase?")]
    public e_TriggerTypes endingTrigger;

    [SerializeField]
    [Header("How long does the chase last if not ended?")]
    private float lifeTime = 15.0f;

    private float lifeTimer = 0.0f;

    public UnityEvent ev_ChaseStarted;

    public UnityEvent ev_ChaseEnded;
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
        InitializeValues();
    }

    private void InitializeValues()
    {
        TriggerVolume starting_volume;
        if(startingVolume == e_TriggerVolumes.TriggerVolumeA)
        {
            starting_volume = triggerVolumeA;
        }
        else
        {
            starting_volume = triggerVolumeB;
        }

        if(startingTrigger == e_TriggerTypes.Entered)
        {
            starting_volume.ev_EnteredVolume.AddListener(SpawnChaser);
        }
        else if (startingTrigger == e_TriggerTypes.Exited)
        {
            starting_volume.ev_ExitedVolume.AddListener(SpawnChaser);
        }
        else
        {
            starting_volume.ev_Viewed.AddListener(SpawnChaser);
        }
        TriggerVolume ending_volume;
        if (endingVolume == e_TriggerVolumes.TriggerVolumeA)
        {
            ending_volume = triggerVolumeA;
        }
        else
        {
            ending_volume = triggerVolumeB;
        }

        if (endingTrigger == e_TriggerTypes.Entered)
        {
            ending_volume.ev_EnteredVolume.AddListener(DespawnChaser);
        }
        else if (startingTrigger == e_TriggerTypes.Exited)
        {
            ending_volume.ev_ExitedVolume.AddListener(DespawnChaser);
        }
        else
        {
            ending_volume.ev_Viewed.AddListener(DespawnChaser);
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        if(chaserInstance)
        {
            lifeTimer += Time.deltaTime;
            if(lifeTimer >= lifeTime)
            {
                DespawnChaser();
            }
        }
    }

    public void SpawnChaser()
    {
        if (!chaserInstance)
        {
            ev_ChaseStarted.Invoke();
            chaserInstance = Instantiate(chaserPrefab);

            
            chaserInstance.transform.position = spawnPoint.transform.position;
                
        }
    }

    public void DespawnChaser()
    {
        if(chaserInstance)
        {
            ev_ChaseEnded.Invoke();
            Destroy(chaserInstance);
        }
    }
}
