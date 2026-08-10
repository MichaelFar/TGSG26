/*
Contributor(s): Alec Nguyen, Michael Farrar
Brief Description: Time manager singleton that controls the flow of time in the game, has events that can be listened to
Date: 6/23/2026
*/
using GlobalDataTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Persistence;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;
    [SerializeField]
    private Texture2D[] skyboxArray;
    private int currentSkyboxIndex = 0;

    [SerializeField] private Gradient gradientNightToSunrise;
    [SerializeField] private Gradient gradientSunriseToDay;
    [SerializeField] private Gradient gradientDayToSunset;
    [SerializeField] private Gradient gradientSunsetToNight;
    [SerializeField] private Light globalLight;
    [SerializeField]
    private Gradient[] gradientArray;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI nightText;
    [SerializeField] private TextMeshProUGUI dayChangeText;

    [Range(0.0f, 100.0f)]
    public float percentThresholdToGiveWarning = 75;

    private float minutes;
    public float Minutes { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    public float minutesThreshold = 5;

    private float hours;
    public float Hours { get { return hours; } set { hours = value; OnHoursChange(value); } }

    public float hoursThreshold = 10;

    private int days;
    public int Days { get { return days; } set { days = value; OnDayChange(value);} }

    public int minutesPerHour = 5;
    public int hoursPerDay = 10;
    public int maxDays = 5;
    
    private float sunriseHour
    {
        get { return hoursPerDay * 1/4; }
    }

    private float dayHour
    {
        get { return hoursPerDay * 2/5; }
    }

    private float sunsetHour
    {
        get { return hoursPerDay * 3/4; }
    }

    private float nightHour
    {
        get { return hoursPerDay * 4/5; }
    }

    private float tempSecond;

    public float minuteLength = 1;

    private float secondsCountToday;

    public UnityEvent ev_NightTime;
    public UnityEvent ev_dayOneEvent;
    public UnityEvent ev_dayTwoEvent;
    public UnityEvent ev_dayThreeEvent;
    public UnityEvent ev_dayFourEvent;
    public UnityEvent ev_dayFiveEvent;

    private UnityEvent[] dayEventArray;

    public UnityEvent ev_dayHasChanged;

    private bool timeIsPaused = false;
    public static TimeManager Instance { get { return _instance; } }
    private static TimeManager _instance;

    private bool hasInvokedNight = false;
    private float total_seconds_before_text_change;
    
    private float secondsUntilTextureChange = 0.0f;

    private float totalTimeElapsed = 0.0f;

    private float pauseTimeTimerGoal = 0.0f;

    private bool timerRunning = false;

    public SubtitleController subtitleController;
    [HideInInspector]
    public bool isNight = false;
    private void Awake()
    {

        dayEventArray = InitializeArray<UnityEvent>(maxDays);
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        //startingSecondsThreshold = total_seconds_in_day * 0.2f;

        if(minutesThreshold <= 0)
        {
            minutesThreshold = 1;
        }

        if(hoursThreshold <= 0)
        {
            hoursThreshold = 1;
        }
    }
    
    public void Start()
    {
        dayChangeText.text = "Day " + days.ToString();
        Days = 1;
        //This line visually syncs the skybox with the typical experience the player encounters per subsequent day
        SetSkyboxTexture(skyboxArray[currentSkyboxIndex + 1], skyboxArray[currentSkyboxIndex + 1]);
        currentSkyboxIndex = 1;
        

    }

    public void Update()
    {
        totalTimeElapsed += Time.deltaTime;
        if(!timeIsPaused)
        {
            tempSecond += Time.deltaTime;
            secondsCountToday += Time.deltaTime;
            secondsUntilTextureChange += Time.deltaTime;
        }
        
        if(timerRunning)
        {
            if(totalTimeElapsed >= pauseTimeTimerGoal)
            {
                timerRunning = false;
                SetPauseTime(false);
            }
        }
        
        if (tempSecond >= minuteLength)
        {
            tempSecond = 0;
            Minutes++;
        }
        ProcessSkyBoxTransition();
        
        if(Input.GetButtonUp("DebugTimeStop"))
        {
            //SetPauseTime(true);
            ResetDayToBeginning();
        }

    }
    //Note does not pause the game, just stops the time manager tick
    public void SetPauseTime(bool new_value)
    {
        timeIsPaused = new_value;
    }

    public void PauseTimeForDuration(float duration)
    {
        if(timerRunning)
        {
            return;
        }
        timerRunning = true;
        SetPauseTime(true);
        pauseTimeTimerGoal = totalTimeElapsed + duration;
    }

    private void ProcessSkyBoxTransition()
    {
        total_seconds_before_text_change = (hoursThreshold * minutesThreshold * minuteLength) * 0.25f;
        SetSkyboxBlend(secondsUntilTextureChange / (total_seconds_before_text_change));
        SetLightBlend(secondsUntilTextureChange / (total_seconds_before_text_change));
        isNight = secondsCountToday >= (hoursThreshold * minutesThreshold * minuteLength) * .75f; //|| value < 2;
        //globalLight.transform.rotation.SetEulerRotation((secondsCountToday / total_seconds_in_day) * 360.0f, 0.0f, 0.0f);//(1f / 1440f) * 360f, Space.World);
        globalLight.transform.rotation = Quaternion.Euler((secondsCountToday / total_seconds_before_text_change) * 90.0f, 0.0f, 0.0f);
        if (isNight == true && !hasInvokedNight)
        {
            nightText.text = "Night";
            ev_NightTime.Invoke();
            hasInvokedNight = true;
            subtitleController.DisplaySubtitlesWithTimer("It's getting late. I should head to bed", (hoursThreshold * minutesThreshold * minuteLength) * .25f);
        }

        if (secondsUntilTextureChange >= total_seconds_before_text_change)
        {
            print(total_seconds_before_text_change * 4.0f + " is total time today");

            if (currentSkyboxIndex == skyboxArray.Length - 1)
            {
                SetSkyboxTexture(skyboxArray[currentSkyboxIndex], skyboxArray[0]);

                currentSkyboxIndex = 0;
            }
            else
            {

                SetSkyboxTexture(skyboxArray[currentSkyboxIndex], skyboxArray[currentSkyboxIndex + 1]);
                currentSkyboxIndex += 1;
            }
            print("Index of skybox is " + currentSkyboxIndex + " and array length is " + skyboxArray.Length);

            secondsUntilTextureChange = 0.0f;
            
        }

        if (timeText != null)
        {
            timeText.text = days.ToString("00") + ":" + hours.ToString("00") + ":" + minutes.ToString("00");
        }
    }

    public void ResetDayToBeginning()
    {
        tempSecond = 0;
        secondsCountToday = 0;
        secondsUntilTextureChange = 0;
        
        SetSkyboxTexture(skyboxArray[0], skyboxArray[1]);
        currentSkyboxIndex = 1;
        minutes = 0;
        hours = 0;
        if (timeText != null)
        {
            timeText.text = days.ToString("00") + ":" + hours.ToString("00") + ":" + minutes.ToString("00");
        }
    }


    public void SkipToNextDay()
    {
        ResetDayToBeginning();
        Days += 1;
    }
    private void OnMinutesChange(float value)
    {
//Change values to be able to be changed by the designer easily
        //globalLight.transform.Rotate(Vector3.right, (1f/1440f)*360f, Space.World);
        if(value>= minutesThreshold)
        {
            Minutes = 0;
            Hours++;
        }
        if (Hours >= hoursThreshold)
        {
            Hours = 0;
            Days++;
            
        }
    }

    private void OnHoursChange(float value)
    {
        bool isNight = value >= hoursThreshold * .75; //|| value < 2;

        


    }

    private void OnDayChange(int value)
    {
        print("Delta ticks elapsed today " + secondsCountToday);
        secondsCountToday = 0;
        hasInvokedNight = false;
        nightText.text = "Day";
        dayChangeText.text = "Day " + value.ToString();
        if (value == 1)
        {
            ev_dayOneEvent.Invoke();
        }
        else if (value == 2)
        {
            ev_dayTwoEvent.Invoke();
        }
        else if (value == 3)
        {
            ev_dayThreeEvent.Invoke();
        }
        else if (value == 4)
        {
            ev_dayFourEvent.Invoke();
        }
        else if (value == 5)
        {
            ev_dayFiveEvent.Invoke();
        }
        if(value <= dayEventArray.Length)
        {
            dayEventArray[value - 1].Invoke();
        }
        else
        {
            print("Game ends here probably");
        }
        ev_dayHasChanged.Invoke();
    }

    private void SetSkyboxTexture(Texture2D a, Texture2D b)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        
    }
    private void SetSkyboxBlend(float blend)
    {
        RenderSettings.skybox.SetFloat("_Blend", blend);
    }

    private void SetLightBlend(float blend)
    {
        globalLight.color = gradientArray[currentSkyboxIndex].Evaluate(blend);
    }
    
    public int GetDay()
    {
        return Days;
    }

    public int GetMaxDays()
    {
        return maxDays;
    }
    //Connects a given function to a given day
    //eg: ConnectToDayEvent(2, MyMethod);
    public void ConnectToDayEvent(int day_to_connect, UnityAction action_to_connect)
    {
        if(day_to_connect < GetMaxDays())
        {
            dayEventArray[day_to_connect].AddListener(action_to_connect);
        }
        

    }
    //Returns a new array of type T with given length
    T[] InitializeArray<T>(int length) where T : new()
    {
        T[] array = new T[length];
        for (int i = 0; i < length; ++i)
        {
            array[i] = new T();
        }

        return array;
    }


}
