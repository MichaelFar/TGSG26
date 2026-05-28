using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InventoryItemData itemData;

    private Collider myCollider;
    
    private GameObject objectToFollow;


    public UnityEvent ev_Activated;
    void Start()
    {
        myCollider = GetComponent<Collider>();
        if(itemData)
        {
            itemData.ev_DroppedItem.AddListener(DropItemBehavior);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        InterpolateToObject();
    }

    public void Activate()
    {
        ev_Activated.Invoke();
    }

    public void DropItemBehavior()
    {
        transform.parent = null;
        myCollider.enabled = true;
    }
    void InterpolateToObject()
    {
        if (objectToFollow)
        {
            
            transform.DOMove(objectToFollow.transform.position, Time.deltaTime * 1.0f / (Time.deltaTime * 2.0f));
            
        }
    }

    public void SetObjectToFollow(GameObject new_object_to_follow)
    {
        objectToFollow = new_object_to_follow;
    }
    //DEBUG replace with interaction system
    private void OnTriggerEnter(Collider other)
    {
        InventoryComponent inventory_obj = other.GetComponent<InventoryComponent>();
        print(other);
        if (inventory_obj)
        {
            print("Picking up item");
            inventory_obj.AddItemToArray(this);
            myCollider.enabled = false;
        }
    }
    
}
