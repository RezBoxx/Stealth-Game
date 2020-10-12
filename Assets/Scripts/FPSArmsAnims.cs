using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSArmsAnims : MonoBehaviour
{

    Animator animator;
    PLayerController pl;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pl = GameObject.FindObjectOfType<PLayerController>();
    }

    public void Fire()
    {
        animator.Play("Fire", 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
    }
}
