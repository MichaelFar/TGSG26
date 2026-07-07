using NUnit.Framework;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Material DrySoil, WetSoil;
    [SerializeField] private GameObject[] GrowthStagePrefabs;
    [SerializeField] private Transform PlantGroup;
    private int CurrentGrowthStage = 0;
    private GameObject CurrentStagePrefab;

    private bool IsWatered;
    private MeshRenderer SoilMeshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoilMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void WateredPlant()
    {
        print("Watered the plant");
        IsWatered = true;
        CheckSoil(IsWatered);
        TimeManager.Instance.ConnectToDayEvent(TimeManager.Instance.GetDay() + 1, AdvanceNextStage);
    }
    public void AdvanceNextStage()
    {
        if (IsWatered)
        {
            CurrentGrowthStage++;
        }
        else
        {
            Wither();
        }
        if (CurrentStagePrefab != null)
        {
            Destroy(CurrentStagePrefab);
        }
        CurrentStagePrefab = Instantiate(GrowthStagePrefabs[CurrentGrowthStage], PlantGroup.position, PlantGroup.rotation, PlantGroup);
        IsWatered = false;
        CheckSoil(IsWatered);
    }

    private void Wither()
    {
        if (!IsWatered)
        {
            GrowthStagePrefabs[CurrentGrowthStage].GetComponent<MeshRenderer>().material.color = Color.brown;
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

}
