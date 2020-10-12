using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


public class ShootingAgent : NetworkBehaviour
{
    [SerializeField] GameObject MainCamerStuff;
    //AudioSource shootingSound;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;
    public GameObject bulletSpawnPos;
    PLayerController pl;
    public float delay = 0.25f;
    float t;
    public bool damageCalculationBool;
    Collider col;
    public LayerMask layerMask;
    public int grenadeAmmo = 3;
    public float throwForce = 15f;
    private bool grenadeThrown;
    public float Shootingdamage = 30;
    [SerializeField] private float recoilSpeed;
    [SerializeField] private GameObject ShootSoundbox;

    //[SerializeField]  GameObject MainCamerStuff;


    Slider overheatSlider;

    public GameObject projectilePrefab;
    public Transform projectileMount;
    [SerializeField] private float overheatAdd;
    [SerializeField] GameObject damageSpherePrefab;
    //Shootline VFXController;
    public bool shootingOverheat = false;

    void Start()
    {
        overheatSlider = GameObject.Find("OverheatSlider").GetComponent<Slider>();
        //shootingSound = MainCamerStuff.GetComponent<AudioSource>();
        t = delay;
        pl = GameObject.FindObjectOfType<PLayerController>();
        col = GetComponent<Collider>();


        //find better way to reference this (maybe in player prefab)
        //VFXController = GameObject.Find("VFX Controller").GetComponent<Shootline>();
        //VFXController = GetComponent<Shootline>();
    }

