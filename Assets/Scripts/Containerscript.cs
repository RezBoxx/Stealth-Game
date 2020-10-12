using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containerscript : MonoBehaviour
{
    public string caseSwitch;
    [SerializeField]private float equipRange;
    [SerializeField]private LayerMask layerMask;
 
    // Update is called once per frame
    
    void Update()
    { 
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, equipRange,layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider TargetCollider = colliders[i];
            PLayerController plTarget = TargetCollider.GetComponent<PLayerController>();
            if (!plTarget)
            break;
            
            if(Input.GetKey(KeyCode.E)){
                switch(caseSwitch)
                {
                    case "Grenade":
                    plTarget.grenadeAmmo = 3;
                    plTarget.smokeAmmo = 0;
                    plTarget.flashAmmo = 0;
                    plTarget.mineAmmo = 0;
                    plTarget.invisbleAmmo = 0;
                    plTarget.camAmmo = 0;
                    break;
                    case "Smoke":
                    plTarget.grenadeAmmo = 0;
                    plTarget.smokeAmmo = 3;
                    plTarget.flashAmmo = 0;
                    plTarget.mineAmmo = 0;
                    plTarget.invisbleAmmo = 0;
                    plTarget.camAmmo = 0;
                    break;
                    case "Invisibility":
                    plTarget.grenadeAmmo = 0;
                    plTarget.smokeAmmo = 0;
                    plTarget.flashAmmo = 0;
                    plTarget.mineAmmo = 0;
                    plTarget.invisbleAmmo = 2;
                    plTarget.camAmmo = 0;
                    break;
                    case "Flash":
                    plTarget.grenadeAmmo = 0;
                    plTarget.smokeAmmo = 0;
                    plTarget.flashAmmo = 3;
                    plTarget.mineAmmo = 0;
                    plTarget.invisbleAmmo = 0;
                    plTarget.camAmmo = 0;
                    break;
                    case "Mine":
                    plTarget.grenadeAmmo = 0;
                    plTarget.smokeAmmo = 0;
                    plTarget.flashAmmo = 0;
                    plTarget.mineAmmo = 3;
                    plTarget.invisbleAmmo = 0;
                    plTarget.camAmmo = 0;
                    break;
                    case "Cam":
                    plTarget.grenadeAmmo = 0;
                    plTarget.smokeAmmo = 0;
                    plTarget.flashAmmo = 0;
                    plTarget.mineAmmo = 0;
                    plTarget.invisbleAmmo = 0;
                    plTarget.camAmmo = 3;
                    break;
                    default:
                    print("Please enter: Grenade, Smoke, Invisibility, Flash, Mine or Cam into the string field.");
                break;
                }
            }
        }   
    }
}
