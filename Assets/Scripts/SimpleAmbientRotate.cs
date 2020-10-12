using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class SimpleAmbientRotate : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private Vector3 axis = new Vector3(0, 1, 0);

    private Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Rotate(axis, speed * Time.deltaTime);
    }
}