    public bool shootingLocked;

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.right * 100, Color.red, 1);
        t = t + 1 * Time.deltaTime;
        damageCalculationBool = false;

        if (isLocalPlayer)
        {
            if (overheatSlider.value >= 95)
            {
                shootingOverheat = true;
            }
            else if (overheatSlider.value == 0)
            {
                shootingOverheat = false;
            }
            if (Input.GetKey(KeyCode.Mouse0) && shootingOverheat == false && pl.dead == false)// && t > delay)
            {
                Debug.Log("shootlog 1");
                //called every frame while true
                if (!shootingLocked)
                {
                    Debug.Log("shootlog 2");
                    CmdRaycastShot();
                    //shootingSound.Play();
                    overheatSlider.value = overheatSlider.value + overheatAdd;
                    StartCoroutine(IEShotDelay(0.5f));
                }
                Debug.Log("yyyyy");
                

                //CmdFiring();  
            }
        }
        
    }
    void FixedUpdate()
    {
        if (overheatSlider.value >= 1)
        {
            --overheatSlider.value;
        }
    }
    IEnumerator IEShotDelay(float time)
    {
        Debug.Log("shootlog 3");
        //shootingSound.Play();
        shootingLocked = true;
        float f = 0;
        while (f <= 1)
        {
            Debug.Log("shootlog 4");
            

            f += Time.deltaTime * 25f;
            pl.mouseYRange += Time.deltaTime * recoilSpeed;
            // CmdFire();

            yield return null;
        }
        yield return new WaitForSeconds(time);
        shootingLocked = false;
    }


    //[Command]
    //void CmdFire()
    //{
    //    GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, pl.armsAndGuns.transform.rotation);
    //    NetworkServer.Spawn(projectile);
    //    //overheatSliderScript.overheatSlider.value += overheatValue;
    //    //Firing();
    //}
    [Command]
    void CmdRaycastShot()
    {
        Debug.Log("shootlog 5");
        
        //shootingSound.Play;
        Debug.DrawRay(pl.armsAndGuns.transform.position, pl.armsAndGuns.transform.forward * 1000, Color.red);
        if (Physics.Raycast(projectileMount.transform.position, pl.armsAndGuns.transform.forward, out RaycastHit hit))
        {
            Debug.Log(hit);
            GameObject shootsound = Instantiate(ShootSoundbox, pl.armsAndGuns.transform.position, pl.armsAndGuns.transform.rotation);
            NetworkServer.Spawn(shootsound);
            Destroy(shootsound, 1f);

            Debug.Log("shootlog 6");
            //Debug.Break();
            GameObject damageSphere = Instantiate(damageSpherePrefab, hit.point, transform.rotation);
            NetworkServer.Spawn(damageSphere);
            Destroy(damageSphere, 0.5f);



            //VFXController.CreateShotVFX(projectileMount.transform.forward * -1 + projectileMount.transform.position, hit);
            //CreateShotVFX(projectileMount.transform.position, hit);
            //Debug.Break();


            //Last Resort
            //shot line

            Vector3 startPoint = pl.armsAndGuns.transform.position;

            GameObject g = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
            NetworkServer.Spawn(g);
            StartCoroutine(Shoot(g, hit.normal, startPoint));

            //destroy sparks
            GameObject tempfx = Instantiate(fx, hit.point + hit.normal * .1f, Quaternion.LookRotation(hit.normal, Vector3.up));
            NetworkServer.Spawn(tempfx);
            Destroy(tempfx, 1);

            //spawn decal
            GameObject tempDecal = Instantiate(decal, hit.point, Quaternion.LookRotation(-hit.normal, Vector3.up));
            NetworkServer.Spawn(tempDecal);
            //rotate decals randomly
            tempDecal.transform.Rotate(tempDecal.transform.forward, Random.Range(0, 90), Space.World);


            //RPCRaycastShot();         
        }
    }


    public GameObject effect;
    public float speed = .1f;
    Camera cam;
    public AnimationCurve curve;
    public GameObject fx;
    public GameObject decal;


    IEnumerator Shoot(GameObject g, Vector3 normal, Vector3 startPoint)
    {
        //RpcShootSound();
        Debug.Log("Enum Shoot");
        LineRenderer l = g.GetComponent<LineRenderer>();
        Vector3[] pos = new Vector3[] { startPoint, g.transform.position };
        l.SetPositions(pos);
        g.transform.GetChild(0).transform.position += normal * .1f;
        Material m = Instantiate(l.material);
        l.material = m;

        Light licht = g.transform.GetChild(0).GetComponent<Light>();
        float lichtstart = licht.intensity;

        float t = 0;
        int frames = 0;

        while (t <= 1)
        {
            t += Time.deltaTime / speed;
            //Vector3 currentPos = Vector3.Lerp(startPoint, g.transform.position, Mathf.Clamp(t, 0, .5f));
            l.material.SetFloat("_Fade", curve.Evaluate(t));
            licht.intensity = Mathf.Lerp(lichtstart, 0, t);

            frames++;


            yield return null;
        }

        //print(frames);

        Destroy(g);

    }
    //[ClientRpc]

    //void RpcShootSound()
    //{
    //    Debug.Log("shootlog 7");
    //    if (!isLocalPlayer)
    //    {
    //        Debug.Log("shootlog 8");
    //        shootingSound.Play();
    //    }

    //}
}

    //disabled for testing

    //public void CreateShotVFX(Vector3 startPoint, RaycastHit hit)
    //{
    //    //shot line
    //    GameObject g = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
    //    NetworkServer.Spawn(g);
    //    //StartCoroutine(Shoot(g, hit.normal, startPoint));

    //    //destroy sparks
    //    GameObject tempfx = Instantiate(fx, hit.point + hit.normal * .1f, Quaternion.LookRotation(hit.normal, Vector3.up));
    //    NetworkServer.Spawn(tempfx);
    //    Destroy(tempfx, 1);

    //    //spawn decal
    //    GameObject tempDecal = Instantiate(decal, hit.point, Quaternion.LookRotation(-hit.normal, Vector3.up));
    //    NetworkServer.Spawn(tempDecal);
    //    //rotate decals randomly
    //    tempDecal.transform.Rotate(tempDecal.transform.forward, Random.Range(0, 90), Space.World);
    //}


    // [ClientRpc]
    // void RPCRaycastShot()
    // {
    //     GameObject damageSphere = Instantiate(damageSpherePrefab,spawnpoint,transform.rotation);
    // }

    //void CmdFiring()
    //{
    //    damageCalculationBool = true;
    //    t = 0;
    //    shootingSound.Play();
    //    Debug.DrawRay(transform.position, transform.right * 100, Color.red, 5);
    //    if (Physics.Raycast(transform.position, transform.right, out RaycastHit hit, layerMask.value) && damageCalculationBool == true)
    //    {
    //        if (hit.collider.tag == "Spy" || hit.collider.tag == "Soldier")
    //        {
    //            var enemyCollider = hit.collider.gameObject.GetComponent<PLayerController>();
    //            enemyCollider.TakeDamage(Shootingdamage);
    //            Debug.Log(enemyCollider.playerHealth);
    //        }
    //        if (hit.collider.tag == "Head")
    //        {
    //            var enemyCollider = hit.collider.gameObject.GetComponent<PLayerController>();
    //            enemyCollider.TakeDamage(Shootingdamage * 1.5f);
    //            Debug.Log(enemyCollider.playerHealth);
    //        }
    //        StartCoroutine(IEShotDelay(0f));
    //    }
    //}

//Instantiate(bulletPrefab, bulletSpawnPos.transform.position, transform.rotation);
//hit = Physics.Raycast(transform.position,transform.right,col.bounds.extents.x+100,layerMask.value);
//Debug.Log(hit);         

/*if(pl.joystick.RightBumper && grenadeAmmo > 0)
{
    --grenadeAmmo;     
}     /* */
//}



//}
