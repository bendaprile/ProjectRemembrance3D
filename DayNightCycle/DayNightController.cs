using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DayNightController : MonoBehaviour
{

    [Range(0, 24)] public float timeOfDay;
    public Light sun;
    public Light moon;
    public float dayOrbitSpeed = 0.5f;
    public float nightOrbitSpeed = 0.8f;

    private bool isNight;

    // Update is called once per frame
    void Update()
    {

        if (timeOfDay < 5.5 || timeOfDay > 18.8)
        {
            timeOfDay += Time.deltaTime * nightOrbitSpeed;
        }
        else
        {
            timeOfDay += Time.deltaTime * dayOrbitSpeed;
        }

        if (timeOfDay > 24)
        {
            timeOfDay = 0;
        }

        UpdateTime();
    }


    private void OnValidate()
    {
        UpdateTime();
    }


    //TODO: Add functionality to make sun darker at dawn and dusk and then get
    // brighter throughout the day. 
    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180f;

        sun.transform.rotation = Quaternion.Euler(sunRotation, -45.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -45.0f, 0);

        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180f)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180f)
            {
                StartNight();
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
        moon.shadows = LightShadows.None;
        sun.shadows = LightShadows.Soft;
    }

    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
    }
}
