using UnityEngine;
using UnityEngine.Events;

public class FishingSpawn : MonoBehaviour
{

    public GameObject fishToSpawn;
    public GameObject evilFishToSpawn;

    public GameObject spawnPoint;

    private bool readyToSpawn = false;

    private bool isOccupied = false;

    private GameObject fishSpawned;

    private InventoryItemData fishItemData;
    private InventoryItemData evilFishItemData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishItemData = fishToSpawn.GetComponent<ItemPickup>().itemData;
        evilFishItemData = evilFishToSpawn.GetComponent<ItemPickup>().itemData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnNormalFish()
    {
        GameObject fish_instance = Instantiate(fishToSpawn);
        fish_instance.transform.position = spawnPoint.transform.position;
        fishSpawned = fish_instance;
        fishSpawned.GetComponent<ItemPickup>().DropItemBehavior();
    }

    private void SpawnEvilFish()
    {
        GameObject fish_instance = Instantiate(evilFishToSpawn);
        fish_instance.transform.position = spawnPoint.transform.position;
        fishSpawned = fish_instance;
        fishSpawned.GetComponent<ItemPickup>().DropItemBehavior();
    }

    public void HookNormalFishSpawnIntoDailyEvent()
    {
        TimeManager.Instance.ev_dayHasChanged.AddListener(DailyFishSpawnNormal);
    }

    public void HookEvilFishSpawnIntoDailyEvent()
    {
        TimeManager.Instance.ev_dayHasChanged.AddListener(DailyFishSpawnEvil);
    }

    public void DailyFishSpawnNormal()
    {
        if(!isOccupied)
        {
            SpawnNormalFish();
            isOccupied = true;
        }
    }

    public void DailyFishSpawnEvil()
    {
        if(!isOccupied)
        {
            SpawnEvilFish();
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    if(item)
    {
        if(item.gameObject == fishSpawned)
        {
            if(item.itemData == normalEggItemData || item.itemData == evilEggItemData)
            {
                isOccupied = false;
            }
        }
    }
}
