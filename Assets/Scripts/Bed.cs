using UnityEditorInternal;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject spawnPoint;

    public SubtitleController subtitleController;

    public TransitionLoader transitionObject;

    public PlayerMovement player;

    private bool hasBeenUsed = false;

    public bool shouldTeleportAtEOD = true;

    private bool allChoresCompletedToday = true;

    private float debugTimer = 0.0f;

    void Start()
    {
        TimeManager.Instance.ev_dayHasChanged.AddListener(CheckIfHasBeenUsedThenTeleport);

    }

    // Update is called once per frame
    void Update()
    {
        //Remove this later, this is just for prototype
        debugTimer += Time.deltaTime;
        if (debugTimer > 1)
        {
            debugTimer = 0.0f;
            allChoresCompletedToday = ChoreManager.Instance.CheckIfCurrentDayCompleted();

        }
    }
    public void OnInteract(GameObject object_interacting = null)
    {
        if (!TimeManager.Instance.isNight)
        {
            subtitleController.DisplaySubtitlesWithTimer("It is too early to go to bed...", 5.0f);
        }
        else
        {
            hasBeenUsed = true;
            transitionObject.SetHasPlayerSlept(hasBeenUsed);
            print("Set PlayerSlept to " + hasBeenUsed);
            TimeManager.Instance.SkipToNextDay();
            DisplayNewDayTransition();
        }
    }

    public void CheckIfHasBeenUsedThenTeleport()
    {
        print("Checking if bed has been used");
        if (hasBeenUsed)
        {
            hasBeenUsed = false;
            return;
        }

        if (shouldTeleportAtEOD)
        {
            print("Teleporting player");
            Physics.SyncTransforms();
            player.transform.position = spawnPoint.transform.position;
            DisplayTeleportNewDayTransition();
        }


    }

    public void DisplayNewDayTransition()
    {
        if (allChoresCompletedToday)
        {
            transitionObject.StartTransition("Day " + TimeManager.Instance.GetDay().ToString());
        }
        else
        {
            transitionObject.StartTransition("Day " + TimeManager.Instance.GetDay().ToString() + " but you didn't complete all chores yesterday you bozo");
        }
        ResetChoresCompleted();

    }
    public void DisplayTeleportNewDayTransition()
    {
        if (allChoresCompletedToday)
        {
            transitionObject.StartNewGameTransition("Day " + TimeManager.Instance.GetDay().ToString());
        }
        else
        {
            transitionObject.StartNewGameTransition("Day " + TimeManager.Instance.GetDay().ToString() + " but you didn't complete all chores yesterday you bozo");
        }
        ResetChoresCompleted();
    }

    public bool CanInteract()
    {
        return true;
    }

    private void ResetChoresCompleted()
    {
        allChoresCompletedToday = false;
    }

    public void LookedAway()
    {
        throw new System.NotImplementedException();
    }
}
