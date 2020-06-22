using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashGateManager : MonoBehaviour
{
    public GameObject userManager;
    public Text childNameText;
    public Button kidsPlayButton, parentsModeButton;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        
        GameObject newUserManager = Instantiate(userManager) as GameObject;
        kidsPlayButton.onClick.AddListener(() => LoadAppPlay());
        parentsModeButton.onClick.AddListener(() => LoadParentalGatePortrait());

        UserProfileInfo();
    }

    private void UserProfileInfo()
    {
        childNameText.text = UserManager.childName;
    }

    private void LoadAppPlay()
    {
        SceneManager.LoadScene("App_Play");
    }

    private void LoadParentalGatePortrait()
    {
        SceneManager.LoadScene("App_ParentalGatePortrait");
    }
}
