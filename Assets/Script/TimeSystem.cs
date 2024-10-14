using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeSystem : MonoBehaviour
{

    List<string> days_of_week = new List<string>(new string[] { "Sun.", "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." });
    List<string> seasons = new List<string>(new string[] { "Spring", "Summer", "Fall", "Winter" });
    List<string> minute_intervals = new List<string>(new string[] { "00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55" });

    public int seconds = 0;
    public int minutes = 0;
    public int hours = 22;
    public int days = 0;
    public int months = 0;
    public int years = 0;
    public string weekday;
    public int weekday_counter = 0;
    public string season = "Spring";
    public string splitter = "AM";
    public string minute_interval = "00";

    public int timeSpeed = 200;

    public static event Action Daypassed;

    public TextMeshProUGUI hoursUi;
    public TextMeshProUGUI dayUi;
    public Gradient gradient;
    public Light2D light2D;

    private const int dayStartHour = 6;
    private const int dayEndHour = 2;

    // Start is called before the first frame update
    void Start()
    {
        weekday = days_of_week[0];
    }

    // Update is called once per frame
    void Update()
    {
        advanceTime();
    }

    private void startNewDay()
    {
        Daypassed.Invoke();
    }


    private void advanceTime()
    {
        // Calculate how many seconds to advance based on Time.deltaTime and time speed
        int secondsToAdvance = Mathf.FloorToInt(Time.deltaTime * timeSpeed);

        // Advance seconds by the calculated amount
        seconds += secondsToAdvance;

        // Update minutes and hours based on the total seconds
        UpdateMinutesAndHours();

        updateUi();
        setLight();
    }

    private void UpdateMinutesAndHours()
    {
        // Update minutes and wrap seconds
        minutes += seconds / 60;
        seconds %= 60;

        // Update hours and wrap minutes
        hours += minutes / 60;
        minutes %= 60;
        // Handle wrapping the hours between 6 AM and 2 AM
        if (hours >= 24)
        {
            hours = 0;  // Reset hours when we go past 24 (end of the day)
        }

        if (hours >= dayEndHour && hours < dayStartHour)
        {
            hours = dayStartHour;  // Reset to 6 AM if we pass 2 AM
            days++;  // Move to the next day
            startNewDay();
        }
    }

    private void updateUi()
    {
        // Formatted output
        if (hours > 12)
        {
            splitter = "PM";
        } else
        {
            splitter = "AM";
        }
        string currentTime = $"{hours:D2}:{RoundMinutesToNearest5(minutes):D2} {splitter}";

        string currentDay = $"{days_of_week[days % 7]}";

        hoursUi.SetText(currentTime);
        dayUi.SetText(currentDay);
    }

    private void setLight()
    {
        light2D.color = gradient.Evaluate(ConvertHourToNormalized(hours));
    }
    
    private float ConvertHourToNormalized(float currentHour)
    {
        // If the time is from 6 AM to 11:59 PM (6 AM to Midnight), we subtract 6
        if (currentHour >= 6)
        {
            currentHour -= 6;
        }
        // If the time is from Midnight (12 AM) to 2 AM, we add 18 to shift it properly
        else
        {
            currentHour += 18;
        }

        // Normalize by dividing by 20 (since the day lasts 20 hours from 6 AM to 2 AM)
        return currentHour / 20f;
    }

    private int RoundMinutesToNearest5(int minutes)
    {
        if (minutes > 55)
        {
            return 55;
        }
        return Mathf.RoundToInt(minutes / 5f) * 5;
    }
}
