using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Analytics;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class AutoLoginManager : MonoBehaviour
{
    private FirebaseAuth auth;
    
    private async void Awake()
    {        
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if(dependencyStatus == DependencyStatus.Available)
        {
            Debug.Log("Firebase Dependencies available");
            auth = FirebaseAuth.DefaultInstance;
            CheckAuthUser();
        }
    }

    private void Start()
    {
        Screen.fullScreen = false;
    }

    private void CheckAuthUser()
    {
        var currUser = auth.CurrentUser;
        if(currUser != null)
        {
            CheckEmailVerified();

            Debug.LogFormat("User Signed-In successfully {0} {1}",
                currUser.Email, currUser.UserId);
            Invoke("LoadAppHomeScene", 3.0f);
        }
        else
        {
            Invoke("LoadAppRegistScene", 3.0f);
        }
    }

    private void CheckEmailVerified()
    {
        var currUser = auth.CurrentUser;
        if(currUser.IsEmailVerified)
        {
            Debug.Log("Email Verified");            
        }
        else
        {
            Debug.Log("Please verify your email address");
        }
    }

    private void LoadAppRegistScene()
    {
        Debug.Log("Load App_Registration Menu");
        SceneManager.LoadScene("App_Registration");
    }

    private void LoadAppHomeScene()
    {
        Debug.Log("Load App_Home Menu");
        SceneManager.LoadScene("App_Home");
    }
}
