using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Analytics;
using Firebase.Messaging;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

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
        // PlayGamesPlatform.Activate();
        FirebaseMessaging.TokenReceived += OnTokenReceived;
        FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }

    private void CheckAuthUser()
    {
        FirebaseUser currUser = auth.CurrentUser;
        if(currUser != null)
        {
            CheckEmailVerified();

            // Social.localUser.Authenticate((bool success) =>
            // {
            //     if(success)
            //     {
            //         // ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(gravity.bot);
            //         Debug.Log("Success GooglePlayGames");
            //     }
            // });

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
                await Router.UserEmailVerifiedNode(currUser.UserId).SetValueAsync("Email Terverikasi");
                Debug.Log("Email Verified");
                
                Invoke("CheckChildName", 1.0f);                
            }
            else if(!currUser.IsEmailVerified)
            {
                Debug.Log("Please verify your email address");
                
                Invoke("CheckChildName", 1.0f);                
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
            }
            else
            {
                Invoke("LoadAppSplashGateScene", 2.0f);                
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

    private void LoadAppSplashGateScene()
    {
        Debug.Log("Load AppSplashGate Menu");
        SceneManager.LoadScene("App_SplashGate");
    }
    
    private void LoadAppChildInfoScene()
    {
        Debug.Log("Load App_ChildInfo Menu");
        SceneManager.LoadScene("App_ChildInfo");
    }    
}
