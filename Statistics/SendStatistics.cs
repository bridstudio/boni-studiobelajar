using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Firebase.Auth;

public class SendStatistics : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;
    private string childAge, childGender, sceneName;
    private string exitButtonPressed, failedCount, restartButtonPressed, shapeSelected, timerIdle, timerOnTouch;
    private readonly string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScCbef4b63ABhW0_wQ1iK67aIiOba9d1PNDDkIYRmDZxS6GNA/formResponse";

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
    }    

    public void SendStats()
    {
        SendUserStats(user.UserId);
    }

    private void SendUserStats(string uid)
    {
        try
        {
            UserStats userStats = new UserStats();
            DatabaseManager.sharedInstance.CreateNewUserStats(userStats, uid);
            Debug.Log("Sending UserStats");

            UpdateLevel();
            SendToGoogleForms();
        }
        catch(Exception ex)
        {
            Debug.Log(ex.InnerException.Message);
        }        
    }

    private void UpdateLevel()
    {
        if(UserStatsManager.sceneName == "GameColoring")
        {
            Router.UserWithUID(user.UserId).Child("levelUnlocked").SetValueAsync("2");
        }
        else if(UserStatsManager.sceneName == "Drag-N-Drop")
        {
            Router.UserWithUID(user.UserId).Child("levelUnlocked").SetValueAsync("3");
        }
        else if(UserStatsManager.sceneName == "Game")
        {
            Router.UserWithUID(user.UserId).Child("levelUnlocked").SetValueAsync("4");
        }
    }

    private void SendToGoogleForms()
    {
        exitButtonPressed = UserStatsManager.exitButtonPressed;
        failedCount = UserStatsManager.failedCount;
        restartButtonPressed = UserStatsManager.restartButtonPressed;
        shapeSelected = UserStatsManager.shapeSelected;
        timerIdle = UserStatsManager.timerIdle;
        timerOnTouch = UserStatsManager.timerOnTouch;
        
        sceneName = UserStatsManager.sceneName;
        childAge = UserManager.childAge;
        childGender = UserManager.childGender;

        StartCoroutine(PostToGoogleForms(exitButtonPressed, failedCount, restartButtonPressed, shapeSelected, timerIdle, timerOnTouch, sceneName, childAge, childGender));
    }

    IEnumerator PostToGoogleForms(string exitButtonPressed, string failedCount, string restartButtonPressed, string shapeSelected, string timerIdle, string timerOnTouch, string sceneName, string childAge, string childGender)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1198210276", exitButtonPressed);
        form.AddField("entry.1501425783", failedCount);
        form.AddField("entry.363537418", restartButtonPressed);
        form.AddField("entry.1057042240", shapeSelected);
        form.AddField("entry.1746855575", timerIdle);
        form.AddField("entry.177024826", timerOnTouch);
        form.AddField("entry.1132375022", sceneName);
        form.AddField("entry.374704517", childAge);
        form.AddField("entry.2096971648", childGender);

        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        // UnityWebRequest www = new UnityWebRequest(BASE_URL, rawData.ToString());
        yield return www;
    }
}
