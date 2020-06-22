using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Analytics;
using MaterialUI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class SendChildInfo : MonoBehaviour
{
    private string childAgeString = "", childGenderString = "";
    public InputField inputChildName;
    public Button[] buttonChildAge, buttonChildGender;        
    public Image[] buttonChildAgeColor, buttonChildGenderColor;
    public Text[] textChildAgeColor, textChildGenderColor;
    public Button buttonNextChildName, buttonSubmitChildInfo;
    public GameObject dialogLoading;    
    FirebaseAuth auth;
    FirebaseUser user;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;

        buttonSubmitChildInfo.onClick.AddListener(() => SendChildInformation());
        
        buttonChildAge[0].onClick.AddListener(() => OnSubmitChildAge(0));
        buttonChildAge[1].onClick.AddListener(() => OnSubmitChildAge(1));
        buttonChildAge[2].onClick.AddListener(() => OnSubmitChildAge(2));
        buttonChildAge[3].onClick.AddListener(() => OnSubmitChildAge(3));
        
        buttonChildGender[0].onClick.AddListener(() => OnSubmitChildGender(0));
        buttonChildGender[1].onClick.AddListener(() => OnSubmitChildGender(1));
    }

    void Update()
    {
        ValidateNameInput();        
        ValidateSubmitButton();
    }

    public void OpenDialogLoading()
    {        
        dialogLoading.SetActive(true);
        dialogLoading.GetComponent<DialogBoxConfig>().Open();
    }

    public void CloseDialogLoading()
    {
        dialogLoading.SetActive(false);
        dialogLoading.GetComponent<DialogBoxConfig>().Close();        
    }

    private void ValidateNameInput()
    {
        string name = inputChildName.text;
        if(name.Length >= 3)
        {
            buttonNextChildName.interactable = true;
        }
        else if(name.Length < 3)
        {
            buttonNextChildName.interactable = false;
        }
    }    

    private void ValidateSubmitButton()
    {
        if(childAgeString == "" && childGenderString == "")
        {
            buttonSubmitChildInfo.interactable = false;
        }
        else if(childAgeString != "" && childGenderString != "")
        {
            buttonSubmitChildInfo.interactable = true;
        }
    }

    private async void SendChildInformation()
    {
        try
        {
            OpenDialogLoading();            
            
            await Router.UserWithUID(user.UserId).Child("childName").SetValueAsync(inputChildName.text);
            await Router.UserWithUID(user.UserId).Child("childAge").SetValueAsync(childAgeString);
            await Router.UserWithUID(user.UserId).Child("childGender").SetValueAsync(childGenderString);                        
            
            // Social.ReportProgress("CgkIq5WRl80GEAIQAQ", 100.0f, (bool success) =>
            // {
            //     Debug.Log("Success Google Play Games 'Welcome' ");
            // });
            
            CloseDialogLoading();

            SceneManager.LoadScene("App_Splash");
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.InnerException.Message);
        }
    }

    private void OnSubmitChildAge(int childAge)
    {
        if(childAge == 0)
        {
            childAgeString = "2";
        }
        else if(childAge == 1)
        {
            childAgeString = "3";
        }
        else if(childAge == 2)
        {
            childAgeString = "4";
        }
        else if(childAge == 3)
        {
            childAgeString = "5";
        }

        for (int i = 0; i < buttonChildAgeColor.Length; i++)
        {
            if(i == childAge)
            {
                buttonChildAgeColor[i].color = new Color32(0, 117, 219, 255);
                textChildAgeColor[i].color = new Color32(255, 255, 255, 255);
            }
            else
            {
                buttonChildAgeColor[i].color = new Color32(255, 255, 255, 255);
                textChildAgeColor[i].color = new Color32(55, 71, 79, 255);
            }
        }
    }
    
    private void OnSubmitChildGender(int childGender)
    {
        if(childGender == 0)
        {
            childGenderString = "Boy";            
        }
        else if(childGender == 1)
        {
            childGenderString = "Girl";
        }        

        for (int i = 0; i < buttonChildGenderColor.Length; i++)
        {
            if(i == childGender)
            {
                buttonChildGenderColor[i].color = new Color32(0, 117, 219, 255);
                textChildGenderColor[i].color = new Color32(255, 255, 255, 255);
            }
            else
            {
                buttonChildGenderColor[i].color = new Color32(255, 255, 255, 255);
                textChildGenderColor[i].color = new Color32(55, 71, 79, 255);
            }
        }
    }
}
