using UnityEngine;
using UnityEngine.UI;

public class PromptController : MonoBehaviour
{
    public Text promptText;

    public void SetText(string new_text)
    {
        promptText.text = new_text;
    }




}
