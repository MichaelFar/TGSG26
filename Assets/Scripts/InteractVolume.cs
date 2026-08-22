using UnityEngine;
using UnityEngine.Events;
public class InteractVolume : MonoBehaviour, IInteractable
{

    public UnityEvent ev_Interacted;

    public UnityEvent ev_LookedAway;

    public UnityEvent ev_LookedAt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CanInteract()
    {
        ev_LookedAt.Invoke();
        return true;
    }

    public void LookedAway()
    {
        ev_LookedAway.Invoke();
        
    }

    public void OnInteract(GameObject object_interacting = null)
    {
        ev_Interacted.Invoke();
    }
}
