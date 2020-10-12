using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;
using System;


public class PLayerController : NetworkBehaviour
{
    public float m_Speed = 6.0f;
    public float m_LookSens = 1.0f;
    CharacterController characterController;
    private float verticalAxisInput;
    private float horizontalAxisInput;
    private float mouseVerticalAxisInput;
    private float mouseHorizontalAxisInput;
    public float mouseYRange;

    public float maxHealth = 100f;
    public Camera cam;
    public GameObject flashlight;
    public bool hasFlashlight;

    public GameObject NightvisionFX;
    public bool hasNightvision;
    ShootingAgent shootingAgent;
    public bool dead = false;
    public int mineAmmo;
    public int grenadeAmmo;
    public int smokeAmmo;
    public int flashAmmo;
    public int invisbleAmmo;
    public int camAmmo;
    public GameObject minePrefab;
    public GameObject mineDropLocation;
    public bool mineSetting = true;

    PlayerManager playerManager;
    Terminal[] terminal;
    public LayerMask terminalMask;
    public bool hacking;
    public float hackingRange = 10;
    [SerializeField] private float hackingSpeed = 1;
    [SerializeField] private float yAxisRangePlus = 90;
    [SerializeField] private float yAxisRangeMinus = -90;
    [SerializeField] private ParticleSystem system;

    [SerializeField] private bool noButtonHolding = false;
    [SerializeField] private TextMeshPro displayName;
    private int terminalButtonCheck;
    [SerializeField] private bool lockDown = false;
    Timer timerScript;
    //UI UI;
    Nametag nametag;
    AudioSource walkingSound;
    public GameObject armsAndGuns;
    CameraManager cameraManager;
    [SyncVar]
    public float playerHealth = 100;
    private bool Crouch;
    private Canvas canvas;
    private Renderer[] playerRenderer;
    [SerializeField] Animator animator;
    [SerializeField] private GameObject spyRagdoll;
    [SerializeField] private GameObject soldierRagdoll;
    [SerializeField]private int playerNumber;
    Vector3 playerTransform;
    private int culmask;
    private Terminal t;
    [SerializeField]private GameObject spyModel;
    [SerializeField]private GameObject soldierModel;
    [SerializeField]private NetworkManagerHUD networkManager;
    [SerializeField]private TextMeshProUGUI win;
    [SerializeField]private GameObject realArms;
    [SerializeField]private GameObject agentgun;
    [SerializeField]private GameObject spygun;
    public LayerMask playerLayer1, playerLayer2, playerLayer3, playerLayer4;
    



