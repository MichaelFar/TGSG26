using UnityEngine.UI;
using Unity.GraphToolkit.Editor;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    /*
Contributor(s): Jonah, Timmie, Michael
Brief Description: Changes text on the canvas
Date: 6/3/2026
*/

    private Text changeText;


    public void SetText(string set_text) 
    {
        changeText.text = set_text;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            //Debug.Log("Variable value is: " + changeText);
            SetText("bruh.com");
        } 
    }
}
