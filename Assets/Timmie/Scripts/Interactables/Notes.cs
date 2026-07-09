using UnityEngine;
using UnityEngine.Events;

public class Notes : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent itemPickUp;
    [SerializeField] private NoteUI noteUI;
    [SerializeField] private NoteData noteData;

    public void OnInteract(GameObject object_interacting = null)
    {
        itemPickUp.Invoke();
    }

    public void ShowNote()
    {
        UIHandler.Instance.ShowNoteUI(noteData);
        PauseMenu.Instance.SetGamePaused(true);
        NoteInventory.Instance.AddNote(noteData);
        Destroy(gameObject);
    }
}
