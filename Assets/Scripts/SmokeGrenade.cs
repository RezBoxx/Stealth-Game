using System.Collections;
using UnityEngine;
public class SmokeGrenade : MonoBehaviour
{
    [SerializeField]private ParticleSystem smokeSystem;
    [SerializeField]private float lifeTime;
    AudioSource smokeSound;
    public bool damage = false;
    GrenadeThrow grenadeThrow;
    Transform transf;
    [SerializeField] private float force = 10;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        smokeSound = GetComponent<AudioSource>();
        StartCoroutine(IEExplosionTimer(lifeTime));
        rb = GetComponent<Rigidbody>();
        grenadeThrow = FindObjectOfType<GrenadeThrow>();
        transf = grenadeThrow.grenadeSpawn;
        rb.velocity = force * transf.up / 1.5f + transf.forward * 20;
    }
    
    IEnumerator IEExplosionTimer(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        damage = true;   
        smokeSystem.Play();
        smokeSound.Play();
        //transform.(null);
        smokeSystem.transform.SetParent(null);
        //Destroy(gameObject,0.1f);
    }
}
