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
    FirebaseAuth auth;    
    
    async void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
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
        FirebaseUser currUser = auth.CurrentUser;
        if(currUser != null)
        {
            CheckEmailVerified();

            Debug.LogFormat("User Signed-In successfully {0} {1}",
                currUser.Email, currUser.UserId);            
        }
        else
        {
            Invoke("LoadAppRegistScene", 3.0f);
        }
    }

    private async void CheckEmailVerified()
    {
        FirebaseUser currUser = auth.CurrentUser;
        try
        {
            if(currUser.IsEmailVerified)
            {            
                await Router.UserEmailVerifiedNode(currUser.UserId).SetValueAsync("Verified");
                Debug.Log("Email Verified");
                
                Invoke("CheckChildName", 2.0f);                
            }
            else if(!currUser.IsEmailVerified)
            {
                Debug.Log("Please verify your email address");
                
                Invoke("CheckChildName", 2.0f);                
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.InnerException.Message);
        }        
    }

    private async void CheckChildName()
    {
        FirebaseUser currUser = auth.CurrentUser;
        try
        {
            var userChildName = await Router.UserChildNameNode(currUser.UserId).GetValueAsync();
            
            if(userChildName.Value.ToString() == "")
            {
                Invoke("LoadAppChildInfoScene", 2.0f);
                SceneManager.LoadScene("App_ChildInfo");
            }
            else
            {
                Invoke("LoadAppHomeScene", 2.0f);
                SceneManager.LoadScene("App_Home");
            }
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.InnerException.Message);
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
    
    private void LoadAppChildInfoScene()
    {
        Debug.Log("Load App_ChildInfo Menu");
        SceneManager.LoadScene("App_ChildInfo");
    }    
}
