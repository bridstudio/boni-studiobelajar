using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class DatabaseManager : MonoBehaviour
{
    FirebaseAuth auth;
	public static DatabaseManager sharedInstance = null;	

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
		
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://boni-studiobelajaranak.firebaseio.com/");
		auth = FirebaseAuth.DefaultInstance;
    }	

	public void CreateNewUser(User user, string uid)
	{
		string userJSON = JsonUtility.ToJson(user);
		Router.UserWithUID(uid).SetRawJsonValueAsync(userJSON);
	}

	public void CreateNewUserStats(UserStats userStats, string uid)
	{
		string userStatsJSON = JsonUtility.ToJson(userStats);
		Router.UserStatsWithDateTime(uid).SetRawJsonValueAsync(userStatsJSON);
	}

	public async void GetUserStats(Action<List<UserStats>> completionBlock)
	{
		List<UserStats> tempList = new List<UserStats>();
		try
		{
			DataSnapshot userStats = await Router.UserStatsWithUID(auth.CurrentUser.UserId).GetValueAsync();

			foreach(DataSnapshot userStatsNode in userStats.Children)
			{
				// var values = userStatsNode.Value as Dictionary<string, object>;
				var userStatsDict = (IDictionary<string, object>)userStatsNode.Value;
				// IDictionary dictUserStats = (IDictionary)userStatsNode.Value;
				UserStats newUser = new UserStats(userStatsDict);

				Debug.Log ("Firebase: " + userStatsDict["dateTime"] + " - " + "Firebase: " + userStatsDict["sceneName"] + " - " + "Firebase: " + userStatsDict["exitButtonPressed"] + " - " + userStatsDict["failedCount"] + " - " + userStatsDict["restartButtonPressed"] + " - " + userStatsDict["shapeSelected"] + " - " + userStatsDict["timerIdle"] + " - " + userStatsDict["timerOnTouch"]);

				tempList.Add(newUser);
			}
			completionBlock(tempList);
		}
		catch(Exception ex)
		{
			Debug.Log(ex.InnerException.Message);
		}
	}
}
