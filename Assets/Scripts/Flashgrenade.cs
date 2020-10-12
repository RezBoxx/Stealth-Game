using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Flashgrenade : NetworkBehaviour
{
    [SerializeField]private float lifeTime;
    [SerializeField]private GameObject flash;
    [SerializeField]private ParticleSystem flashLight;
    [SerializeField]private float explosionRadius = 30;
    [SerializeField]private LayerMask layerMask;
    [SerializeField] private float force = 10;
    GrenadeThrow grenadeThrow;
    Transform transf;
    Rigidbody rb;

    void Start()
    {
        StartCoroutine(IEExplosionTimer(lifeTime));
        rb = GetComponent<Rigidbody>();
        grenadeThrow = FindObjectOfType<GrenadeThrow>();
        transf = grenadeThrow.grenadeSpawn;
        rb.velocity = force * transf.up / 1.5f + transf.forward * 20;
    }

    IEnumerator IEExplosionTimer(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);         
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius,layerMask);
        for(int i = 0; i < colliders.Length; i ++)
        {
            Collider targetCollider = colliders[i];
            PLayerController plTarget = targetCollider.GetComponent<PLayerController>();
            if (!plTarget)
            break;
            Instantiate(flash,plTarget.cam.transform.position,plTarget.cam.transform.localRotation,plTarget.cam.transform);
        }
        flashLight.Play();
        Destroy(gameObject,0.1f);
    }
}
