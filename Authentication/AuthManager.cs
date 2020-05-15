using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Google;
using Firebase;
using Firebase.Auth;
using Firebase.Analytics;
using MaterialUI;

public class AuthManager : MonoBehaviour
{
    protected string webClientId = "226876476075-lton2h0ut204qp9ofi0895ou4ib1dnhb.apps.googleusercontent.com";
    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    public Text statusTextSignIn, statusTextSignUp, statusTextResetPass;
    public GameObject dialogLoading, dialogEmailVerif, dialogResetPass;
    private DialogBoxConfig _dialogBoxConfig;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestEmail = true,
            RequestIdToken = true
        };
    }        

    public void SignInWithGoogle()
    {
        OnSignInWithGoogle();
    }

    public void SignOutWithGoogle()
    {
        OnSignOutFromGoogle();
    }

    public void SignOutFromEmail()
    {
        OnSignOutFromEmail();
    }

    // Google Sign In
	private void OnSignInWithGoogle()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        UpdateStatusSignIn("Calling SignIn with Google");
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnSignInSilentlyWithGoogle()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        UpdateStatusSignIn("Calling SignIn Silently with Google");
        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    private void OnGamesSignInWithGoogle()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        UpdateStatusSignIn("Calling Games SignIn with Google");
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnSignOutFromGoogle()
    {
        UpdateStatusSignIn("Calling SignOut from Google");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnectFromGoogle()
    {
        UpdateStatusSignIn("Calling Disconnect from Google");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if(task.IsFaulted)
        {
            using(IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if(enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    UpdateStatusSignIn("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    UpdateStatusSignIn("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if(task.IsCanceled)
        {
            UpdateStatusSignIn("Canceled");
        }
        else
        {
            UpdateStatusSignIn("Update to FirebaseAuth with Google");
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    public async void SignInWithGoogleOnFirebase(string idToken)
    {
        try
        {
            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
            if(credential != null)
            {
                FirebaseUser newUser = await auth.SignInWithCredentialAsync(credential);
                UpdateStatusSignIn("User with Google SignIn: " + newUser.Email + " " + newUser.UserId);
                FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);

                // Post to Firebase Database
				UserManager.emailVerified = "Verified";
                UserManager.authType = "Google";
				UserManager.email = newUser.Email;
                PostToDatabase(newUser.UserId);
                SceneManager.LoadScene("App_Home");
            }
            else
            {
                UpdateStatusSignIn("SignIn with Google credential = null!");
            }
        }
        catch(AggregateException ex)
        {
            if(ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                UpdateStatusSignIn("\nError Code= " + inner.ErrorCode + "Message= " + inner.Message);
        }
    }

    public async void SignUpWithEmail(string email, string password)
    {        
        OpenDialogLoading();

        try
        {
            FirebaseUser newUser = await auth.CreateUserWithEmailAndPasswordAsync(email, password);                        
            Debug.LogFormat("User Signed Up Successfully: {0} ({1})",
                newUser.Email, newUser.UserId);            
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSignUp);

            CloseDialogLoading();            
            
            // Send email verification to new user
            SendEmailVerification();            
        }
        catch(Exception ex)
        {
            UpdateStatusSignUp(ex.InnerException.Message);
            CloseDialogLoading();
        }
    }

    public async void SignInWithEmail(string email, string password)
    {
        OpenDialogLoading();

        try
        {
            FirebaseUser newUser = await auth.SignInWithEmailAndPasswordAsync(email, password);
            Debug.LogFormat("User Signed In Successfully: {0} ({1})",
                newUser.Email, newUser.UserId);
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);

            CloseDialogLoading();

            // Load scene
            UpdateStatusSignIn("Loading scene...");
            SceneManager.LoadScene("App_Home");
        }
        catch(Exception ex)
        {
            UpdateStatusSignIn(ex.InnerException.Message);
            CloseDialogLoading();
        }
    }    

    private async void SendEmailVerification()
    {
        try
        {
            var currUser = auth.CurrentUser;
            if(currUser != null)
            {
                await currUser.SendEmailVerificationAsync();
                OpenDialogEmailVerif();

                UpdateStatusSignUp("Email verification sent to: " + currUser.Email);                

                // Post to Database
                UserManager.emailVerified = "Unverified";
				UserManager.authType = "Email";
				UserManager.email = currUser.Email;
                PostToDatabase(currUser.UserId);

                // Load scene
                UpdateStatusSignUp("Loading scene...");
                // SceneManager.LoadScene("App_Home");
            }
            else
            {
                UpdateStatusSignUp("Send Email verification failed, please Sign Up again.");
                CloseDialogEmailVerif();
            }
        }
        catch(Exception ex)
        {
            UpdateStatusSignUp(ex.InnerException.Message);
            CloseDialogEmailVerif();
        }
    }

    private void CheckEmailVerified()
    {
        var currUser = auth.CurrentUser;
        if(currUser.IsEmailVerified)
        {
            // Router.UsersEmailVerifiedNode(currUser.UserId).SetValueAsync("Verified");
        }
        else
        {
            UpdateStatusSignIn("Please verify your email address");
        }
    }

    public async void ResetPasswordWithEmail(string email)
    {
        OpenDialogLoading();

        try
        {            
            await auth.SendPasswordResetEmailAsync(email);
            CloseDialogLoading();            

            UpdateStatusResetPass("Password Reset Email has been sent, please check your email");
            OpenDialogResetPass();
        }
        catch(Exception ex)
        {
            UpdateStatusResetPass("Wrong Email Address");
            CloseDialogLoading();
            Debug.Log(ex.InnerException.Message);            
        }
    }

	// Sign Out from Email
	private void OnSignOutFromEmail()
    {
        Debug.Log("Logout a user");
        auth.SignOut();
        
        // Load scene
        Debug.Log("Loading App_Regist scene...");
        SceneManager.LoadScene("App_Splash");
    }

    // Database
	private void PostToDatabase(string uid)
    {
        User user = new User();
        DatabaseManager.sharedInstance.CreateNewUser(user, uid);
    }    

	public void OpenDialogLoading()
    {        
        dialogLoading.GetComponent<DialogBoxConfig>().Open();
    }

    public void CloseDialogLoading()
    {
        dialogLoading.GetComponent<DialogBoxConfig>().Close();        
    }

    public void OpenDialogEmailVerif()
    {
        dialogEmailVerif.GetComponent<DialogBoxConfig>().Open();
    }

    public void CloseDialogEmailVerif()
    {
        dialogEmailVerif.GetComponent<DialogBoxConfig>().Close();
    }

    public void OpenDialogResetPass()
    {
        dialogResetPass.GetComponent<DialogBoxConfig>().Open();
    }

    public void CloseDialogResetPass()
    {
        dialogResetPass.GetComponent<DialogBoxConfig>().Close();
    }

    private void UpdateStatusSignIn(string message)
    {
        Debug.Log(message);
        statusTextSignIn.text = message;
    }

    private void UpdateStatusSignUp(string message)
    {
        Debug.Log(message);
        statusTextSignUp.text = message;
    }

    private void UpdateStatusResetPass(string message)
    {
        Debug.Log(message);
        statusTextResetPass.text = message;
    }
}