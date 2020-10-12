using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Collider col;
    public int mineDamage = 30;
    public float mineRange = 10.0f;
    public LayerMask layerMask;
    AudioSource mineExplosionSound;
    void Start()
    {
        col = GetComponent<Collider>();
        mineExplosionSound = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Spy"|| col.gameObject.tag =="Soldier")
        {
            mineExplosionSound.Play();
            if (col.gameObject.tag == "Spy" || col.gameObject.tag == "Soldier")
            {
                DamageCalculation();
                Destroy(gameObject);
            }
            if(!mineExplosionSound.isPlaying) { Destroy(gameObject); }
            
        }
    }
    void DamageCalculation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, mineRange, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider TargetCollider = colliders[i].GetComponent<Collider>();
            if (!col)
                continue;
            PLayerController plTarget = TargetCollider.GetComponent<PLayerController>();
            if (!plTarget)
                continue;
            float damage = mineDamage;
            plTarget.TakeDamage(damage);
            Debug.Log(plTarget.playerHealth);
        }
    }
}
