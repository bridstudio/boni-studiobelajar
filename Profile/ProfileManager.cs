using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class ProfileManager : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;
    public Text email, emailVerified, childName, childAge, childGender, levelUnlocked;
    public Button signOutButton;
    private string authType;
    public Image authImage;
    public Sprite emailIcon, googleIcon;
    
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
        signOutButton.onClick.AddListener(() => OnSignOutUser());
        
        ProfileInfo();
    }    

    public void ProfileInfo()
    {
        authType = UserManager.authType;
        email.text = UserManager.email;
        emailVerified.text = UserManager.emailVerified;
        childName.text = UserManager.childName;
        childAge.text = UserManager.childAge + " years old";
        childGender.text = UserManager.childGender;
        levelUnlocked.text = UserManager.levelUnlocked;

        if(authType == "Google")
        {
            authImage.sprite = googleIcon;
        }
        else
        {
            authImage.sprite = emailIcon;
        }
    }    

    public void OnSignOutUser()
    {
        ClearUserManagerValue();

        Debug.Log("Logout a user");
        auth.SignOut();
        
        // Load scene
        Debug.Log("Loading App_Regist scene...");
        SceneManager.LoadScene("App_Splash");
    }

    public void ClearUserManagerValue()
    {
        UserManager.email = "";
        UserManager.emailVerified = "";
        UserManager.childName = "";
        UserManager.childAge = "";
        UserManager.childGender = "";
        UserManager.levelUnlocked = "";
    }
}
