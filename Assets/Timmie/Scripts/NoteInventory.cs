using System.Collections.Generic;
using UnityEngine;


/*
Contributor(s): Timmie Xiong
Brief Description: Holds the notes data in a list and handles adding notes to said list.
Date: 7/8/26
*/
public class NoteInventory : MonoBehaviour
{
    public static NoteInventory Instance { get; private set; }
    [SerializeField] private List<NoteData> PickedUpNotes = new List<NoteData>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNote(NoteData pickedUpNote)
    {
        PickedUpNotes.Add(pickedUpNote);
    }
}
