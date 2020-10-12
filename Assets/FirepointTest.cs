using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepointTest : MonoBehaviour
{
    public GameObject muzzle, trail, hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(Time.time + " pew pew");
            GameObject g = Instantiate(muzzle, transform.position, Quaternion.LookRotation(transform.right, transform.up));
            g.transform.SetParent(transform, true);
            Destroy(g, .09f);
        }
    }
}
