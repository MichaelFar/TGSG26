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

    void Start()
    {
        TimeManager.Instance.ev_dayHasChanged.AddListener(CheckIfHasBeenUsedThenTeleport);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnInteract(GameObject object_interacting = null)
    {
        if(!TimeManager.Instance.isNight)
        {
            subtitleController.DisplaySubtitlesWithTimer("It is too early to go to bed...", 5.0f);
        }
        else
        {
            hasBeenUsed = true;
            TimeManager.Instance.SkipToNextDay();
            DisplayNewDayTransition();
        }
    }

    public void CheckIfHasBeenUsedThenTeleport()
    {
        print("Checking if bed has been used");
        if(hasBeenUsed)
        {
            hasBeenUsed = false;
            return;
        }

        if(shouldTeleportAtEOD)
        {
            player.gameObject.transform.position = spawnPoint.transform.position;
            DisplayTeleportNewDayTransition();
        }
            

    }

    public void DisplayNewDayTransition()
    {
        transitionObject.StartTransition("Day " + TimeManager.Instance.GetDay().ToString());
    }
    public void DisplayTeleportNewDayTransition()
    {
        transitionObject.StartNewGameTransition("Day " + TimeManager.Instance.GetDay().ToString());
    }

}
