using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserStatsManager : MonoBehaviour
{
    Scene m_scene;
    DateTime m_currentDate = DateTime.Now;

    public static string exitButtonPressed, failedCount, restartButtonPressed, shapeSelected, timerIdle, timerOnTouch;
    public static string sceneName, dateTime, clockTime, currHour, currMinute, currSecond, currDay, currMonth, currYear;

    void Start()
    {
        m_scene = SceneManager.GetActiveScene();
        sceneName = m_scene.name.ToString();
        
        currDay = m_currentDate.Day.ToString();
        currMonth = m_currentDate.Month.ToString();
        currYear = m_currentDate.Year.ToString();
        currHour = m_currentDate.Hour.ToString();
        currMinute = m_currentDate.Minute.ToString();
        currSecond = m_currentDate.Second.ToString();

        dateTime = currDay + "-" + currMonth + "-" + currYear;
        clockTime = currHour + ":" + currMinute + ":" + currSecond; 
        timerOnTouch = "";
        timerIdle = "";
        shapeSelected = "";
        exitButtonPressed = "";
        restartButtonPressed = "";
        failedCount = "";
    }
}
