using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cams;
    Camera current;
    private int index;
    private int first;
    private int last;
    PLayerController pl;
    public bool inputblock;
    void Start()
    {
        pl = GetComponent<PLayerController>();
        cams[0] = pl.cam;
        
        for(int i = 1;i < cams.Length;i++)
        {
            cams[i] = GameObject.Find("SurveilanceCamPrefab" + i).GetComponent<Camera>(); // dont rename pls.
        }
        last = (cams.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)&& gameObject.tag == "Soldier")
        {
            if(index>last)
            index = first;
            Camera.SetupCurrent(cams[index]);
            
            for(int i = 0;i < cams.Length; i++)
            {
                if (cams[0].enabled == false)
            {
                inputblock = true;
            }
            else
            {
                inputblock = false;
            }
                if(cams[i] == Camera.current)
                    cams[i].enabled = true;
                else
                    cams[i].enabled = false;
            }
            index++;
        }
    }
}
