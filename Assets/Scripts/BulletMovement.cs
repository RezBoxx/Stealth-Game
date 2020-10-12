using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 20f;
    public LayerMask layerMask;
    Collider col;
    public float Lifetime = 5f;

    ShootingAgent shootingAgent;



    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        Destroy(gameObject, Lifetime);
        shootingAgent = GameObject.FindObjectOfType<ShootingAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet Movement            
        Quaternion rot = transform.rotation;
        Vector3 pos = transform.position;
        Vector3 posChange = new Vector3(speed * Time.deltaTime, 0, 0);
        pos += rot * posChange;
        transform.position = pos;
    }

    IEnumerator DeleteBullet()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    void OnColliderEnter(Collision a)
    {
        print(Time.time + " " + gameObject.name);
        StartCoroutine(DeleteBullet());
    }
}
