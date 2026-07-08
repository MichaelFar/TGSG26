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
    [SerializeField] private Transform PlantGroup;
    private int CurrentGrowthStage = 0;
    private GameObject CurrentStagePrefab;

    private bool IsWatered;
    private bool IsDead;
    private bool IsReadyToHarvest;
    private MeshRenderer SoilMeshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoilMeshRenderer = GetComponent<MeshRenderer>();
        TimeManager.Instance.ev_dayHasChanged.AddListener(AdvanceNextStage);
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
        CurrentStagePrefab = Instantiate(GrowthStagePrefabs[CurrentGrowthStage], PlantGroup.position, PlantGroup.rotation, PlantGroup);
        IsWatered = false;
        CheckSoil(IsWatered);
        if (CurrentGrowthStage == 2)
        {
            IsReadyToHarvest = true;
        }
        print("Current Growth Stage: " + CurrentGrowthStage);
    }

    private void Wither()
    {
        if (!IsWatered)
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
        }
        print("Harvested Plant");
    }

}
