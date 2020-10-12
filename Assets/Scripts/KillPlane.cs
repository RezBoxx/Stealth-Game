using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    [SerializeField]private LayerMask layerMask;
    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position,transform.localScale/2,Quaternion.identity,layerMask);
        for(int i = 0;i < colliders.Length; i++)
        {
            Collider targetCollider = colliders[i];
            PLayerController plTarget = targetCollider.GetComponent<PLayerController>();
            if(!plTarget)
            break;
            Debug.Log("layermask killplane health 0");
            plTarget.playerHealth = 0;
        }
    }
}
