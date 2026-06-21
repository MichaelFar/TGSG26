using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;

    [SerializeField] private Gradient gradientNightToSunrise;
    [SerializeField] private Gradient gradientSunriseToDay;
    [SerializeField] private Gradient gradientDayToSunset;
    [SerializeField] private Gradient gradientSunsetToNight;
    [SerializeField] private Light globalLight;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI nightText;

    private int minutes;
    public int Minutes { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    private int hours;
    public int Hours { get { return hours; } set { hours = value; OnHoursChange(value); } }

    private int days;
    public int Days { get { return days; } set { days = value; } }

    private float tempSecond;
    public UnityEvent ev_NightTime;
    public UnityEvent dayOneEvent;
    public UnityEvent dayTwoEvent;
    public UnityEvent dayThreeEvent;
    public UnityEvent dayFourEvent;
    public UnityEvent dayFiveEvent;

    public void Update()
    {
        tempSecond += Time.deltaTime;

        if (tempSecond >= 1)
        {
            tempSecond = 0;
            Minutes++;
        }

        if (timeText != null)
        {
            timeText.text = days.ToString("00") + ":" + hours.ToString("00") + ":" + minutes.ToString("00");
        }
        
    }

    private void OnMinutesChange(int value)
    {
        globalLight.transform.Rotate(Vector3.up, (1f/1440f)*360f, Space.World);
        if(value>= 5)
        {
            Minutes = 0;
            Hours++;
        }
        if (Hours >= 10)
        {
            Hours = 0;
            Days++;
            
        }
    }

    private void OnHoursChange(int value)
    {
        bool isNight = value >= 8 || value < 2;

        if (isNight == true)
        {
            nightText.text = "Night";
            Invoke(nameof(ev_NightTime), 1f);
        }
        else
        {
            nightText.text = "Day";
            CancelInvoke(nameof(ev_NightTime));
        }

        if (value == 2)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 1f));
            StartCoroutine(LerpLight(gradientNightToSunrise, 1f));
        }
        else if (value == 4)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 1f));
            StartCoroutine(LerpLight(gradientSunriseToDay, 1f));
        }
        else if (value == 6)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 1f));
            StartCoroutine(LerpLight(gradientDayToSunset, 1f));
        }
        else if (value == 8)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 1f));
            StartCoroutine(LerpLight(gradientSunsetToNight, 1f));
            
        }
    }

    private void OnDayChange(int value)
    {
        if (value == 1)
        {
            Invoke(nameof(dayOneEvent), 1f);
        }
        else if (value == 2)
        {
            Invoke(nameof(dayTwoEvent), 1f);
        }
        else if (value == 3)
        {
            Invoke(nameof(dayThreeEvent), 1f);
        }
        else if (value == 4)
        {
            Invoke(nameof(dayFourEvent), 1f);
        }
        else if (value == 5)
        {
            Invoke(nameof(dayFiveEvent), 1f);
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
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
}
