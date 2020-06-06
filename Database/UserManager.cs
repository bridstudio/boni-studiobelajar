using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using SimpleJSON;

public class UserManager : MonoBehaviour
{
    public static string authType, email, emailVerified, childName, childAge, childGender, levelUnlocked;
    public static UserManager sharedInstance = null;
    FirebaseAuth auth;

    void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if(sharedInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        auth = FirebaseAuth.DefaultInstance;
    }

    void Start()
    {
        DynamicUserValueChanged();
    }

    public void DynamicUserValueChanged()
    {
        FirebaseUser user = auth.CurrentUser;

        Router.UserWithUID(user.UserId).ValueChanged += HandleUserValueChanged;
    }

    void HandleUserValueChanged(object sender, ValueChangedEventArgs args)
    {
        if(args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message);
            return;
        }

        if(args.Snapshot != null && args.Snapshot.ChildrenCount > 0)
        {
            JSONNode node = JSON.Parse(args.Snapshot.GetRawJsonValue());

            UserManager.authType = node["authType"].Value.ToString();
            UserManager.email = node["email"].Value.ToString();
            UserManager.emailVerified = node["emailVerified"].Value.ToString();
            UserManager.childName = node["childName"].Value.ToString();
            UserManager.childAge = node["childAge"].Value.ToString();
            UserManager.childGender = node["childGender"].Value.ToString();
            UserManager.levelUnlocked = node["levelUnlocked"].Value.ToString();

            Debug.Log(email + " " + emailVerified + " " + childName + " " + childAge + " " + childGender + " " + levelUnlocked);
        }
    }
}
