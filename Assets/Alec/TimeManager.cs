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

    private float minutes;
    public float Minutes { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    public float minutesThreshold = 5;

    private float hours;
    public float Hours { get { return hours; } set { hours = value; OnHoursChange(value); } }

    public float hoursThreshold = 10;

    private int days;
    public int Days { get { return days; } set { days = value; OnDayChange(value);} }

    public int maxDays = 5;

    private float tempSecond;

    private float secondsCountToday;

    public UnityEvent ev_NightTime;
    public UnityEvent ev_dayOneEvent;
    public UnityEvent ev_dayTwoEvent;
    public UnityEvent ev_dayThreeEvent;
    public UnityEvent ev_dayFourEvent;
    public UnityEvent ev_dayFiveEvent;

    private UnityEvent[] dayEventArray;

    public UnityEvent ev_dayHasChanged;

    public static TimeManager Instance { get { return _instance; } }
    private static TimeManager _instance;

    private bool hasInvokedNight = false;
    private float total_seconds_in_day;
    private float startingSecondsThreshold;
    private float secondsThresholdCoefficient = 0.2f;
    private float secondsUntilTextureChange = 0.0f;
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
        
        startingSecondsThreshold = total_seconds_in_day * 0.2f;

    }
    
    public void Start()
    {
        
        dayChangeText.text = "Day " + days.ToString();
        Days = 1;
        
        SetSkyboxTexture(skyboxArray[currentSkyboxIndex + 1], skyboxArray[currentSkyboxIndex + 1]);
        currentSkyboxIndex = 1;
        //RenderSettings.skybox.set

    }

    public void Update()
    {
        tempSecond += Time.deltaTime;
        secondsCountToday += Time.deltaTime;
        secondsUntilTextureChange += Time.deltaTime;
        total_seconds_in_day = (hoursThreshold * minutesThreshold) * 0.25f;
        if (tempSecond >= 1)
        {
            tempSecond = 0;
            Minutes++;
        }

        SetSkyboxBlend(secondsUntilTextureChange / (total_seconds_in_day));
        SetLightBlend(secondsUntilTextureChange / (total_seconds_in_day));
        if (secondsUntilTextureChange >= total_seconds_in_day )
        {
            //currentSkyboxIndex += 1;
            

            
            if(currentSkyboxIndex == skyboxArray.Length - 1)
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
            //secondsThresholdCoefficient = 0.2f * currentSkyboxIndex + 1;
        }
        
        if (timeText != null)
        {
            timeText.text = days.ToString("00") + ":" + hours.ToString("00") + ":" + minutes.ToString("00");
        }
        
    }

    private void OnMinutesChange(float value)
    {
//Change values to be able to be changed by the designer easily
        globalLight.transform.Rotate(Vector3.up, (1f/1440f)*360f, Space.World);
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

        if (isNight == true && !hasInvokedNight)
        {
            nightText.text = "Night";
            ev_NightTime.Invoke();
            hasInvokedNight = true;
        }
        else
        {
            hasInvokedNight = false;
            nightText.text = "Day";
        }
//Change Value into percentage of Hours
/*
        if (value <= hoursThreshold * .2)
        {
            SetSkyboxTexture(skyboxNight, skyboxSunrise);
            StartCoroutine(LerpLight(gradientNightToSunrise, 1f));
        }
        else if (value <= hoursThreshold * .4)
        {
            SetSkyboxTexture(skyboxSunrise, skyboxDay);
            StartCoroutine(LerpLight(gradientSunriseToDay, 1f));
        }
        else if (value <= hoursThreshold * .6)
        {
            SetSkyboxTexture(skyboxDay, skyboxSunset);
            StartCoroutine(LerpLight(gradientDayToSunset, 1f));
        }
        else if (value <= hoursThreshold * .8)
        {
            SetSkyboxTexture(skyboxSunset, skyboxNight);
            StartCoroutine(LerpLight(gradientSunsetToNight, 1f));
            
        }
*/
    }

    private void OnDayChange(int value)
    {
        secondsCountToday = 0;
        ev_dayHasChanged.Invoke();
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
        if(value <= dayEventArray.Length - 1)
        {
            dayEventArray[value].Invoke();
        }
        else
        {
            print("Game ends here probably");
        }
        
    }

    private void SetSkyboxTexture(Texture2D a, Texture2D b)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        //RenderSettings.skybox.SetFloat("_Blend", 0);
        /*
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            
        }
        */
        //RenderSettings.skybox.SetTexture("_Texture1", b);
    }
    private void SetSkyboxBlend(float blend)
    {
        RenderSettings.skybox.SetFloat("_Blend", blend);
    }

    private void SetLightBlend(float blend)
    {
        globalLight.color = gradientArray[currentSkyboxIndex].Evaluate(blend);
    }
    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
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
