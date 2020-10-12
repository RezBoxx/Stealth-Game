using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GrenadeThrow : NetworkBehaviour
{
    [SerializeField] Rigidbody grenade;
    [SerializeField] Rigidbody smokeGrenade;
    [SerializeField] Rigidbody flashGrenade;
    public Transform grenadeSpawn;
    [SerializeField] GameObject grenadeGO;
    [SerializeField] GameObject smokeGrenadeGO;
    public GameObject flashGrenadeGO;
    [SerializeField] float grenadelifeTime = 1;
    public float flashlifeTime = 3;
    [SerializeField] float smokelifeTime = 2;
    public ParticleSystem flashLight;
    public float explosionRadius = 20;
    public LayerMask layerMask;
    public GameObject flash;
    public bool damage = false;
    [SerializeField] private ParticleSystem smokeSystem;
    //public Transform painispoint;


    [SerializeField] private float force = 10;
    PLayerController pl;


    void Start()
    {
        pl = GameObject.FindObjectOfType<PLayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.G) && pl.grenadeAmmo >= 1)
            {
                --pl.grenadeAmmo;
                CmdFragThrow();
            }
            if (Input.GetKeyDown(KeyCode.G) && pl.smokeAmmo >= 1)
            {
                --pl.smokeAmmo;
                CmdSmokeThrow();
            }
            if (Input.GetKeyDown(KeyCode.G) && pl.flashAmmo >= 1)
            {
                --pl.flashAmmo;
                CmdFlashThrow();
            }
        }
    }
    [Command]
    void CmdFragThrow()
    {
        GameObject Grenade = Instantiate(grenadeGO, grenadeSpawn.position, grenadeSpawn.rotation);
        NetworkServer.Spawn(Grenade);
        //RpcFragThrowEffects();
        //grenade.velocity = force * grenadeSpawn.up/1.5f + grenadeSpawn.forward *20; 
    }
    [Command]
    void CmdSmokeThrow()
    {
        GameObject SmokeGrenade = Instantiate(smokeGrenadeGO, grenadeSpawn.position, grenadeSpawn.rotation);
        NetworkServer.Spawn(SmokeGrenade);
        //
        StartCoroutine(IEEExplosionTimer(smokelifeTime));
        //smokeGrenade.velocity = force * grenaawndeSpawn.up/1.5f + grenadeSpawn.forward * 20;
        //RpcSmokeThrowEffects();
    }
    [Command]
    void CmdFlashThrow()
    {
        Debug.Log("Server creates flash");
        GameObject FlashGrenade = Instantiate(flashGrenadeGO, grenadeSpawn.position, grenadeSpawn.rotation);
        NetworkServer.Spawn(FlashGrenade);
        //flashGrenade.velocity = force * grenadeSpawn.up/1.5f + grenadeSpawn.forward * 20;
        //RpcFlashThrowEffects();
        StartCoroutine(IEExplosionTimer(flashlifeTime));


    }
    IEnumerator IEExplosionTimer(float flashlifeTime)
    {
        Debug.Log("flashcountdown init");
        yield return new WaitForSeconds(flashlifeTime);
        Debug.Log("peep");
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            flashLight.Play();
            Debug.Log("#colliders");
            Collider targetCollider = colliders[i];
            PLayerController plTarget = targetCollider.GetComponent<PLayerController>();
            //if (!plTarget)
            //    break;
            GameObject tempFlash = Instantiate(flash, plTarget.armsAndGuns.transform.position, plTarget.armsAndGuns.transform.localRotation, plTarget.armsAndGuns.transform);
            NetworkServer.Spawn(tempFlash);
            Debug.Log("spawned fleshlight");

            
        }
        flashLight.Play();

        //Destroy(flash, 0.1f);
        
    }
    IEnumerator IEEExplosionTimer(float smokelifeTime)
    {
        yield return new WaitForSeconds(smokelifeTime);
        damage = true;
        smokeSystem.Play();
        //transform.(null);
        smokeSystem.transform.SetParent(null);
        //Destroy(gameObject, 0.1f);
    }
}

    //[ClientRpc]
    //void RpcFragThrowEffects(){
    //}
    //void RpcSmokeThrowEffects() {
    //}
    //void RpcFlashThrowEffects()
    //{
    //    //if(isLocalPlayer)
    //    //{ return; }
        

    //}
