/*
Contributor(s): Michael Farrar
Brief Description: Item component that attaches to an item that can be picked up by the inventory
Date: 6/2/2026
*/

using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InventoryItemData itemData;

    public Collider myCollider;
    
    private Rigidbody myRigidBody;

    private GameObject objectToFollow;

    public bool isHeld = false;
    public UnityEvent ev_Activated;
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidBody = GetComponent<Rigidbody>();
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
        objectToFollow = null;
        myCollider.enabled = true;
        isHeld = false;
        myRigidBody.useGravity = true;
    }
    void InterpolateToObject()
    {
        if (objectToFollow)
        {

            //Tween move_tween = transform.DOMove(objectToFollow.transform.position, Time.deltaTime * 3.0f / (Time.deltaTime ));
            transform.forward = objectToFollow.transform.forward;
            transform.position = Vector3.Lerp(transform.position, objectToFollow.transform.position, Time.deltaTime * 5.0f);
            
        }
    }

    public void SetObjectToFollow(GameObject new_object_to_follow)
    {
        objectToFollow = new_object_to_follow;
        
        //InterpolateToObject();
    }
    //DEBUG replace with interaction system
    private void OnTriggerEnter(Collider other)
    {
        /*
        InventoryComponent inventory_obj = other.GetComponent<InventoryComponent>();
        print(other);
        if (inventory_obj && !isHeld)
        {
            print("Picking up item");
            inventory_obj.AddItemToArray(this);
            myCollider.enabled = false;
            myRigidBody.useGravity = false;
        }
        */
    }
    public GameObject GetDestinationObject()
    {
        return objectToFollow;
    }

    public void OnInteract(GameObject object_interacting)
    {
        InventoryComponent inventory_obj = object_interacting.GetComponent<InventoryComponent>();
        //print(other);
        if (inventory_obj && !isHeld)
        {
            print("Picking up item");
            inventory_obj.AddItemToArray(this);
            myCollider.enabled = false;
            myRigidBody.useGravity = false;
        }
    }
}
