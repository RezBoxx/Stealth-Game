using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UICompassClamp : MonoBehaviour
{
    public TextMeshProUGUI[] terminalPoints;
    public TextMeshProUGUI[] terminalPointsStatus; 
    CameraManager cameraManager;
    private bool  found = false;
    public List <Vector3> terminalsPositions = new List<Vector3>();
    

    void Start()
    {
        terminalPoints = GameObject.Find("TerminalPoints").GetComponentsInChildren<TextMeshProUGUI>();
        terminalPointsStatus = GameObject.Find("TerminalPointsUI").GetComponentsInChildren<TextMeshProUGUI>();
        for(int i = 0;i< terminalsPositions.Count;i++)
        {
            terminalsPositions[i] = GameObject.Find("Terminal" + i).transform.position;
        }
    }
    void Update()
    {   
        
        
        
        if(found == false )
        {
            cameraManager = GetComponent<CameraManager>();
        }
        if(cameraManager != null)
        {
            found = true;
        }
    }
    void LateUpdate()
    {
        for(int i = 0;i < terminalPoints.Length;i++)
        {
            if(cameraManager.cams[0] != null)
            if(found && cameraManager.cams[0].GetComponent<Camera>().enabled == true )
            {
                Vector3 namepos = Camera.main.WorldToScreenPoint(terminalsPositions[i]);
                if(namepos.z > 0)
                {
                    terminalPoints[i].transform.position = namepos;
                    terminalPoints[i].gameObject.SetActive(true);
                    if(namepos.z < 0)
                    {   
                        terminalPoints[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
