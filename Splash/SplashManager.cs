using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;

public class SplashManager : MonoBehaviour
{
    FirebaseAuth auth;

    private async void Awake()
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

    private void CheckAuthUser()
    {
        FirebaseUser currUser = auth.CurrentUser;
        if(currUser != null)
        {            
            Invoke("CheckChildName", 1.0f);
        }
        else
        {
            Invoke("LoadAppRegistScene", 3.0f);
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
                Invoke("LoadAppHomeScene", 2.0f);                
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
        Debug.Log("Load App_HomeScene Menu");
        SceneManager.LoadScene("App_Home");
    }

    private void LoadAppChildInfoScene()
    {
        Debug.Log("Load App_ChildInfo Menu");
        SceneManager.LoadScene("App_ChildInfo");
    }    
}
