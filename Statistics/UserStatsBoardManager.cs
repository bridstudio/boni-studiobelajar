using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Database;
using MaterialUI;

public class UserStatsBoardManager : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;
    public List<UserStats> userStatsList = new List<UserStats>();
    public GameObject rowPrefab;
    public GameObject scrollContainer;
    public GameObject dialogLoading;    

    public void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
        userStatsList.Clear();

        OpenDialogLoading(true);

        DatabaseManager.sharedInstance.GetUserStats(result =>
        {
            userStatsList = result;

            InitialiseUI();
        });        
    }

    public void OpenDialogLoading(bool dialog)
    {                
        dialogLoading.SetActive(dialog);
    }    

    public void InitialiseUI()
    {
        foreach(UserStats userStats in userStatsList)
        {
            CreateRow(userStats);            
        }
    }

    public void CreateRow(UserStats userStats)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;

        newRow.GetComponent<RowConfig>().Initialise(userStats);
        newRow.transform.SetParent(scrollContainer.transform, false);

        OpenDialogLoading(false);
    }

}
