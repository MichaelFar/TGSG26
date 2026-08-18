using UnityEngine;

public class StalkingPhantom : MonoBehaviour, IViewable
{

    public bool shouldDieOnView = true;
    // Update is called once per frame
    void Update()
    {

    }


    public void OnView()
    {
        if(shouldDieOnView)
        {
            Destroy(gameObject);
        }
    }

    public void OnLookAway()
    {
        throw new System.NotImplementedException();
    }
}
