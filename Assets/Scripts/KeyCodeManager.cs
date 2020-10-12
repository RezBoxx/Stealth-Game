using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeManager : MonoBehaviour
{

    public static KeyCodeManager KCM;

    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode right { get; set; }
    public KeyCode left {get;set;}
    public KeyCode crouch{get;set;}
    public KeyCode gadget{get;set;}
    public KeyCode interact{get;set;}
    public KeyCode camAccess{get;set;}

    void Awake()
    {
        if (KCM == null)
        {
            DontDestroyOnLoad(gameObject);
            KCM = this;
        }
        else if (KCM != this)
        {
            Destroy(gameObject);
        }

        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        right =  (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchKey", "LeftControl"));
        gadget = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("gadgetKey", "G"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactKey", "E"));
        camAccess = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("camAccessKey", "H"));
    }
    
}
