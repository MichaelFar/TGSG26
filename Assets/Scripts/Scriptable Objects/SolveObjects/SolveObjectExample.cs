/*
Contributor(s): Michael Farrar
Brief Description: Inherits from Solve Object, test object for deleting the associated item when used to solve
Date: 6/5/2026
*/
using UnityEngine;

using UnityEngine.Events;
[CreateAssetMenu(fileName = "SolveObjectExample", menuName = "Scriptable Objects/SolveObjectExample")]
public class SolveObjectExample : SolveObject
{

    public override bool OnSolve()
    {
        if(base.OnSolve())
        {
            //slotToAffect.DropItemFromSlot();
            UnityEngine.MonoBehaviour.print("My custom behavior worked");
            
        }
        
        return true;
    }

    
}
