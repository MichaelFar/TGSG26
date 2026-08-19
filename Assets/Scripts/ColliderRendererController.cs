using UnityEngine;

public class ColliderRendererController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private MeshRenderer thisRenderer;
    private Collider thisCollider;
    private SkinnedMeshRenderer thisSkinRenderer;



    private MeshRenderer[] childRenderers;
    private Collider[] childColliders;
    

    private SkinnedMeshRenderer[] childSkinRenderers;

    public bool startHidden = false;
    public bool startWithNoCollision = false;
    private void Awake()
    {
        thisRenderer = GetComponent<MeshRenderer>();
        thisCollider = GetComponent<Collider>();
        thisSkinRenderer = GetComponent<SkinnedMeshRenderer>();


        childRenderers = GetComponentsInChildren<MeshRenderer>();
        childColliders = GetComponentsInChildren<Collider>();
        childSkinRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    void Start()
    {
        if(startHidden)
        {
            HideRenderers();
        }
        if(startWithNoCollision)
        {
            DisableColliders();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideRenderers()
    {
        foreach (MeshRenderer i in childRenderers)
        {
            i.enabled = false;
        }
        foreach (SkinnedMeshRenderer i in childSkinRenderers)
        {
            i.enabled = false;
        }
        if (thisSkinRenderer)
            thisSkinRenderer.enabled = false;
        if (thisRenderer)
            thisRenderer.enabled = false;
    }

    public void EnableRenderers()
    {
        foreach (SkinnedMeshRenderer i in childSkinRenderers)
        {
            i.enabled = true;
        }
        foreach (MeshRenderer i in childRenderers)
        {
            i.enabled = true;
        }
        if (thisSkinRenderer)
            thisSkinRenderer.enabled = true;
        if (thisRenderer)
            thisRenderer.enabled = true;
    }

    public void EnableColliders()
    {
        foreach (Collider i in childColliders)
        {
            i.enabled = true;
        }

        if (thisCollider)
            thisCollider.enabled = true;
    }

    public void DisableColliders()
    {
        foreach (Collider i in childColliders)
        {
            i.enabled = false;
        }

        if (thisCollider)
            thisCollider.enabled = false;
    }

    public void DisableCollidersAndHideRenderers()
    {
        print("Disabling renderer");

        DisableColliders();

        HideRenderers();
    }
    //Misnamed, enables both
    public void EnableCollidersAndHideRenderers()
    {
        EnableColliders();
        EnableRenderers();
        
    }

}
