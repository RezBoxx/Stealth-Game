using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class ShotGlowingDecal : MonoBehaviour
{
    public float speed = 1;
    public float lifetime = 10;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = Instantiate(GetComponent<DecalProjectorComponent>().material);
        GetComponent<DecalProjectorComponent>().material = material;

        StartCoroutine(ReduceGlow());
    }

    IEnumerator ReduceGlow()
    {
        float t = 0;

        while (t <= 1)
        {
            material.SetFloat("_Glow", Mathf.Lerp(1, 0, t));
            t += Time.deltaTime * speed;

            yield return null;
        }

        material.SetFloat("_Glow", 0);

        //yield return new WaitForSeconds(lifetime);
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
