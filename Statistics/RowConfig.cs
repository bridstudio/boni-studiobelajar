using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour
{
    public Text dateTime, clockTime, exitButtonPressed, failedCount, restartButtonPressed, shapeSelected, timerIdle, timerOnTouch, sceneName;
    public Image tabImage;

    public void Initialise(UserStats userStats)
    {
        this.dateTime.text = userStats.dateTime;
        this.clockTime.text = userStats.clockTime;
        this.exitButtonPressed.text = userStats.exitButtonPressed;
        this.failedCount.text = userStats.failedCount;
        this.restartButtonPressed.text = userStats.restartButtonPressed;
        this.shapeSelected.text = userStats.shapeSelected;
        this.timerIdle.text = userStats.timerIdle;
        this.timerOnTouch.text = userStats.timerOnTouch;
        this.sceneName.text = userStats.sceneName;        
    }

    void Start()
    {
        ChangeTabColor();
    }

    public void ChangeTabColor()
    {
        if(this.sceneName.text == "GameColoring")
        {
            tabImage.color = new Color32(0, 117, 219, 255);
        }
        else if(this.sceneName.text == "Drag-N-Drop")
        {
            tabImage.color = new Color32(109, 180, 50, 255);
        }
        else if(this.sceneName.text == "Game")
        {
            tabImage.color = new Color32(229, 85, 55, 255);
        }
    }
}
