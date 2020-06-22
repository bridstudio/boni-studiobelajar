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
        // FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
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
		Router.UserStatsWithUID(uid).Push().SetRawJsonValueAsync(userStatsJSON);
	}

	public async void GetUserStats(Action<List<UserStats>> completionBlock)
	{
		List<UserStats> tempList = new List<UserStats>();
		try
		{
			DataSnapshot userStats = await Router.UserStatsWithUID(auth.CurrentUser.UserId).GetValueAsync();

			foreach(DataSnapshot userStatsNode in userStats.Children)
			{				
				var userStatsDict = (IDictionary<string, object>)userStatsNode.Value;				
				UserStats newUser = new UserStats(userStatsDict);				

				tempList.Add(newUser);
			}
			completionBlock(tempList);
		}
		catch(Exception ex)
		{
			Debug.Log(ex.InnerException.Message);
		}
	}

    public void CreateNewCurriculum(Curriculum curriculum)
    {
        string curriculumJSON = JsonUtility.ToJson(curriculum);
        Router.Curriculum().Push().SetRawJsonValueAsync(curriculumJSON);
    }

    public async void GetCurriculum(Action<List<Curriculum>> completionBlock)
    {
        List<Curriculum> tempList = new List<Curriculum>();
        try
        {
            DataSnapshot curriculum = await Router.Curriculum().GetValueAsync();

            foreach(DataSnapshot curriculumNode in curriculum.Children)
            {
                var curriculumDict = (IDictionary<string, object>)curriculumNode.Value;
                Curriculum newCurriculum = new Curriculum(curriculumDict);
                tempList.Add(newCurriculum);
            }

            completionBlock(tempList);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.InnerException.Message);
        }
    }

    public void CreateNewPost(Post post)
    {
        string postJSON = JsonUtility.ToJson(post);
        Router.Post().Push().SetRawJsonValueAsync(postJSON);        
    }

    public async void GetPost(Action<List<Post>> completionBlock)
    {
        List<Post> tempList = new List<Post>();
        
        try
        {
            DataSnapshot post = await Router.Post().GetValueAsync();

            foreach(DataSnapshot postNode in post.Children)
            {
                var postDict = (IDictionary<string, object>)postNode.Value;
                Post newPost = new Post(postDict);
                tempList.Add(newPost);
            }

            completionBlock(tempList);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.InnerException.Message);
        }        
    }
}
