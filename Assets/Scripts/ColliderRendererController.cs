using UnityEngine;

public class ColliderRendererController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private MeshRenderer thisRenderer;
    private Collider thisCollider;


    private MeshRenderer[] childRenderers;
    private Collider[] childColliders;


    public bool startHidden = false;
    private void Awake()
    {
        thisRenderer = GetComponent<MeshRenderer>();
        thisCollider = GetComponent<Collider>();

        childRenderers = GetComponentsInChildren<MeshRenderer>();
        childColliders = GetComponentsInChildren<Collider>();
    }
    void Start()
    {
        if(startHidden)
        {
            DisableCollidersAndHideRenderers();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableCollidersAndHideRenderers()
    {
        print("Disabling renderer");
        foreach (MeshRenderer i in childRenderers)
        {
            i.enabled = false;
        }
        foreach (Collider i in childColliders)
        {
            i.enabled = false;
        }

        thisRenderer.enabled = false;
        thisCollider.enabled = false;
    }

    public void EnableCollidersAndHideRenderers()
    {
        foreach (MeshRenderer i in childRenderers)
        {
            i.enabled = true;
        }
        foreach (Collider i in childColliders)
        {
            i.enabled = true;
        }

        thisRenderer.enabled = true;
        thisCollider.enabled = true;
    }

}
