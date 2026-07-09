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
            StartTransition("This thing here");
        }

        //Transition.GetCurrentAnimatorStateInfo(0).

       
        
    }

    public void StartTransition(string text)
    {
        UIHandler.Instance.canPause = false;
        Transition.SetTrigger("FadeTrigger");



        SetTransitionText(text);
    }
    
}
