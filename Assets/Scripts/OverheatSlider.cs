using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatSlider : MonoBehaviour
{
    public Slider overheatSlider;
    void Start()
    {
        overheatSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        --overheatSlider.value;
    }
}
