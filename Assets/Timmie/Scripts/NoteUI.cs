using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteUI : BaseUI
{

    [SerializeField] private TMP_Text noteText;
    [SerializeField] private Image noteImage;
    public void LoadContent(NoteData data)
    {
        // noteText.text = noteData.body;
        noteImage.sprite = data.noteSprite;
    }
}
