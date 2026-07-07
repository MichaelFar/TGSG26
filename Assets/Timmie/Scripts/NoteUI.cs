using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteUI : BaseUI
{
    [SerializeField] private NoteData noteData;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private Image noteImage;
    [SerializeField] private Sprite noteSprite;
    public override void Show()
    {
        base.Show();
        LoadContent();

    }

    private void LoadContent()
    {
        // noteText.text = noteData.body;
        noteImage.sprite = noteSprite;
    }
}
