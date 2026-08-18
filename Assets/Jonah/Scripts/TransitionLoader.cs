using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class TransitionLoader : MonoBehaviour
{

    [SerializeField] private Animator Transition;
    [SerializeField] private Transform PlayerCamAnchor, BedCamAnchor;
    [SerializeField] private Camera PlayerCam;
    private Animator PlayerCamAnimator;
    private MoveCamera MoveCam;
    private Transform CameraTransform;
    private bool PlayerSlept;

    public ChangeText TransitionText;



    public void SetTransitionText(string text)
    {
        TransitionText.SetText(text);
    }

    public void Start()
    {
        //StartNewGameTransition("Day 1");
        PlayerCamAnimator = PlayerCam.GetComponent<Animator>();
        PlayerCamAnimator.enabled = false;
        MoveCam = PlayerCam.GetComponent<MoveCamera>();
        CameraTransform = PlayerCam.transform;
    }

    public void Update()
    {
        
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
        //Enable camera's animator component and disable move camera component at the start of animation


        Transition.SetTrigger("FadeTrigger");

        print(Transition.GetCurrentAnimatorStateInfo(Transition.GetLayerIndex("Base Layer")).length + " is the duration of the fade");
        //There is no way to retrieve the time programmatically, it is hard coded for this reason
        TimeManager.Instance.PauseTimeForDuration(3.15f);
        SetTransitionText(text);
    }

    public void WakeUp()
    {
        print("WakeUp called, PlayerSlept = " + PlayerSlept);
        if (PlayerSlept)
        {
            PlayerCamAnimator.enabled = true;
            MoveCam.enabled = false;

            CameraTransform.SetParent(BedCamAnchor);
            CameraTransform.localPosition = Vector3.zero;
            CameraTransform.localRotation = Quaternion.identity;
            PlayerCamAnimator.Play("WakeUp", 0, 0f);
            print("Waking up");
        }
    }

    public void SetHasPlayerSlept(bool hasPlayerSlept)
    {
        PlayerSlept = hasPlayerSlept;
    }

}
