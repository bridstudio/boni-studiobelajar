using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Database;
using MaterialUI;

public class UserStatsBoardManager : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;
    public List<UserStats> userStatsList = new List<UserStats>();
    public GameObject rowPrefab, scrollContainer, dialogLoading;
    public Text childCountText;
    public GameObject infoGameObject;

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
        if(userStatsList.Count > 0)
        {
            childCountText.text = "";
            
            foreach(UserStats userStats in userStatsList)
            {
                CreateRow(userStats);
            }            
        }
        else
        {
            OpenDialogLoading(false);
            childCountText.text = "Maaf, belum ada data yang tersedia. Coba mainkan beberapa permainan";
        }
    }

    public void CreateRow(UserStats userStats)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;

        newRow.GetComponent<RowConfig>().Initialise(userStats);
        newRow.transform.SetParent(scrollContainer.transform, false);

        OpenDialogLoading(false);
    }

    public void DestroyInfoGameObject()
    {
        Destroy(infoGameObject);
    }
}
