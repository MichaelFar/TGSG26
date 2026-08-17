using UnityEngine;

public class StalkingPhantom : MonoBehaviour, IViewable
{


    // Update is called once per frame
    void Update()
    {

    }


    public void OnView()
    {
        Destroy(gameObject);
    }

    public void OnLookAway()
    {
        throw new System.NotImplementedException();
    }
}
