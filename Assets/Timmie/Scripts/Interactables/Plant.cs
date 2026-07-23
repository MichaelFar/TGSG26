using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;


/*
Contributor(s): Timmie Xiong
Brief Description: The plant interactable. Looks at an array of prefabs in the inspector and grows to the next stage if watered.
If not watered everyday it will wither and die. Attach this to the soil for the plant.
Date: 7/7/26
*/
public class Plant : MonoBehaviour
{
    [SerializeField] private Material DrySoil, WetSoil, DeadGrass;
    [SerializeField] private GameObject[] GrowthStagePrefabs;
    [SerializeField] private GameObject[] spookyGrowthStagePrefabs;
    [SerializeField] private Transform PlantGroup;
    private int CurrentGrowthStage = 0;
    private GameObject CurrentStagePrefab;

    private bool IsWatered;
    private bool IsDead;
    private bool IsReadyToHarvest;
    private MeshRenderer SoilMeshRenderer;

    public GameObject normalPlantItem;

    public GameObject spookyPlantItem;

    public GameObject spawnLocationObject;

    public SolveObject bloodMilkSO;

    private GameObject plantToSpawnOnHarvest;

    private PuzzleInteractionPoint myInteractionPoint;

    private int frameCount = 0;
    private void Awake()
    {
        myInteractionPoint = GetComponent<PuzzleInteractionPoint>();
        //plantToSpawnOnHarvest = normalPlantItem;
        //myInteractionPoint.ev_AllListsInitialized.AddListener(InitializeSOEvents);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoilMeshRenderer = GetComponent<MeshRenderer>();
        TimeManager.Instance.ev_dayHasChanged.AddListener(AdvanceNextStage);
        plantToSpawnOnHarvest = normalPlantItem;
        
    }

    void Update()
    {
        frameCount += 1;
        if(frameCount == 1)
        {
            InitializeSOEvents();
        }
    }

    public void InitializeSOEvents()
    {
        
        myInteractionPoint.ConnectToSOParallelEvent(bloodMilkSO, SwitchPrefabsToSpooky);
        myInteractionPoint.ConnectToSOParallelEvent(bloodMilkSO, WateredPlant);
        
    }

    public void WateredPlant()
    {
        print("Watered the plant");
        IsWatered = true;
        CheckSoil(IsWatered);
    }
    public void AdvanceNextStage()
    {
        if (IsWatered && !IsDead)
        {
            CurrentGrowthStage++;
        }
        else if (!IsWatered)
        {
            Wither();
            return;
        }
        if (CurrentStagePrefab != null)
        {
            Destroy(CurrentStagePrefab);
            
        }
        if (GrowthStagePrefabs[CurrentGrowthStage])
        {
            CurrentStagePrefab = Instantiate(GrowthStagePrefabs[CurrentGrowthStage], spawnLocationObject.transform.position, PlantGroup.rotation, PlantGroup);
        }
        IsWatered = false;
        CheckSoil(IsWatered);
        if (CurrentGrowthStage == GrowthStagePrefabs.Length - 1)
        {
            IsReadyToHarvest = true;
        }
        print("Current Growth Stage: " + CurrentGrowthStage);
    }

    private void Wither()
    {
        if (!IsWatered && CurrentStagePrefab)
        {
            CurrentStagePrefab.GetComponent<MeshRenderer>().material = DeadGrass;
            IsDead = true;
        }
    }

    private void CheckSoil(bool isWatered)
    {
        if (isWatered)
        {
            SoilMeshRenderer.material = WetSoil;
        }
        else
        {
            SoilMeshRenderer.material = DrySoil;
        }
    }

    public void HarvestPlant()
    {
        if (IsReadyToHarvest)
        {
            Destroy(CurrentStagePrefab);
            GameObject spawned_plant = Instantiate(plantToSpawnOnHarvest);
            spawned_plant.transform.position = spawnLocationObject.transform.position;
        }
        print("Harvested Plant");
    }
    private void SwitchPrefabsToSpooky()
    {
        GrowthStagePrefabs = spookyGrowthStagePrefabs;
        plantToSpawnOnHarvest = spookyPlantItem;
    }

}
