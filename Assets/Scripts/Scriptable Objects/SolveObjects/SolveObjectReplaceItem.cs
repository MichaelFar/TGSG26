/*
Contributor(s): Michael Farrar
Brief Description: Inherits from Solve Object, This can replace the held item with the item itemToReplace
Date: 6/5/2026
*/
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.Events;
[CreateAssetMenu(fileName = "SolveObjectReplaceItem", menuName = "Scriptable Objects/SolveObjectReplaceObject")]


public class SolveObjectReplaceObjectInHand : SolveObject
{
    
    public GameObject itemToReplace;
    public float timeDelayBeforeFollow = 2.0f;
    public override bool OnSolve()
    {
        if(base.OnSolve())
        {
            
            Transform previous_item_transform = slotToAffect.GetHeldItem().transform;
            slotToAffect.DeleteItemFromSlot();
            ItemPickup instanced_item = SpawnItem(previous_item_transform).GetComponent<ItemPickup>();
            
            PickupItem(instanced_item);
            
        }
        
        return true;
    }
    
    private GameObject SpawnItem(Transform new_transform)
    {
        return Instantiate(itemToReplace, new_transform.position, new_transform.rotation);
    }
    
    private void PickupItem(ItemPickup item_to_add)
    {
       // yield return new WaitUntil(() => item_to_add.isInitialized);


        slotToAffect.AddItemToSlot(item_to_add);
        
        item_to_add.SetUseGravity(false);
        item_to_add.SetColliderEnabled(false);
        item_to_add.SetObjectToFollow(slotToAffect.gameObject);
    }
    
    
}
