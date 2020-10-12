using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnPlayerTest : MonoBehaviour
{
    PlayerManager playerManager;
    [SerializeField]private bool oneCall = true;
    Timer timerScript;
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        timerScript = FindObjectOfType<Timer>();
    }

    // Pls dont delete
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.L)&& oneCall == true)
        {
            for( int i = 0 ; i < 5 ; i++ )
            {
                playerManager.CreatePlayer();
            }
            timerScript.StartTimer(300);
            oneCall = false;
        }
        */
    }
}
