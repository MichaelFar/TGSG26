using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InventoryItemData itemData;

    private GameObject objectToFollow;


    public UnityEvent ev_Activated;
    void Start()
    {
        itemData.ev_DroppedItem.AddListener(DropItemBehavior);
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
    }
    void InterpolateToObject()
    {
        if (objectToFollow)
        {
            transform.DOMove(objectToFollow.transform.position, Time.deltaTime * 20.0f);
        }
    }

    public void SetObjectToFollow(GameObject new_object_to_follow)
    {
        objectToFollow = new_object_to_follow;
    }
}
