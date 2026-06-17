/*
Contributor(s): Michael Farrar
Brief Description: Item component that attaches to an item that can be picked up by the inventory
Date: 6/2/2026
*/

using BayatGames.SaveGameFree;
using DG.Tweening;
using GlobalDataTypes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour, IInteractable,ISaveable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InventoryItemData itemData;

    public Collider myCollider;
    
    private Rigidbody myRigidBody;

    private GameObject objectToFollow;

    public bool isHeld = false;
    public UnityEvent ev_Activated;
    [HideInInspector]
    public bool isInitialized = false;
    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        myRigidBody = GetComponent<Rigidbody>();
        
    }
    void Start()
    {
        
        if(itemData)
        {
            itemData.ev_DroppedItem.AddListener(DropItemBehavior);
        }
        isInitialized = true;
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
        SetColliderEnabled(true);
        isHeld = false;
        SetUseGravity(true);
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
            inventory_obj.DetermineItemPickup(this);
            
        }
    }
    public void SetUseGravity(bool new_value)
    {
        myRigidBody.useGravity = new_value;
    }
    public void SetColliderEnabled(bool new_value)
    {
        myCollider.enabled = new_value;
    }

    public void InitializeSaveData(string identifier)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(string identifier)
    {
        SaveGame.Save<ItemPickup>(identifier, this);
    }
}
