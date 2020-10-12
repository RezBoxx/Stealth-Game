using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAnimController : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Crouching", false);
        }
        else
        {
            animator.SetBool("Crouching", true);
        }

        animator.SetFloat("x", Input.GetAxis("Horizontal"));
        animator.SetFloat("z", Input.GetAxis("Vertical"));
    }
}
