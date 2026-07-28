using UnityEngine;

public class Stalker : MonoBehaviour, IViewable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform transformToCastFrom;

    public GameObject visualParent;

    public Collider triggerVolume;
    public bool isActive = false;
    void Start()
    {
        SetIsActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsActive(bool new_value)
    {
        isActive = new_value;
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer i in renderers)
        {
            i.enabled = new_value;
        }
    }

    public void OnView()
    {
        SetIsActive(false);
    }

    public void RandomlyAppear()
    {
        float roll = Random.Range(0.0f, 10.0f);
        print("Rolling to reappear");
        if(roll <= 5.0f)
        {
            SetIsActive(true);
        }
    }
}
