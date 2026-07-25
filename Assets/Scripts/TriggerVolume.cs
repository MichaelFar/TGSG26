using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent ev_EnteredVolume;

    public UnityEvent ev_ExitedVolume;
    private void OnTriggerEnter(Collider other)
    {
        ev_EnteredVolume.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        ev_ExitedVolume.Invoke();
    }
}
