using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Fishing isFishing;
    public bool castedFishingRod = false;
    public float spinSpeed = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isFishing.isHoldingFishingRod == true)
        {
            castedFishingRod = true;
        }
        else
        {
            castedFishingRod = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (castedFishingRod == true)
        {
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        }
    }
}
