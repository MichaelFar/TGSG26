using UnityEngine;

public class TransitionLoader : MonoBehaviour
{

    [SerializeField] private Animator Transition;
    
    public ChangeText TransitionText;

    public void SetTransitionText(string text)
    {
        TransitionText.SetText(text);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartTransition();
        }
    }

    public void StartTransition()
    {
        Transition.SetTrigger("FadeTrigger");

        SetTransitionText("Is this right?");
    }

}
