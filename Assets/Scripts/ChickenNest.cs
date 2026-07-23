//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class ChickenNest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject normalEggToSpawn;
    public GameObject evilEggToSpawn;

    public GameObject spawnPoint;

    private bool readyToSpawn = false;

    private bool isOccupied = false;

    private GameObject eggSpawned;

    private InventoryItemData normalEggItemData;
    private InventoryItemData evilEggItemData;
    void Start()
    {
        normalEggItemData = normalEggToSpawn.GetComponent<ItemPickup>().itemData;
        evilEggItemData = evilEggToSpawn.GetComponent<ItemPickup>().itemData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNormalEgg()
    {
        GameObject egg_instance = Instantiate(normalEggToSpawn);
        egg_instance.transform.position = spawnPoint.transform.position;
        eggSpawned = egg_instance;
        eggSpawned.GetComponent<ItemPickup>().DropItemBehavior();
    }

    private void SpawnEvilEgg()
    {
        GameObject egg_instance = Instantiate(evilEggToSpawn);
        egg_instance.transform.position = spawnPoint.transform.position;
        eggSpawned = egg_instance;
        eggSpawned.GetComponent<ItemPickup>().DropItemBehavior();
    }

    public void HookNormalEggSpawnIntoDailyEvent()
    {
        TimeManager.Instance.ev_dayHasChanged.AddListener(DailyEggSpawnNormal);
    }
    public void HookEvilEggSpawnIntoDailyEvent()
    {
        TimeManager.Instance.ev_dayHasChanged.AddListener(DailyEggSpawnEvil);
    }

    public void DailyEggSpawnNormal()
    {
        if(!isOccupied)
        {
            SpawnNormalEgg();
            TimeManager.Instance.ev_dayHasChanged.RemoveListener(DailyEggSpawnNormal);
            isOccupied = true;
        }
    }
    public void DailyEggSpawnEvil()
    {
        if (!isOccupied)
        {
            SpawnEvilEgg();
            TimeManager.Instance.ev_dayHasChanged.RemoveListener(DailyEggSpawnEvil);
            isOccupied = true;
        }
        else if (eggSpawned.GetComponent<ItemPickup>().itemData == normalEggItemData)
        {
            Destroy(eggSpawned);
            SpawnEvilEgg(); 
            isOccupied = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        

        
            ItemPickup item = other.GetComponent<ItemPickup>();
            if(item)
            {
                if (item.gameObject == eggSpawned)
                {
                    if (item.itemData == normalEggItemData || item.itemData == evilEggItemData)
                    {
                        isOccupied = false;
                    }
                }
                
            }
        
    }
}
