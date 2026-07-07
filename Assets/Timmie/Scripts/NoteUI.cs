using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class NoteUI : BaseUI
{
    [SerializeField] private NoteData noteData;
    [SerializeField] private TMP_Text noteText;
    public override void Show()
    {
        base.Show();
        LoadContent();

    }

    private void LoadContent()
    {
        noteText.text = noteData.body;
    }
}
