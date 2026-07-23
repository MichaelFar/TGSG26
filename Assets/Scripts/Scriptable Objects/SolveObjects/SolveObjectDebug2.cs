/*
Contributor(s): Michael Farrar
Brief Description: Inherits from Solve Object, test object for deleting the associated item when used to solve
Date: 6/5/2026
*/
using UnityEngine;

using UnityEngine.Events;
[CreateAssetMenu(fileName = "SolveObjectDebug2", menuName = "Scriptable Objects/SolveObjectDropItem")]
public class SolveObjectDebug2 : SolveObject
{

    public override bool OnSolve()
    {
        if(base.OnSolve())
        {
            slotToAffect.DropItemFromSlot();
        }
        
        return true;
    }

    
}
