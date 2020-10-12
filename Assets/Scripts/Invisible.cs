using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Invisible : NetworkBehaviour
{
    [SerializeField] private Material invisibleMat;
    [SerializeField] private Material gunMat;
    [SerializeField] private Material default001;
    [SerializeField] private Material NoName;
    [SerializeField] private Material owngunmat;
    [SerializeField] private Material armormat;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject arms;
    [SerializeField] private GameObject owngun;
    [SerializeField] private GameObject armor;
    private bool wait = true;
    [SerializeField] private int invisibleTime = 10;
    PLayerController pl;
    private int reUseTimer = 100;



    // Start is called before the first frame update
    void Start()
    {
        pl = GetComponent<PLayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ++reUseTimer;
        if(reUseTimer >= 100)
        {
            reUseTimer = 100;
        }
        if(Input.GetKeyDown(KeyCode.H)&& gameObject.tag =="Spy" && wait == true)
        {
            wait = false;
            
            StartCoroutine(IEMaterialChange(invisibleTime));
        }
    }

    IEnumerator IEMaterialChange(float time)
    {
        CmdMaterialChange();
        yield return new WaitForSeconds(time);
        CmdMaterialChangeBack();
    }
    [Command]
    void CmdMaterialChange()
    {
        RpcMaterialChange();
        // print(Time.time + " invisivlein");
        // gun.GetComponent<Renderer>().material = invisibleMat;
        // mesh.GetComponent<Renderer>().material = invisibleMat;
        // mesh.GetComponent<Renderer>().enabled = false;
    }
     [Command]
    void CmdMaterialChangeBack()
    {
        RpcMaterialChangeBack();
        // gun.GetComponent<Renderer>().material = gunMat;
        // mesh.GetComponent<Renderer>().material = default001;
        // mesh.GetComponent<Renderer>().enabled = true;
        // wait = true;
        // reUseTimer = 0;
    }
    [ClientRpc]
    void RpcMaterialChange()
    {
        print(Time.time + " invisivlein");
        gun.GetComponent<Renderer>().material = invisibleMat;
        mesh.GetComponent<Renderer>().material = invisibleMat;
        //mesh.GetComponent<Renderer>().enabled = false;
        arms.GetComponent<Renderer>().material = invisibleMat;
        owngun.GetComponent<Renderer>().material = invisibleMat;
        armor.GetComponent<Renderer>().material = invisibleMat;

    }
    [ClientRpc]
    void RpcMaterialChangeBack()
    {
        gun.GetComponent<Renderer>().material = gunMat;
        mesh.GetComponent<Renderer>().material = default001;
        arms.GetComponent<Renderer>().material = NoName;
        owngun.GetComponent<Renderer>().material = owngunmat;
        armor.GetComponent<Renderer>().material = armormat;
        wait = true;
        reUseTimer = 0;
    }
}
