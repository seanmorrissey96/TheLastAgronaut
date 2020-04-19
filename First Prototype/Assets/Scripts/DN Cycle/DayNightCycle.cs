using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float targetDayLength = 0.5f;
    [SerializeField] private float elapsedTime;
    [SerializeField] private bool use24clock = true;
    [SerializeField] private Text clockText;
    [SerializeField] private Text dayNumberText;
    [SerializeField] [Range(0f, 1f)] public float timeOfDay;
    [SerializeField] public static int dayNumber = 1;
    [SerializeField] private Transform dailyRotation;
    [SerializeField] private Light sun;
    [SerializeField] private float intensity;
    [SerializeField] private float sunBaseIntensity = 1f;
    [SerializeField] private float sunVariation = 1.5f;
    [SerializeField] private Gradient sunColor;
    [SerializeField] private AudioSource dSounds;
    [SerializeField] private AudioSource nSounds;
    [SerializeField] private bool isMorning;
    [SerializeField] private bool isNight;

    [SerializeField] private AudioClip morningSounds;
    [SerializeField] private AudioClip nightSounds;

    [SerializeField] private float spaceshipCue = 0.8f;
    [SerializeField] private AudioSource spaceshipSound;

    [SerializeField] private AudioSource eerySound;
    [SerializeField] private AudioClip eeryClip;
    [SerializeField] private bool isDay2;

    public event EventHandler SpaceshipMove;

    private List<DNModuleBase> moduleList = new List<DNModuleBase>();

    private float timeScale = 100f;
    public bool pause = false;

    public float TargetDayLength
    { get { return targetDayLength; }}
    public float TimeOfDay
    { get { return timeOfDay; }}
    public int DayNumber 
    { get { return dayNumber;}}

    private void Start()
    {
        isMorning = false;
        isNight = false;

        //Starting/wake up time - 8AM
        timeOfDay = 0.33f;
        elapsedTime = 20f;
    }

    private void Update()
    {
        if (!isDay2 && dayNumber == 2)
        {
            isDay2 = true;
            eerySound.PlayOneShot(eeryClip);
        }

        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
            UpdateDayNumberText();
            UpdateClock();
        }

        AdjustSunRotation();
        SunIntensity();
        AdjustSunColor();
        UpdateModules();
        MoveSpaceship();

        if (!spaceshipSound.isPlaying && timeOfDay > spaceshipCue)
        {
            spaceshipSound.Play();
        }

        if (!isMorning && timeOfDay > 0.2 && timeOfDay < 0.5)
        {
            dSounds.PlayOneShot(morningSounds);
            isMorning = true;
        }
        else if (timeOfDay > 0.5)
        {
            dSounds.Stop();
            isMorning = false;
        }

        if (!isNight && timeOfDay > 0.75)
        {
            nSounds.PlayOneShot(nightSounds);
            isNight = true;
        }

        else if (timeOfDay >= 0.99)
        {
            nSounds.Stop();
            isNight = false;
            pause = true;
        }
    }

    private void UpdateTimeScale()
    {
        timeScale = 24 / (targetDayLength / 60);
    }

    private void UpdateTime()
    {
        timeOfDay += Time.deltaTime * timeScale / 86400; // seconds in a day
        elapsedTime += Time.deltaTime;
        if (timeOfDay > 1) //new day
        {
            elapsedTime = 0;
            dayNumber++;
            timeOfDay -= 1;
        }
    }

    private void UpdateClock()
    {
        float time = elapsedTime / (targetDayLength * 60);
        float hour = Mathf.FloorToInt(time * 24);
        float minute = Mathf.FloorToInt(((time * 24) - hour) * 60);

        string hourString;
        string minuteString;

        if (!use24clock && hour > 12)
        {
            hour -= 12;
        }

        if (hour < 10)
        {
            hourString = "0" + hour.ToString();
        }
        else
        {
            hourString = hour.ToString();
        }

        if (minute < 10)
        {
            minuteString = "0" + minute.ToString();
        }
        else
        {
            minuteString = minute.ToString();
        }

        if (use24clock)
        {
            clockText.text = hourString + " : " + minuteString;
        }
        else if (time > 0.5f)
        {
            clockText.text = hourString + " : " + minuteString + " pm";
        }
        else
        {
            clockText.text = hourString + " : " + minuteString + " am";
        }

        
    }

    private void UpdateDayNumberText()
    {
        dayNumberText.text = "Day " + dayNumber.ToString();
    }

    private void AdjustSunRotation()
    {
        float sunAngle = timeOfDay * 360f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));
    }

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }

    private void AdjustSunColor()
    {
        sun.color = sunColor.Evaluate(intensity);
    }

    public void AddModule(DNModuleBase module)
    {
        moduleList.Add(module);
    }

    public void RemoveModule(DNModuleBase module)
    {
        moduleList.Remove(module);
    }

    private void UpdateModules()
    {
        foreach (DNModuleBase module in moduleList)
        {
            module.UpdateModule(intensity);
        }
    }

    private void MoveSpaceship()
    {
        if (timeOfDay > spaceshipCue)
        {
            SpaceshipMove?.Invoke(this, EventArgs.Empty);
        }
    }
}