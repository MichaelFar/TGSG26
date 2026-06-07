using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

/*
Contributor(s): Timmie Xiong
Brief Description: Handles input for opening/closing the Task UI
Date: 6/1/26
*/
public class TaskListUI : MonoBehaviour
{
    private CanvasGroup taskListUIGroup;
    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taskListUIGroup = GetComponent<CanvasGroup>();
        taskListUIGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool is_held = Keyboard.current.tabKey.IsPressed();
        if (is_held != isOpen)
        {
            isOpen = is_held;
            taskListUIGroup.alpha = is_held ? 1 : 0;
        }
    }
}
