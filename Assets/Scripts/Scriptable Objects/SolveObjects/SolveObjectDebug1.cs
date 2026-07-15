/*
Contributor(s): Michael Farrar
Brief Description: Inherits from Solve Object, test object for deleting the associated item when used to solve
Date: 6/5/2026
*/
using UnityEngine;

using UnityEngine.Events;
[CreateAssetMenu(fileName = "SolveObjectDebug", menuName = "Scriptable Objects/SolveObjectDeleteObjectInHand")]
public class SolveObjectDeleteObjectInHand : SolveObject
{
    
    public override bool OnSolve()
    {
        if(base.OnSolve())
        {
            slotToAffect.DeleteItemFromSlot();
            
        }
        
        return true;
    }

    
}
