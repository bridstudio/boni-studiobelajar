using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour
{
    public Text dateTime, exitButtonPressed, failedCount, restartButtonPressed, shapeSelected, timerIdle, timerOnTouch, sceneName;

    public void Initialise(UserStats userStats)
    {
        this.dateTime.text = userStats.dateTime;
        this.exitButtonPressed.text = userStats.exitButtonPressed;
        this.failedCount.text = userStats.failedCount;
        this.restartButtonPressed.text = userStats.restartButtonPressed;
        this.shapeSelected.text = userStats.shapeSelected;
        this.timerIdle.text = userStats.timerIdle;
        this.timerOnTouch.text = userStats.timerOnTouch;
        this.sceneName.text = userStats.sceneName;
    }
}
