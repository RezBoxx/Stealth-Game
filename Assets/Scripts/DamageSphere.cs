using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSphere : MonoBehaviour
{
    [SerializeField]private int shootingDamage;
    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject,0.01f); 
    }

    // Update is called once per frame
    
    void OnTriggerEnter(Collider other)
    {
        PLayerController pl = other.GetComponent<PLayerController>();
        if(!pl)
        return;
        pl. playerHealth =- shootingDamage;
        Debug.Log(pl.playerHealth);
    }
}
