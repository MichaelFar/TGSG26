using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using GlobalDataTypes;
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

    public enum e_TriggerReset
    {
        [InspectorName("Trigger Every Day")]
        EveryDay = -2,
        [InspectorName("Trigger At Night")]
        EveryNight = -1,
        [InspectorName("Trigger On Day 1")]
        Day1 = 0,
        [InspectorName("Trigger On Day 2")]
        Day2 = 1,
        [InspectorName("Trigger On Day 3")]
        Day3 = 2,
        [InspectorName("Trigger On Day 4")]
        Day4 = 3,
        [InspectorName("Trigger On Day 5")]
        Day5 = 4,
        
    }
    [Header("What days can this be triggered?")]
    [SerializeField]
    public e_TriggerReset[] triggerTimeList;

    [Header("How many times can this trigger? Resets on the specified days. Make -1 for indefinite")]
    public int timesCanTrigger = -1;

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

    private bool canTrigger = false;
    private int numTimesTriggered = 0;
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

        e_TriggerReset[] no_duplicate_triggers = triggerTimeList.Distinct().ToArray();

        int[] day_index_array = new int[TimeManager.Instance.GetMaxDays()];
        int index = 0;
        foreach (e_TriggerReset i in no_duplicate_triggers)
        {
            if(i == e_TriggerReset.EveryDay)
            {
                TimeManager.Instance.ev_dayHasChanged.AddListener(SetCanTriggerToTrue);
                TimeManager.Instance.ev_dayHasChanged.AddListener(ResetNumTriggers);
                SetCanTriggerToTrue();
            }
            else if (i == e_TriggerReset.EveryNight)
            {
                TimeManager.Instance.ev_NightTime.AddListener(SetCanTriggerToTrue);
                TimeManager.Instance.ev_NightTime.AddListener(ResetNumTriggers);
            }
            else
            {
                TimeManager.Instance.ConnectToDayEvent((int)i, SetCanTriggerToTrue);
                TimeManager.Instance.ConnectToDayEvent((int)i, ResetNumTriggers);

                day_index_array[index] = (int)i;
                print("Adding day index " + day_index_array[index] + " to day index array");
                index += 1;
                if((int)i == 0)
                {
                    SetCanTriggerToTrue();
                }
            }

        }

        int[] day_index_list = HelperFunctions.InitializeArray<int>(TimeManager.Instance.GetMaxDays());
        foreach(int i in day_index_list)
        {
            if(!day_index_array.Contains(i) && i != 0)
            {
                print("Setting day index " + i + " to setting trigger to false");
                TimeManager.Instance.ConnectToDayEvent(i, SetCanTriggerToFalse);
            }
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
        if (!chaserInstance && numTimesTriggered < timesCanTrigger || !chaserInstance && timesCanTrigger < 0)
        {
            if(canTrigger)
            {
                numTimesTriggered += 1;
                ev_ChaseStarted.Invoke();
                chaserInstance = Instantiate(chaserPrefab);


                chaserInstance.transform.position = spawnPoint.transform.position;
            }
                
                
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

    private void SetCanTriggerToTrue()
    {
        canTrigger = true;
    }
    private void SetCanTriggerToFalse()
    {
        canTrigger = false;
    }

    private void ResetNumTriggers()
    {
        numTimesTriggered = 0;
    }
}
