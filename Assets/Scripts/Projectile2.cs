using UnityEngine;
using Mirror;

public class Projectile2 : NetworkBehaviour
{
    public float destroyAfter = 5;
    public Rigidbody rigidBody;
    public float force = 1000;
    public GameObject currentTarget;
    //[SerializeField]LayerMask layerMask;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        rigidBody.AddForce(transform.forward * force);
        //CmdRaycastShot();
    }

    // destroy for everyone on the server
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    // ServerCallback because we don't want a warning if OnTriggerEnter is
    // called on the client
    /*public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Soldier")
        {
            CmdDealDmg(other.gameObject);
        }
    }
    //[Command]
    public void CmdDealDmg(GameObject TargetCollider)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10, layerMask);
        for(int i = 0; i< colliders.Length; i++)
        {
            Collider Targetcollider = colliders[i].GetComponent<Collider>();
            PLayerController pLayerController = Targetcollider.GetComponent<PLayerController>();
            if (!pLayerController)
                return;
            pLayerController.playerHealth -= 34;
            Debug.Log("Network command Dmg");
        }
    }*/
    
}

