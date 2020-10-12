using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider c = GetComponent<Collider>();
        print(gameObject.name + " " + c.bounds.extents * 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
