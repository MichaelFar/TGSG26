using UnityEngine;

public class AnimEventRelay : MonoBehaviour
{
    [SerializeField] private TransitionLoader transitionLoader;

    public void OnFadeOutComplete()
    {
        transitionLoader.WakeUp();
    }
    public void OnWakeUpComplete()
    {
        //ill put something here eventually
    }
}
