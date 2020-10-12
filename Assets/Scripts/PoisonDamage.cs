using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    [SerializeField]private LayerMask layerMask;
    private float multiplier;
    [SerializeField]private float poisonDamage;
    SmokeGrenade smokeGrenade;
    [SerializeField]private float range;
    
    void Reset(){
        range = 10;
    }
    void Start()
    {
        smokeGrenade  = FindObjectOfType<SmokeGrenade>();
    }
    void Update(){
        if(smokeGrenade.damage == true){
            Collider[] colliders = Physics.OverlapSphere(transform.position,range,layerMask); 
            for (int i = 0;i <colliders.Length;i++)
            {
                Collider TargetCollider = colliders[i];
                print(TargetCollider);
                PLayerController plTarget = TargetCollider.GetComponent<PLayerController>();
                if (!plTarget)
                    break;
                multiplier = Time.deltaTime * poisonDamage;
                float damage = multiplier;
                plTarget.TakeDamage(damage);
                Debug.Log(plTarget.playerHealth);
            }
        }
    }
    
}
