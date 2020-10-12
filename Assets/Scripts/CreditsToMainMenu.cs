using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsToMainMenu : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }



}
