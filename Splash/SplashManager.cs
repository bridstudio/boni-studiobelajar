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
            Invoke("LoadAppHomeScene", 3.0f);            
        }
        else
        {
            Invoke("LoadAppRegistScene", 3.0f);
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
}
