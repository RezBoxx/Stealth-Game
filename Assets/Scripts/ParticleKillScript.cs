using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKillScript : MonoBehaviour
{
    [SerializeField]private float killTime;

    void Reset(){
        killTime = 10;
    }
    void Start()
    {
        Destroy(gameObject,killTime);
    }
}
