using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour, IViewable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent ev_EnteredVolume;

    public UnityEvent ev_ExitedVolume;

    public UnityEvent ev_Viewed;


    public void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
        if(ev_Viewed.GetPersistentEventCount() > 0)
        {
            SetLayerToViewLayer();
        }
            
    }

    public void OnView()
    {
        print("Viewed trigger volume");
        ev_Viewed.Invoke();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Player entered trigger volume");
        ev_EnteredVolume.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        print("Player left trigger volume");
        ev_ExitedVolume.Invoke();
    }

    public void SetLayerToViewLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("ViewLayer");
    }
}
