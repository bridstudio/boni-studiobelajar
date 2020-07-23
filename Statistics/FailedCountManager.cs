using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailedCountManager : MonoBehaviour
{    
    public static int failedCount, exitButtonPressed, restartButtonPressed;
    private RaycastHit2D hit2d;
    private bool isHit = false;
    private bool mouseDown = false;
    private int timerHit;    

    // Start is called before the first frame update
    void Start()
    {
        failedCount = 0;
        exitButtonPressed = 0;
        restartButtonPressed = 0;        

        // GameObject goStart = GameObject.FindGameObjectWithTag("Checker");
        // goStart.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UserStatsManager.failedCount = failedCount.ToString();
        UserStatsManager.exitButtonPressed = exitButtonPressed.ToString();
        UserStatsManager.restartButtonPressed = restartButtonPressed.ToString();
        
        if(Input.GetMouseButtonDown(0))
        {
            mouseDown = true;            
        }
        else if(Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        CheckForTouch();
    }

    public void SetActiveChecker()
    {
        GameObject goStart = GameObject.FindGameObjectWithTag("Checker");
        goStart.SetActive(true);
    }

    public static void AddFailedCount()
    {
        failedCount += 1;
        Debug.Log("FailedCount: " + failedCount);
    }

    public void AddExitButtonPressed()
    {
        exitButtonPressed += 1;
        Debug.Log("ExitButtonPressed: " + exitButtonPressed);
    }

    public void AddRestartButtonPressed()
    {
        restartButtonPressed += 1;
        Debug.Log("RestartButtonPressed: " + restartButtonPressed);
    }

    public void CheckForTouch()
    {                        
        hit2d = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);        
        if(hit2d.collider != null)
        {
            if(isHit == false && mouseDown == true)
            {
                if(hit2d.transform.tag == "Obstacle")
                {                    
                    AddFailedCount();
                    isHit = true;
                }
                else if(hit2d.collider.tag == "Checker")
                {
                    string gameObjectName = hit2d.collider.name;                    
                    Debug.Log(gameObjectName);
                    // Destroy(GameObject.Find(gameObjectName));
                    GameObject go = GameObject.Find(gameObjectName);
                    go.SetActive(false);

                    if(!go.activeSelf)
                    {
                       UserStatsManager.progressCompletion += 1;
                    //    Debug.Log("UserStatsManager.progressCompletion: " + UserStatsManager.progressCompletion);
                    }
                }
            }            
        }
        else if(hit2d.collider == null)
        {
            //Debug.Log("NOT HIT");
            isHit = false;
        }
    }        
}
