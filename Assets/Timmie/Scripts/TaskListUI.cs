using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
Contributor(s): Timmie Xiong
Brief Description: Handles input for opening/closing the Task UI
Date: 6/1/26
*/
public class TaskListUI : BaseUI
{
    private CanvasGroup taskListUIGroup;
    private bool isOpen = false;

    public TextMeshProUGUI textLabel;
    string demon_mod = "<color=\"red\">";

    string normal_mod = "<color=\"white\">";

    string completed_mod = "<color=\"green\">";
    private List<TaskLabelStruct> taskLabelList = new List<TaskLabelStruct>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taskListUIGroup = GetComponent<CanvasGroup>();
        taskListUIGroup.alpha = 0;

        UIHandler.Instance.OnTaskListToggled += SetVisible;
        ChoreManager.Instance.ev_ChoreUpdated.AddListener(UpdateTextLabel);
        //TimeManager.Instance.ev_dayHasChanged.AddListener(PopulateTextLabelList);
       // TimeManager.Instance.ev_dayHasChanged.AddListener(UpdateTextLabel);
        PopulateTextLabelList();
        UpdateTextLabel();
    }

    void OnDestroy()
    {
        if (UIHandler.Instance != null)
        {
            UIHandler.Instance.OnTaskListToggled -= SetVisible;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetVisible(bool visible)
    {
        if (visible)
        {
            UIHandler.Instance.ShowUI(this);
            PauseMenu.Instance.SetGamePaused(false);
        }
        else
        {
            UIHandler.Instance.CloseUI();
        }
    }

    public void PopulateTextLabelList()
    {
        

        List<string> description_list = ChoreManager.Instance.GetAllChoreDescriptions();

        List<ChoreTask> chore_list = ChoreManager.Instance.GetAllCurrentChores();

        taskLabelList.Clear();

        foreach (ChoreTask i in chore_list)
        {
            if(i.GetIsDemon())
            {
                taskLabelList.Add(new TaskLabelStruct(i ,demon_mod));
            }
            else if(!i.GetIsDemon())
            {
                taskLabelList.Add(new TaskLabelStruct(i ,normal_mod));
            }
            else
            {
                taskLabelList.Add(new TaskLabelStruct(i ,completed_mod));
            }
                
        }
    }

    private class TaskLabelStruct
    {
        public TaskLabelStruct(ChoreTask new_task, string new_text_mod)
        {
            task = new_task;
            string_mod = new_text_mod;
            //text = new_text;
            //isComplete = new_status;
        }

        public ChoreTask task;
        public string string_mod;
        public string text { get { return task ? string_mod + task.GetDescription() + "</color> \n": ""; } }
        //Checks if task is not null then returns GetIsComplete if true and false if false
        public bool isComplete { get { return task ? task.GetIsComplete() : false; } }
    }

    public void UpdateTextLabel()
    {
        print("Updating label");
        textLabel.text = "";
        foreach (TaskLabelStruct i in taskLabelList)
        {
            if(i.isComplete)
            {
                int index = taskLabelList.FindIndex(p => p.text == i.text);
                string description = taskLabelList.Find(p => p.text == i.text).text;
                taskLabelList[index].string_mod = i.string_mod.Replace(normal_mod, completed_mod);
                if(i.task.GetIsDemon())
                {
                    taskLabelList[index].string_mod = i.string_mod.Replace(demon_mod, completed_mod);
                }
            }
            textLabel.text += i.text;
        }

    }
}
