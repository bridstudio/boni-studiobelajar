using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class CurriculumBoardManager : MonoBehaviour
{
    public List<Curriculum> curriculumList = new List<Curriculum>();
    public GameObject rowPrefab, scrollContainer, dialogLoading;
    public Text childCountText;
    public GameObject infoGameObject;

    public void Awake()
    {
        curriculumList.Clear();

        OpenDialogLoading(true);        

        DatabaseManager.sharedInstance.GetCurriculum(result =>
        {
            curriculumList = result;

            InitialiseUI();
        });

        // Router.Curriculum().LimitToFirst(1);
    }

    public void OpenDialogLoading(bool dialog)
    {
        dialogLoading.SetActive(dialog);
    }

    void InitialiseUI()
    {
        if(curriculumList.Count > 0)
        {
            childCountText.text = "";

            foreach(Curriculum curriculum in curriculumList)
            {
                CreateRow(curriculum);
            }
        }
        else
        {
            OpenDialogLoading(false);
            childCountText.text = "Sorry, there was no data available";
        }
    }

    void CreateRow(Curriculum curriculum)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;

        newRow.GetComponent<CurriculumRowConfig>().Initialise(curriculum);
        newRow.transform.SetParent(scrollContainer.transform, false);

        OpenDialogLoading(false);
    }

    public void DestroyInfoGameObject()
    {
        Destroy(infoGameObject);
    }
}
