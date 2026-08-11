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

    public GameObject playerRespawnPoint;

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
        EveryDay = -10,
        [InspectorName("Trigger At Night")]
        EveryNight = -1,
        [InspectorName("Trigger On Day 1")]
        Day1 = 0,
        [InspectorName("Trigger On Night 1")]
        Night1 = -2,
        [InspectorName("Trigger On Day 2")]
        Day2 = 1,
        [InspectorName("Trigger On Night 2")]
        Night2 = -3,
        [InspectorName("Trigger On Day 3")]
        Day3 = 2,
        [InspectorName("Trigger On Night 3")]
        Night3 = -4,
        [InspectorName("Trigger On Day 4")]
        Day4 = 3,
        [InspectorName("Trigger On Night 4")]
        Night4 = -5,
        [InspectorName("Trigger On Day 5")]
        Day5 = 4,
        [InspectorName("Trigger On Night 5")]
        Night5 = -6,

    }

    public enum e_CatchConsequence
    {
        [InspectorName("Spawn Player At The Respawn Point")]
        RespawnPlayerAtSpawnPoint,
        [InspectorName("Restart the day")]
        RestartDay,
        [InspectorName("End the day")]
        EndDay
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

    [Header("What happens when the player is caught?")]
    public e_CatchConsequence chosenConsequence;

    [SerializeField]
    [Header("How long does the chase last if not ended?")]
    private float lifeTime = 15.0f;

    private float lifeTimer = 0.0f;

    public UnityEvent ev_ChaseStarted;

    public UnityEvent ev_ChaseEnded;

    private UnityAction consequenceAction;

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
        
        if (startingVolume == e_TriggerVolumes.TriggerVolumeA)
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
            starting_volume.SetLayerToViewLayer();
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
            ending_volume.SetLayerToViewLayer();
        }

        if(chosenConsequence == e_CatchConsequence.RespawnPlayerAtSpawnPoint)
        {
            consequenceAction = TeleportToSpawnPoint;
            
        }
        else if(chosenConsequence == e_CatchConsequence.RestartDay)
        {
            consequenceAction = RestartDay;
        }
        else
        {
            consequenceAction = SkipToNextDay;
        }


        e_TriggerReset[] no_duplicate_triggers = triggerTimeList.Distinct().ToArray();

        int[] day_index_array = new int[TimeManager.Instance.GetMaxDays()];
        int[] night_index_array = new int[TimeManager.Instance.GetMaxDays()];
        
        int index = 0;
        foreach (e_TriggerReset i in no_duplicate_triggers)
        {
            print("Trigger found: " + i);
            if(i == e_TriggerReset.EveryDay)
            {
                TimeManager.Instance.ev_dayHasChanged.AddListener(SetCanTriggerToTrue);
                TimeManager.Instance.ev_dayHasChanged.AddListener(ResetNumTriggers);
                SetCanTriggerToTrue();
                print("Every Day has been selected in list");
            }
            else if (i == e_TriggerReset.EveryNight)
            {
                TimeManager.Instance.ev_NightTime.AddListener(SetCanTriggerToTrue);
                TimeManager.Instance.ev_NightTime.AddListener(ResetNumTriggers);
            }
            else if ((int)i < 0)
            {
                int night_index = ((int)i * -1) - 2;
                TimeManager.Instance.ConnectToNightEvent(night_index, SetCanTriggerToTrue);
                TimeManager.Instance.ConnectToNightEvent(night_index, ResetNumTriggers);
                night_index_array[night_index] = night_index;
                print("Adding night index " + night_index + " to night index array");
                
            }
            else
            {
                TimeManager.Instance.ConnectToDayEvent((int)i, SetCanTriggerToTrue);
                TimeManager.Instance.ConnectToDayEvent((int)i, ResetNumTriggers);

                day_index_array[index] = (int)i;
                print("Adding day index " + day_index_array[index] + " to day index array");
                index += 1;
                if ((int)i == 0)
                {
                    SetCanTriggerToTrue();
                }
            }

        }

        int[] day_index_list = Enumerable.Range(0, TimeManager.Instance.GetMaxDays()).ToArray();
        foreach(int i in day_index_list)
        {
            if (!day_index_array.Contains(i) && i != 0)
            {
                print("Setting day index " + i + " to setting trigger to false");
                TimeManager.Instance.ConnectToDayEvent(i, SetCanTriggerToFalse);
            }
            else
            {
                print("day index array contains " + i);
            }

        }
        int[] night_index_list = Enumerable.Range(0, TimeManager.Instance.GetMaxDays()).ToArray();
        foreach (int i in night_index_list)
        {
            if (!night_index_array.Contains(i))
            {
                print("Setting night index " + i + " to setting trigger to false");
                TimeManager.Instance.ConnectToNightEvent(i, SetCanTriggerToFalse);
            }
            else
            {
                print("night index array contains " + i);
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
            print("Spawning Chase");
            if(canTrigger)
            {
                numTimesTriggered += 1;
                ev_ChaseStarted.Invoke();
                chaserInstance = Instantiate(chaserPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);


                //chaserInstance.transform.position = spawnPoint.transform.position;

                print("Chase AI spawned at " + chaserInstance.transform.position + " and spawn point is " + spawnPoint.transform.position);

                ChaseAI chase_ai = chaserInstance.GetComponent<ChaseAI>();

                if(chase_ai)
                {
                    chase_ai.ev_ReachedPlayer.AddListener(consequenceAction);
                    chase_ai.ev_ReachedPlayer.AddListener(DespawnChaser);
                    
                }
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
        print("Setting can chase trigger to true");
        canTrigger = true;
    }
    private void SetCanTriggerToFalse()
    {
        print("Setting can chase trigger to false");
        canTrigger = false;
    }

    private void ResetNumTriggers()
    {
        numTimesTriggered = 0;
    }

    private void TeleportAndTransitionPlayer(Vector3 location, string transition_text)
    {
        //PlayerGlobal.Instance.GetComponent<CharacterController>().enabled = false;
        Physics.SyncTransforms();
        
        PlayerGlobal.Instance.playerRootObject.transform.position = location;

        PlayerGlobal.Instance.transitionController.StartNewGameTransition(transition_text);
        //PlayerGlobal.Instance.GetComponent<CharacterController>().enabled = true;

    }

    private void TeleportToSpawnPoint()
    {
        TeleportAndTransitionPlayer(playerRespawnPoint.transform.position, "");

    }
    private void TeleportToBed(string transition_text)
    {
        TeleportAndTransitionPlayer(PlayerGlobal.Instance.bedController.spawnPoint.transform.position, transition_text);
    }

    private void RestartDay()
    {
        TeleportToBed("Day " + TimeManager.Instance.GetDay().ToString());
        TimeManager.Instance.ResetDayToBeginning();
    }
    private void SkipToNextDay()
    {
        TeleportToBed("Day " + (TimeManager.Instance.GetDay() + 1).ToString());
        TimeManager.Instance.SkipToNextDay();
        
    }


        
}
