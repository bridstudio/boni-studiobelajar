using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string authType;
    public string email;
    public string emailVerified;
    public string childName;
    public string childAge;
    public string childGender;
    public string levelUnlocked;

    public User()
    {
        authType = UserManager.authType;
        email = UserManager.email;
        emailVerified = UserManager.emailVerified;
        childName = UserManager.childName;
        childAge = UserManager.childAge;
        childGender = UserManager.childGender;
        levelUnlocked = UserManager.levelUnlocked;
    }

    public User(IDictionary<string, object> dict)
    {
        this.authType = dict["authType"].ToString();
        this.email = dict["email"].ToString();
        this.emailVerified = dict["emailVerified"].ToString();
        this.childName = dict["childName"].ToString();
        this.childAge = dict["childAge"].ToString();
        this.childGender = dict["childGender"].ToString();
        this.levelUnlocked = dict["levelUnlocked"].ToString();
    }
}
