using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostBoardManager : MonoBehaviour
{
    public List<Post> postList = new List<Post>();
    public GameObject rowPrefab, scrollContainer, dialogLoading;
    public Text childCountText; // childCountText to check whether there is a data in server or not
    public GameObject infoGameObject;

    public void Awake()
    {
        postList.Clear();

        OpenDialogLoading(true);

        DatabaseManager.sharedInstance.GetPost(result =>
        {
            postList = result;

            InitialiseUI();
        });
    }

    private void Start()
    {
        Screen.fullScreen = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void OpenDialogLoading(bool dialog)
    {
        dialogLoading.SetActive(dialog);
    }

    void InitialiseUI()
    {
        if(postList.Count > 0)
        {
            childCountText.text = "";

            foreach(Post post in postList)
            {
                CreateRow(post);
            }
        }
        else
        {
            OpenDialogLoading(false);
            childCountText.text = "Maaf, belum ada data yang tersedia";
        }
    }

    void CreateRow(Post post)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;

        newRow.GetComponent<PostRowConfig>().Initialise(post);
        newRow.transform.SetParent(scrollContainer.transform, false);

        OpenDialogLoading(false);
    }

    public void DestroyInfoGameObject()
    {
        Destroy(infoGameObject);
    }
}
