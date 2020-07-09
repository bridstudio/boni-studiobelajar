using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Analytics;

public class LevelSelectManager : MonoBehaviour
{
    public Button[] levelSelectButton;
    public Text childNameText, unitText;

    void Start()
    {
        Screen.fullScreen = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        
        ChildInfo();
        UnlockLevel();
    }

    private void ChildInfo()
    {
        childNameText.text = UserManager.childName;
        // unitText.text = UserManager.levelUnlocked;
    }

    private void UnlockLevel()
    {
        if(UserManager.levelUnlocked != "0")
        {
            levelSelectButton[0].interactable = true;
            levelSelectButton[1].interactable = true;
            levelSelectButton[2].interactable = true;

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, new Parameter(FirebaseAnalytics.ParameterLevel, 1));
        }        
    }
}
