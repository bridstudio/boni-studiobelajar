using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class SendStatistics : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
    }

    void Start()
    {
        // DatabaseManager.sharedInstance.GetUserStats(user.UserId);
    }

    public void SendStats()
    {
        SendUserStats(user.UserId);
    }

    private void SendUserStats(string uid)
    {
        UserStats userStats = new UserStats();
        DatabaseManager.sharedInstance.CreateNewUserStats(userStats, uid);
        Debug.Log("Sending UserStats");        
    }
}
