using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

    public class PlayerManager : NetworkBehaviour
    {
        public Camera cam;
        PLayerController pl;
        Timer timerScript;
        public int[] playerhealth = new int[] { 80, 80, 90, 90 };
        const int maxPlayers = 4;

        public List<GameObject> spawnpoints = new List<GameObject>();
        public List<GameObject> terminalHackCheck = new List<GameObject>();
        public List<Vector3> playerPositions = new List<Vector3>();
        //public List<GameObject> playerClasses = new List<GameObject>();
        //public List<Canvas> playerCanvases = new List<Canvas>();
        Terminal terminals;
        public List<Terminal> terminal = new List<Terminal>();
        public bool roundStart = false;
        public int playerCount = -1;
        public int soldierDeathCount = 0;
        public int spyDeathCount = 0;
        [SerializeField]private int hackedCount = 0;
        

    


        void Start()
        {
            terminals = terminalHackCheck[terminal.Count - 1].GetComponent<Terminal>();
            for (int i = 0; i < 4; i++)
            playerPositions[i] = spawnpoints[i].transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if(playerCount > 4)
            {
                playerCount = 0;
            }
            //CheckTerminalStatus();
        }
        void CheckTerminalStatus()
        {
            
            for (int i = 0; i < terminal.Count; i++)
            {
                if (terminal[i].IsHacked)
                    hackedCount++;
                if (hackedCount == 1)
                {
                    terminal[0].IsHacked = false;
                    terminal[0].HackPercentage = 0;
                    terminal[1].IsHacked = false;
                    terminal[1].HackPercentage = 0;
                    break;
                }
            }
        }
    }

