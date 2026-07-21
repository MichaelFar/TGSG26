using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Fishing isFishing;
    public float spinSpeed = 20;
    public float currentLocation = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        if (isFishing.FishingQTE == true)
        {
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
            currentLocation = transform.localEulerAngles.z;
            
            if (Input.GetMouseButtonDown(0))
            {
                if (currentLocation > 235 && currentLocation < 275)
                {
                    print("Success");
                    isFishing.FishingQTE.enabled = false;
                }
                else
                {
                    print("Fail");
                    isFishing.FishingQTE.enabled = false;
                }
               
            }
        }
    }
}