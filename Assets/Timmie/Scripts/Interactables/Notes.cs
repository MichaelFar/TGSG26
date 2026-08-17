using UnityEngine;
using UnityEngine.Events;

public class Notes : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent itemPickUp;
    [SerializeField] private NoteUI noteUI;
    [SerializeField] private NoteData noteData;

    public bool CanInteract()
    {
        return true;
    }

    public void LookedAway()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(GameObject object_interacting = null)
    {
        itemPickUp.Invoke();
    }

    public void ShowNote()
    {
        UIHandler.Instance.ShowNoteUI(noteData);
        PauseMenu.Instance.SetGamePaused(true);
        NoteInventory.Instance.AddNote(noteData);
        NoteInvUI.Instance.PopulateGrid();
        Destroy(gameObject);
    }
}
