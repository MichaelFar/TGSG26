using UnityEngine;
using UnityEngine.Events;

public class Notes : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent itemPickUp;
    [SerializeField] private NoteUI noteUI;

    public void OnInteract(GameObject object_interacting = null)
    {
        itemPickUp.Invoke();
    }

    public void ShowNote()
    {
        UIHandler.Instance.ShowUI(noteUI);
        PauseMenu.Instance.SetGamePaused(true);
    }
}
