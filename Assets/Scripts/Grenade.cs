using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public LayerMask layerMask;
    Collider col;
    [SerializeField] private int grenadeDamage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float lifeTime;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AudioSource explosionSound;
    GrenadeThrow grenadeThrow;
    Transform transf;
    [SerializeField] private float force = 10;
    Rigidbody rb;

    void Start()
    {
        explosionSound = GetComponent<AudioSource>();
        //Invoke("PlaySound", 1);
        StartCoroutine(IEEEExplosionTimer(lifeTime));
        rb = GetComponent<Rigidbody>();
        grenadeThrow = FindObjectOfType<GrenadeThrow>();
        transf = grenadeThrow.grenadeSpawn;
        rb.velocity = force * transf.up / 1.5f + transf.forward * 20;
        Debug.Log("are we even using this?");
    }

    // Update is called once per frame

    void DamageCalculation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            print("yes");
            Collider TargetCollider = colliders[i];
            print(TargetCollider);
            PLayerController plTarget = TargetCollider.GetComponent<PLayerController>();
            if (!plTarget)
                break;
            float damage = grenadeDamage;
            plTarget.TakeDamage(damage);
            Debug.Log(plTarget.playerHealth);
        }
    }
    IEnumerator IEEEExplosionTimer(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        DamageCalculation();
        //Debug.Log("sound inc");
        explosionSound.Play();
        //Debug.Log("played da nade sounds");
        explosionEffect.Play();
        explosionEffect.transform.SetParent(null);
        //Destroy(gameObject);
    }
    //void PlaySound()
    //{Debug.Log("sound inc");
    //    explosionSound.Play();
    //    Debug.Log("played da nade sounds");}
}
