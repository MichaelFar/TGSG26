using UnityEngine;
using UnityEngine.Events;


/*
Contributor(s): Timmie Xiong
Brief Description: A timer that periodically rolls the chance for something to appear
Date: 7/31/2026
*/
public class RandomlyAppearTimer : MonoBehaviour
{

    private float Timer = 0f;
    private float Interval = 10f;
    public UnityEvent ev_CheckForAppearance;
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= Interval)
        {
            Timer = 0f;
            ev_CheckForAppearance.Invoke();
        }
    }
}
