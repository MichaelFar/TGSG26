using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;

    private int minutes;
    public int Minutes { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    private int hours;
    public int Hours { get { return hours; } set { hours = value; OnHoursChange(value); } }

    private int days;
    public int Days { get { return days; } set { days = value; } }

    private float tempSecond;

    public void Update()
    {
        tempSecond += Time.deltaTime;

        if (tempSecond >= 1)
        {
            tempSecond = 0;
            Minutes++;
        }
        
    }

    private void OnMinutesChange(int value)
    {
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
        if (value == 2)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 10f));
        }
        else if (value == 5)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
        }
        else if (value == 8)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
        }
        else if (value == 10)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
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
}
