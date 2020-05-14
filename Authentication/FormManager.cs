using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Analytics;

public class FormManager : MonoBehaviour
{
    public InputField signUpEmailInput;
    public InputField signUpPasswordInput = null;
    public InputField signInEmailInput, resetPasswordEmailInput;
    public InputField signInPasswordInput = null;
    public Button signUpButton, signInButton, signUpShowPassButton, signInShowPassButton, nextButton, resetPassButton, policyButton;
    public AuthManager authManager;

    void Start()
    {
        signUpButton.onClick.AddListener(() => OnSignUpWithEmail());
        signInButton.onClick.AddListener(() => OnSignInWithEmail());
        resetPassButton.onClick.AddListener(() => OnResetPasswordWithEmail());
        policyButton.onClick.AddListener(() => SceneManager.LoadScene("App_Policy"));
        
        signUpShowPassButton.onClick.AddListener(() => ToggleSignUpPasswordInputType());
        signInShowPassButton.onClick.AddListener(() => ToggleSignInPasswordInputType());
    }

    // Update is called once per frame
    void Update()
    {
        ValidateEmailSignUp();
        ValidateEmailSignIn();
        ValidatePasswordSignUp();
        ValidateEmailResetPassword();
    }

    public void OnSignUpWithEmail()
    {
        authManager.SignUpWithEmail(signUpEmailInput.text, signUpPasswordInput.text);
        Debug.Log("Sign Up With Email");
    }

    public void OnSignInWithEmail()
    {
        authManager.SignInWithEmail(signInEmailInput.text, signInPasswordInput.text);
        Debug.Log("Sign In With Email");
    }

    public void OnResetPasswordWithEmail()
    {
        authManager.ResetPasswordWithEmail(resetPasswordEmailInput.text);
        Debug.Log("Reset Password With Email");
    }

    private void ValidateEmailSignUp()
    {
        string email = signUpEmailInput.text;        
        var regexPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
		    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
                
        if(email != "" && Regex.IsMatch(email, regexPattern))
        {
            ToggleNextButtonStates(true);
        }
        else
        {
            ToggleNextButtonStates(false);
        }        
    }

    private void ValidatePasswordSignUp()
    {
        string password = signUpPasswordInput.text;
        if(password.Length >= 6)
        {
            ToggleSignUpButtonStates(true);
        }
        else
        {
            ToggleSignUpButtonStates(false);
        }
    }

    private void ValidateEmailSignIn()
    {
        string email = signInEmailInput.text;
        string password = signInPasswordInput.text;
        var regexPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
		    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        
        if(password != "" && email != "" && Regex.IsMatch(email, regexPattern))
        {
            ToggleSignInButtonStates(true);
        }
        else
        {
            ToggleSignInButtonStates(false);
        }
    }

    private void ValidateEmailResetPassword()
    {
        string email = resetPasswordEmailInput.text;        
        var regexPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
		    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        
        if(email != "" && Regex.IsMatch(email, regexPattern))
        {
            ToggleResetButtonStates(true);
        }
        else
        {
            ToggleResetButtonStates(false);
        }
    }

    private void ToggleSignUpButtonStates(bool toState)
    {
        signUpButton.interactable = toState;        
    }

    private void ToggleSignInButtonStates(bool toState)
    {        
        signInButton.interactable = toState;
    }

    private void ToggleNextButtonStates(bool toState)
    {        
        nextButton.interactable = toState;
    }

    private void ToggleResetButtonStates(bool toState)
    {        
        resetPassButton.interactable = toState;
    }

    public void ToggleSignUpPasswordInputType() {
        if (signUpPasswordInput != null) {
            if (signUpPasswordInput.contentType == InputField.ContentType.Password) {
                signUpPasswordInput.contentType = InputField.ContentType.Standard;
            } else {
                signUpPasswordInput.contentType = InputField.ContentType.Password;
            }
            signUpPasswordInput.ForceLabelUpdate ();
        }
    }

    public void ToggleSignInPasswordInputType() {
        if (signInPasswordInput != null) {
            if (signInPasswordInput.contentType == InputField.ContentType.Password) {
                signInPasswordInput.contentType = InputField.ContentType.Standard;
            } else {
                signInPasswordInput.contentType = InputField.ContentType.Password;
            }
            signInPasswordInput.ForceLabelUpdate ();
        }
    }

    public static void UpdateStatus(string message)
    {
        //statusText.text = message;
        Debug.Log(message);
    }
}
