using UnityEngine;
using UnityEngine.Rendering;

public class Flash : MonoBehaviour
{
    public Volume volume;
    [SerializeField]private float multiplier;

    // Update is called once per frame
    void Update()
    {
        volume.weight -= Time.deltaTime/multiplier;
        if(volume.weight <= 0)
        {
            Destroy(gameObject);
        }
    }
}
