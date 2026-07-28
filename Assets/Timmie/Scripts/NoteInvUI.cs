using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class NoteInvUI : BaseUI
{
    public static NoteInvUI Instance { get; private set; }
    [SerializeField] private Transform GridContainer;
    [SerializeField] private GameObject NoteSlotPrefab;
    [SerializeField] private NoteInventory NoteInv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopulateGrid()
    {
        foreach (Transform child in GridContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (NoteData note in NoteInv.GetNotes())
        {
            GameObject noteSlot = Instantiate(NoteSlotPrefab, GridContainer);
            Image icon = noteSlot.GetComponent<Image>();
            icon.sprite = note.noteSprite;

            Button btn = noteSlot.GetComponent<Button>();
            if (btn != null)
            {
                NoteData collectedNote = note;
                btn.onClick.AddListener(() => OnNoteClicked(collectedNote));
            }
        }
    }

    private void OnNoteClicked(NoteData note)
    {
        UIHandler.Instance.ShowNoteUI(note);
        print("Opened Note");
    }
}
