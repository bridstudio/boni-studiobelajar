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
        if(UserManager.levelUnlocked == "1")
        {
            levelSelectButton[0].interactable = true;
            levelSelectButton[1].interactable = false;
            levelSelectButton[2].interactable = false;

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, new Parameter(FirebaseAnalytics.ParameterLevel, 1));
        }
        else if(UserManager.levelUnlocked == "2")
        {
            levelSelectButton[0].interactable = true;
            levelSelectButton[1].interactable = true;
            levelSelectButton[2].interactable = false;

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, new Parameter(FirebaseAnalytics.ParameterLevel, 2));
        }
        else if(UserManager.levelUnlocked == "3")
        {
            levelSelectButton[0].interactable = true;
            levelSelectButton[1].interactable = true;
            levelSelectButton[2].interactable = true;

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, new Parameter(FirebaseAnalytics.ParameterLevel, 3));
        }
    }
}
