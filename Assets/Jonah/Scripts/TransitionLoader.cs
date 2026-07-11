using UnityEngine;
using UnityEngine.UIElements;

public class TransitionLoader : MonoBehaviour
{

    [SerializeField] private Animator Transition;
    
    public ChangeText TransitionText;

    

    public void SetTransitionText(string text)
    {
        TransitionText.SetText(text);
    }

    public void Start()
    {
        StartNewGameTransition("Day 1");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartTransition("This thing here");
        }

        //Transition.GetCurrentAnimatorStateInfo(0).

       
        
    }


    public void StartNewGameTransition(string text)
    {
        Transition.SetTrigger("NewGameTrigger");

        //There is no way to retrieve the time programmatically, it is hard coded for this reason
        TimeManager.Instance.PauseTimeForDuration(2.15f);
        SetTransitionText(text);
    }
    public void StartTransition(string text)
    {
        
        Transition.SetTrigger("FadeTrigger");

        print(Transition.GetCurrentAnimatorStateInfo(Transition.GetLayerIndex("Base Layer")).length + " is the duration of the fade");
        //There is no way to retrieve the time programmatically, it is hard coded for this reason
        TimeManager.Instance.PauseTimeForDuration(3.15f);
        SetTransitionText(text);
    }
    
}
