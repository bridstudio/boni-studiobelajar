using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStats
{
    public string dateTime, clockTime;
    public string exitButtonPressed;
    public string failedCount;
    public string restartButtonPressed;
    public string shapeSelected;
    public string timerIdle;
    public string timerOnTouch;
    public string sceneName;

    public UserStats()
    {
        dateTime = UserStatsManager.dateTime;
        clockTime = UserStatsManager.clockTime;
        sceneName = UserStatsManager.sceneName;
        exitButtonPressed = UserStatsManager.exitButtonPressed;
        failedCount = UserStatsManager.failedCount;
        restartButtonPressed = UserStatsManager.restartButtonPressed;
        shapeSelected = UserStatsManager.shapeSelected;
        timerIdle = UserStatsManager.timerIdle;
        timerOnTouch = UserStatsManager.timerOnTouch;
    }

    public UserStats(IDictionary<string, object> dict)
    {
        this.dateTime = dict["dateTime"].ToString();
        this.clockTime = dict["clockTime"].ToString();
        this.sceneName = dict["sceneName"].ToString();
        this.exitButtonPressed = dict["exitButtonPressed"].ToString();
        this.failedCount = dict["failedCount"].ToString();
        this.restartButtonPressed = dict["restartButtonPressed"].ToString();
        this.shapeSelected = dict["shapeSelected"].ToString();
        this.timerIdle = dict["timerIdle"].ToString();
        this.timerOnTouch = dict["timerOnTouch"].ToString();        
    }
}
