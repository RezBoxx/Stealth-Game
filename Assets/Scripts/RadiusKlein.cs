﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusKlein : MonoBehaviour
{
    public MeshRenderer mesh1, mesh2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mesh1.enabled = !mesh1.enabled;
            mesh2.enabled = !mesh2.enabled;
        }
    }
}
