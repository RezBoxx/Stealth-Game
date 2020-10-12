//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Mirror;

//public class Shootline : NetworkBehaviour
//{
//    public GameObject effect;
//    public float speed = .1f;
//    Camera cam;
//    public AnimationCurve curve;
//    public GameObject fx;
//    public GameObject decal;

//    // Start is called before the first frame update
//    void Start()
//    {
//        cam = Camera.main;
//    }

//    //g = LineRenderer Gameobj, normal = Raycasthit.normal
//    IEnumerator Shoot(GameObject g, Vector3 normal, Vector3 startPoint)
//    {
//        LineRenderer l = g.GetComponent<LineRenderer>();
//        Vector3[] pos = new Vector3[] { startPoint, g.transform.position };
//        l.SetPositions(pos);
//        g.transform.GetChild(0).transform.position += normal * .1f;
//        Material m = Instantiate(l.material);
//        l.material = m;

//        Light licht = g.transform.GetChild(0).GetComponent<Light>();
//        float lichtstart = licht.intensity;

//        float t = 0;
//        int frames = 0;

//        while (t <= 1)
//        {
//            t += Time.deltaTime / speed;
//            //Vector3 currentPos = Vector3.Lerp(startPoint, g.transform.position, Mathf.Clamp(t, 0, .5f));
//            l.material.SetFloat("_Fade", curve.Evaluate(t));
//            licht.intensity = Mathf.Lerp(lichtstart, 0, t);

//            frames++;


//            yield return null;
//        }

//        //print(frames);

//        Destroy(g);

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        /*
//        if (Input.GetMouseButtonDown(0))
//        {
//            Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
//            Ray r = cam.ScreenPointToRay(Input.mousePosition);

//            RaycastHit hito;
//            if (Physics.Raycast(r, out hito))
//            {
//                //shot line
//                GameObject g = Instantiate(effect, hito.point, Quaternion.LookRotation(hito.normal, Vector3.up));
//                StartCoroutine(Shoot(g, hito.normal));

//                //destroy sparks
//                Destroy(Instantiate(fx, hito.point + hito.normal * .1f, Quaternion.LookRotation(hito.normal, Vector3.up)), 1);

//                //spawn decal
//                GameObject tempDecal = Instantiate(decal, hito.point, Quaternion.LookRotation(-hito.normal, Vector3.up));
//                //rotate decals randomly
//                tempDecal.transform.Rotate(tempDecal.transform.forward, Random.Range(0, 90), Space.World);
//            }

//        }
//        */
//    }

//    [Command]
//    public void CmdCreateShotVFX(Vector3 startPoint, RaycastHit hit)
//    {
//        //shot line
//        GameObject g = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
//        NetworkServer.Spawn(g);
//        StartCoroutine(Shoot(g, hit.normal, startPoint));

//        //destroy sparks
//        GameObject tempfx = Instantiate(fx, hit.point + hit.normal * .1f, Quaternion.LookRotation(hit.normal, Vector3.up));
//        NetworkServer.Spawn(tempfx);
//        Destroy(tempfx, 1);

//        //spawn decal
//        GameObject tempDecal = Instantiate(decal, hit.point, Quaternion.LookRotation(-hit.normal, Vector3.up));
//        NetworkServer.Spawn(tempDecal);
//        //rotate decals randomly
//        tempDecal.transform.Rotate(tempDecal.transform.forward, Random.Range(0, 90), Space.World);
//    }
//}
