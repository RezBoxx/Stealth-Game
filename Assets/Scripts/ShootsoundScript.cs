using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootsoundScript : MonoBehaviour
{
    private AudioSource shootsound;
    // Start is called before the first frame update
    void Start()
    {
        shootsound = GetComponent<AudioSource>();
        shootsound.Play();
        if (!shootsound.isPlaying) { Destroy(gameObject); }
    }
}
