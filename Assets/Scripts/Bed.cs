using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject spawnPoint;

    public SubtitleController subtitleController;
    
    void Start()
    {
        
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

        }
    }

}
