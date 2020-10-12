//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Mirror;



//public class ControlScreen : MonoBehaviour
//{
//    [SerializeField] private NetworkManagerHUD networkManager;
//    Text buttonText;
//    KeyCode newKeyCode;
//    Event keyEvent;
//    public GameObject ScreenPanel;
//    bool waitForInput;
//    public Transform menuPanel;
//    public void ShowScreen()
//    {
//        ScreenPanel.SetActive(true);
//    }

//    void Start()
//    {
//        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>();
//        waitForInput = false;
//        Debug.Log(menuPanel);
//        for (int i = 0; i < menuPanel.childCount; i++)
//        {
//            if (menuPanel.GetChild(i).name == "ForwardButton")
//                //menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.forward.ToString();
//            else if (menuPanel.GetChild(i).name == "BackwardButton")
//                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.backward.ToString();
//            else if (menuPanel.GetChild(i).name == "RightButton")
//                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.right.ToString();
//            else if (menuPanel.GetChild(i).name == "LeftButton")
//                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.left.ToString();
//            else if (menuPanel.GetChild(i).name == "CrouchButton")
//                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.crouch.ToString();
//            else if (menuPanel.GetChild(i).name == "InteractionButton")
//                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.interact.ToString();
//            else if (menuPanel.GetChild(i).name == "GadgetButton")
//                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.gadget.ToString();
//            else if (menuPanel.GetChild(i).name == "CameraButton")
//                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = KeyCodeManager.KCM.camAccess.ToString();
//        }
//    }

//    void OnGUI()
//    {
//        keyEvent = Event.current;
//        if (keyEvent.isKey && waitForInput)
//        {
//            newKeyCode = keyEvent.keyCode;
//            waitForInput = false;
//            Debug.Log(newKeyCode);
//        }
//    }

//    public void StartCode(string keyName)
//    {
//        if (!waitForInput)
//        {
//            StartCoroutine(BindKey(keyName));
//        }
//    }

//    public void SendText(Text text)
//    {
//        buttonText = text;
//    }


//    IEnumerator WaitForInput()
//    {
//        while (!keyEvent.isKey)
//            yield return null;
//    }

//    public IEnumerator BindKey(string keyName)
//    {
//        waitForInput = true;
//        yield return WaitForInput();
//        switch (keyName)
//        {
//            case "forward":
//                KeyCodeManager.KCM.forward = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.forward.ToString();
//                PlayerPrefs.SetString("forwardKey", KeyCodeManager.KCM.forward.ToString());
//                break;
//            case "backward":
//                KeyCodeManager.KCM.backward = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.backward.ToString();
//                PlayerPrefs.SetString("backwardKey", KeyCodeManager.KCM.backward.ToString());
//                break;
//            case "right":
//                KeyCodeManager.KCM.right = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.right.ToString();
//                PlayerPrefs.SetString("rightKey", KeyCodeManager.KCM.right.ToString());
//                break;
//            case "left":
//                KeyCodeManager.KCM.left = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.left.ToString();
//                PlayerPrefs.SetString("leftKey", KeyCodeManager.KCM.left.ToString());
//                break;
//            case "crouch":
//                KeyCodeManager.KCM.crouch = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.crouch.ToString();
//                PlayerPrefs.SetString("crouchKey", KeyCodeManager.KCM.crouch.ToString());
//                break;
//            case "gadget":
//                KeyCodeManager.KCM.gadget = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.gadget.ToString();
//                PlayerPrefs.SetString("gadgetKey", KeyCodeManager.KCM.gadget.ToString());
//                break;
//            case "interact":
//                KeyCodeManager.KCM.interact = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.interact.ToString();
//                PlayerPrefs.SetString("interactKey", KeyCodeManager.KCM.interact.ToString());
//                break;
//            case "camAccess":
//                KeyCodeManager.KCM.camAccess = newKeyCode;
//                buttonText.text = KeyCodeManager.KCM.camAccess.ToString();
//                PlayerPrefs.SetString("cameraKey", KeyCodeManager.KCM.camAccess.ToString());
//                break;

//        }
//    }
//}