    void Start()
    {
        //win = GameObject.Find("Spy Win").GetComponent<TextMeshProUGUI>();
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>();
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        nametag = FindObjectOfType<Nametag>();
        shootingAgent = GameObject.FindObjectOfType<ShootingAgent>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        terminal = GameObject.FindObjectsOfType<Terminal>();
        timerScript = GameObject.FindObjectOfType<Timer>();
        walkingSound = GetComponent<AudioSource>();
        cameraManager = GetComponent<CameraManager>();
        InvokeRepeating("PlayWalkingSound", 0.0f, 0.5f);
        crouching = false;
        canvas = GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>();
        canvas.enabled = true;
        playerRenderer = GetComponentsInChildren<Renderer>();
        ++playerManager.playerCount;
        playerNumber = playerManager.playerCount;
        
        if(playerNumber == 0|| playerNumber == 1)
        {
            spyModel.SetActive(true);
            gameObject.tag = "Spy";
        }
        if(playerNumber == 2|| playerNumber == 3)
        {
            soldierModel.SetActive(true);
            gameObject.tag = "Soldier";
        }
        playerTransform = transform.position;
        animator = GetComponentInChildren<Animator>();
        animator.gameObject.layer = playerNumber + 16;
        foreach (Transform child in animator.GetComponentInChildren<Transform>(true))
        {
            child.gameObject.layer = animator.gameObject.layer;
            
        }
        agentgun.gameObject.layer = animator.gameObject.layer;
        spygun.gameObject.layer = animator.gameObject.layer;
        if(playerNumber == 0)
        {
            cam.cullingMask = playerLayer1;
        }
        else if(playerNumber == 1)
        {
            cam.cullingMask = playerLayer2;
        }
        else if(playerNumber == 2)
        {
            cam.cullingMask = playerLayer3;
        }
        else if(playerNumber == 3)
        {
            cam.cullingMask = playerLayer4;
        }

        //UI = FindObjectOfType<UI>();
        //displayName.text = nametag.nameTag;
    }
    // Update is called once per frame

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        //playerCanvas.gameObject.SetActive(true);
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0.021f, 0.807f, 0.01f);
        Camera.main.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        playerHealth = maxHealth;
        Debug.Log("created a local player");
    }
    void OnDisable()
    {
        if (isLocalPlayer)
        {
            Camera.main.transform.SetParent(null);
            Camera.main.transform.localPosition = new Vector3(0f, 50f, 0f);
            Camera.main.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        }
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            grenadeAmmo = 3;
            smokeAmmo = 0;
            mineAmmo = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            grenadeAmmo = 0;
            smokeAmmo = 3;
            mineAmmo = 0;
        }
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            grenadeAmmo = 0;
            smokeAmmo = 0;
            mineAmmo = 3;
        }
        if(cam != null)
        {
            realArms.transform.position = cam.transform.position;
            realArms.transform.rotation = cam.transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.P)){
            print(cam.cullingMask + " mask int");
            print(Convert.ToString(cam.cullingMask, 2) + " mask bin");
            LayerMask l = new LayerMask();
            l = cam.cullingMask;
            print(Convert.ToString(l.value, 2));
        }
        /*if(playerManager.spyDeathCount >= 2)
        {
            characterController.enabled = true;
            animator.gameObject.SetActive(true);
            playerManager.spyDeathCount = 0;
            playerManager.soldierDeathCount = 0;
            dead = false;
        }
        if(playerManager.soldierDeathCount >= 2)
        {
            characterController.enabled = true;
            animator.gameObject.SetActive(true);
            playerManager.soldierDeathCount = 0;
            playerManager.spyDeathCount = 0;
            dead = false;

        }*/
        if (Input.GetKeyDown(KeyCode.G) && mineAmmo >= 1 && mineSetting == true)
        {
            CmdSetMine();
            mineAmmo--;
            mineSetting = false;
            //Instantiate(minePrefab, mineDropLocation.transform.position, transform.rotation);
            //StartCoroutine(IESettingMineActive(2.0f));
        }
        HackFunction();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CmdSpawnRagdoll();
            ++playerManager.spyDeathCount;
        }
        //text.text = "HEALTH: " + (int)playerHealth;
        horizontalAxisInput = Input.GetAxis("Horizontal1");
        verticalAxisInput = Input.GetAxis("Vertical1");
        mouseHorizontalAxisInput = Input.GetAxis("Mouse X1") * m_LookSens;
        mouseVerticalAxisInput = Input.GetAxis("Mouse Y1") * m_LookSens;
        //m_MouseYRange = joystick.RightStickY * m_RotatationYRange;
        mouseYRange += Input.GetAxis("Mouse Y1") * m_LookSens;
        mouseYRange = Mathf.Clamp(mouseYRange, yAxisRangeMinus, yAxisRangePlus);
        //playerHealth = maxHealth;
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }*/
        //Taschenlampe
        if (Input.GetKeyDown(KeyCode.F))
        {
            hasFlashlight = !hasFlashlight;
        }
        if(hasFlashlight)
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            canvas.enabled = !canvas.enabled;
            networkManager.showGUI = !networkManager.showGUI;
        }
        //Nachtsicht
        if (hasNightvision)
        {
            NightvisionFX.SetActive(true);
        }
        else
        {
            NightvisionFX.SetActive(false);
        }

        //super toggle 
        if (Input.GetKeyDown(KeyCode.N))
            hasNightvision = !hasNightvision;

        if (Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.C))
        {
            crouching = !crouching;

        }
        if (cameraManager.inputblock == false && !dead)
            CameraMove();

    }
    public void FixedUpdate()
    {
        if(cam != null)
        NightvisionFX.transform.position = cam.transform.position;
        if (!isLocalPlayer) return;
        //terminalButtonCheck++;
        if (!lockDown)
        {
            Moving();
            //walkingSound.Play();
        }

        
        if (Input.GetKeyDown(KeyCode.U))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        //death = playerhealth <= 0;
        Collider[] terminalChecks = Physics.OverlapSphere(gameObject.transform.position, hackingRange, terminalMask);
        for (int i = 0; i < terminalChecks.Length; i++)
        {
            terminalButtonCheck = i;

            //probably broken here cuz t "unneccessary"
            Terminal t = terminalChecks[i].GetComponent<Terminal>();
        }


        //rausgenommen bis timer wieder geht
        //if (playerHealth < 1 && gameObject.tag == "Soldier" || timerScript.timer <= 0)
        //{
        //    transform.position = playerManager.spawnpoints[1].transform.position;
        //    playerHealth = maxHealth;
        //    StartCoroutine(IEDeath(0));
        //}
        if (terminalButtonCheck > 0)
        {
            //never called
            //Debug.Log("hacking init 5");
            //noButtonHolding = false;
        }



        
        if (playerHealth < 1 && gameObject.tag == "Spy")
        {
            ++playerManager.spyDeathCount;
            CmdSpawnRagdoll();
            transform.position = playerManager.spawnpoints[playerNumber].transform.position;
            /*if(playerManager.spyDeathCount == 1)
            {
                dead = true;
                animator.gameObject.SetActive(false);
                Debug.Log("Spy died");
                
                characterController.enabled = false;
            }*/
            playerHealth = maxHealth;
        }
        /*else
        {
             for(int i = 0; i < playerRenderer.Length; i++)
            {
                playerRenderer[i].enabled = true; 
            }
        }*/
        if (playerHealth < 1 && gameObject.tag == "Soldier")
        {
            ++playerManager.soldierDeathCount;
            CmdSpawnRagdoll();
            transform.position = playerManager.spawnpoints[playerNumber].transform.position;
            /*if(playerManager.soldierDeathCount == 1)
            {
                dead = true;
                animator.gameObject.SetActive(false);
                Debug.Log("Soldier died");
                
                characterController.enabled = false;
            }*/
            playerHealth = maxHealth;
        }
    }



    void Moving()
    {
        if (isLocalPlayer)
        {

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float forSpeed = verticalAxisInput * m_Speed;
            characterController.SimpleMove(forward * forSpeed);
            Vector3 side = transform.TransformDirection(Vector3.right);
            float sidSpeed = horizontalAxisInput * m_Speed;

            CmdWalkingForward(forSpeed);
            CmdWalkingSideward(sidSpeed);
            characterController.SimpleMove(side * sidSpeed);
            float input = Mathf.Abs(forSpeed) + Mathf.Abs(sidSpeed);
        }
    }
    void CameraMove()
    {
        if(cameraManager.cams[0] != null)
        if (cameraManager.cams[0].GetComponent<Camera>().enabled == true)
        {
            transform.Rotate(Vector3.up, mouseHorizontalAxisInput, Space.World);
            mouseVerticalAxisInput = mouseYRange;
            Camera.main.transform.localEulerAngles = Vector3.zero;
            Camera.main.transform.Rotate(cam.transform.right, -mouseVerticalAxisInput, Space.World);
            armsAndGuns.transform.rotation = cam.transform.rotation;
        }
    }
    public bool crouching
    {
        get => Crouch;
        set
        {
            Crouch = value;
            if (Crouch)
            {
                characterController.height = 1f;
                CmdCrouchingAnim(true);
            }
            else
            {
                characterController.height = 1.85f;
                CmdCrouchingAnim(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("you got hit");
        }
    }
    public void TakeDamage(float amount)
    {
        playerHealth -= amount;
        system.Play();
    }
    //IEnumerator IESettingMineActive(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    mineSetting = true;
    //}
    IEnumerator IEDeath()
    {
        characterController.enabled = false;
        lockDown = true;

        yield return new WaitForSeconds(2);
        lockDown = false;
        characterController.enabled = true;
    }
    void PlayWalkingSound()
    {
        if (verticalAxisInput >= .2f || horizontalAxisInput >= .2f)
        {
            walkingSound.loop = true;
            walkingSound.Play();
        }
        else
        {
            walkingSound.loop = false;
        }
    }

    void HackFunction()
    {
        
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, hackingRange, terminalMask);
        if (gameObject.tag == "Spy" && Input.GetKeyDown(KeyCode.E) && terminalButtonCheck == 0&& colliders.Length > 0)
        {
            noButtonHolding = !noButtonHolding;
        }
        
        //stuck in this loop every frame once hack started
        for (int i = 0; i < colliders.Length; i++)
        {
            if (noButtonHolding && colliders.Length > 0)
            {
                CmdTerminalHack();
                Debug.Log("hacking init 1");
                
                //this stops once we get out of sphere
                Debug.Log("hacking init 2");
                t = colliders[i].GetComponent<Terminal>();
                t.gettingHacked = true;
                CmdHacking();
                if (!t.IsHacked)

                    //this runs until hack is done    
                Debug.Log("hacking init 3");
                
                //Debug.Log(t.HackPercentage);

                if (t.HackPercentage >= 100)
                {
                    //win.enabled = true;
                    t.HackPercentage = 100;
                    noButtonHolding = false;
                    //never gets here
                    Debug.Log("Done");
                    break;
                }
            }
        }
    }
    [Command]
    void CmdSetMine()
    {

        GameObject Mine = Instantiate(minePrefab, mineDropLocation.transform.position, transform.rotation);
        NetworkServer.Spawn(Mine);
        StartCoroutine(IESettingMineActive(2.0f));

        IEnumerator IESettingMineActive(float time)
        {
            yield return new WaitForSeconds(time);
            mineSetting = true;
        }
    }
    void CmdTerminalHack()
    {
        RpcCheckHack();
    }
    [ClientRpc]
    void RpcCheckHack()
    {
        //Terminal.HackFuncRef.Hackerino();
        

        for (int i = 0; i < terminal.Length; i++)
        {
           terminal[i].Hackerino();
        }
    }
    [Command]
    void CmdWalkingSideward(float speed)
    {
        RpcWalkingSideward(speed);
    }
    [Command]
    void CmdWalkingForward(float speed)
    {
        RpcWalkingForward(speed);
    }
    [ClientRpc]
    void RpcWalkingSideward(float speed)
    {
        animator.SetFloat("x", speed);
    }
    [ClientRpc]
    void RpcWalkingForward(float speed)
    {
        animator.SetFloat("z", speed);
    }
    [Command]
    void CmdCrouchingAnim(bool crouch)
    {
        RpcCrouchingAnim(crouch);
    }
    [ClientRpc]
    void RpcCrouchingAnim(bool crouch)
    {
        animator.SetBool("Crouching", crouch);
    } 
    [Command]
    void CmdSpawnRagdoll()
    {
        if(gameObject.tag == "Soldier")
        {
            GameObject soldierrag = Instantiate(spyRagdoll,transform.position,transform.rotation);
            NetworkServer.Spawn(soldierrag);
        }
        if(gameObject.tag == "Spy")
        {
            GameObject spyrag = Instantiate(soldierRagdoll,transform.position,transform.rotation);
            NetworkServer.Spawn(spyrag);
        }
    }
    [Command]
    void CmdHacking()
    {
        for(int i = 0 ;i < 2;i++)
        terminal[i].hackPercentage += Time.deltaTime + hackingSpeed;
    }
}
