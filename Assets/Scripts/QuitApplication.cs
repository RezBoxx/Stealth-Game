using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{

    public void Exit()
    {
        Debug.Log("I have Quit you.");
        Application.Quit();
    }

}
