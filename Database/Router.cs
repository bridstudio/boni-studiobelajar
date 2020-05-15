using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class Router : MonoBehaviour
{
    private static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

    public static DatabaseReference User()
    {
      	return baseRef.Child("user");
    }

	public static DatabaseReference UserWithUID(string uid)
	{
		return baseRef.Child("user").Child(uid);
	}

	public static DatabaseReference UserEmailNode(string uid)
	{
		return baseRef.Child("user").Child(uid).Child("email");
	}
	
	public static DatabaseReference UserEmailVerifiedNode(string uid)
	{
		return baseRef.Child("user").Child(uid).Child("emailVerified");
	}

    public static DatabaseReference UserStats()
    {
      	return baseRef.Child("userStats");
    }

	public static DatabaseReference UserStatsWithUID(string uid)
	{
		return baseRef.Child("userStats").Child(uid);
	}

	public static DatabaseReference UserStatsWithDateTime(string uid)
	{
		return baseRef.Child("userStats").Child(uid).Child(UserStatsManager.currHour + ":" + UserStatsManager.currMinute + ":" + UserStatsManager.currSecond + "_" + UserStatsManager.currDay + "-" + UserStatsManager.currMonth + "-" + UserStatsManager.currYear);
	}
}
