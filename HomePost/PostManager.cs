using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    public GameObject userManager;

    void Start()
    {
        Screen.fullScreen = false;
        Screen.orientation = ScreenOrientation.Portrait;
        GameObject newRow = Instantiate(userManager) as GameObject;
    }    
}
