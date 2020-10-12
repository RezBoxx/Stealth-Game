using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;


public class Terminal : NetworkBehaviour
{
    public bool hacked;
    private Renderer materialRenderer;
    
    [SerializeField] private AudioSource hackingSound;
    [SerializeField] private AudioSource alarmSound;
    public static Terminal HackFuncRef;
    [SyncVar]
    public float hackPercentage;
    [SerializeField] private TextMeshProUGUI textHackProgress;
    Terminal[] terminal;
    public bool gettingHacked;
    UICompassClamp iCompassClamp;
    
    public float HackPercentage
    {
        get => hackPercentage;
        set
        {
            float preValue = hackPercentage;
            hackPercentage = value;
            if (hackPercentage >= 100)
            {                
                //Debug.Log("hacked");
                IsHacked = true;
                textHackProgress.text = "Corrupted";
                //sphereRen.enabled = false;

            }
            else
            {
                //Debug.Log(hackPercentage);
                IsHacked = false;
            }
            if (hackPercentage <= 1 << 99)
            {
                if(gettingHacked == false)
                {
                    gameObject.SetActive(false);
                }
                
            }
            if (HackPercentage >= 20)
            //alarmSound.Stop();
            //if(preValue < 1 && hackPercentage >= 1)
            {
                //hackingSound.Play();
                //alarmSound.Play();
            }
        }
    }

    public bool IsHacked
    {
        get => hacked;
        set
        {
            hacked = value;
            Hackerino();
        }
    }

    private void Awake()
    {
        
        
    }
    public void Hackerino()
    {
        {
            //this runs until hack is done, doesnt send client that its done. only called 5 times online, until done offline.
            //Debug.Log("started hackerino function!");
            if (hacked)
            {
                //RpcHacked();

                
                textHackProgress.color = new Color32(0, 255, 255, 255);

                //materialRenderer.material.SetColor("_BaseColor", Color.blue);
                //gets called offline every frame once true
                Debug.Log("it got corrupted uwu!");
            }
            else
            {
                //RpcHackInProgress();
                iCompassClamp = FindObjectOfType<UICompassClamp>();
                //this runs until hack is done, doesnt send client that its done. only called 5 times online, until done offline.
                if(hackPercentage == 100)
                {
                    textHackProgress.text = "Corrupted";
                }
                iCompassClamp.terminalPointsStatus[0].text = "Progress: " + (int)hackPercentage + " %";
                textHackProgress.color = new Color32(255, 0, 0, 255);

                //materialRenderer.material.SetColor("_BaseColor", Color.red);
                //Debug.Log(" corruption.exe in progress");
            }
        }
        //[Command]
        //void CmdHacked()
        //{
        //    RpcHacked();
        //}
        //void CmdHackInProcess()
        //{
        //    RpcHackInProgress();
        //}


        //[ClientRpc]
        //void RpcHacked()
        //{
        //    textHackProgress.text = "Corupted";
        //    textHackProgress.color = new Color32(0, 255, 255, 255);
        //}
        //void RpcHackInProgress()
        //{
        //    textHackProgress.text = "Progress: " + (int)hackPercentage + " %";
        //    textHackProgress.color = new Color32(255, 0, 0, 255);
        //}
    }
}
