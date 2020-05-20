using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCurriculum : MonoBehaviour
{
    public void SendCurrCurriculum()
    {
        SendCurrentCurriculum();
    }

    private void SendCurrentCurriculum()
    {
        Curriculum curriculum = new Curriculum();
        DatabaseManager.sharedInstance.CreateNewCurriculum(curriculum);
        Debug.Log("Send Curriculum");
    }
}
