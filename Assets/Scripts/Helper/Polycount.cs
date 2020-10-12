using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Polycount : MonoBehaviour
{

    private MeshFilter[] meshes;
    IList<MeshFilter> m;

    // Start is called before the first frame update
    void Start()
    {
        meshes = GameObject.FindObjectsOfType<MeshFilter>();
        m = meshes;
        print("meshes " + m.Count);
        //m = m.OrderBy(x => x.mesh.triangles.Length).ToList();

        print("test " + m[0].mesh.triangles.Length);
        foreach (MeshFilter f in m)
        {
            //print(f.gameObject.name + " " + f.mesh.triangles.Length);
        }

    }

    //find all meshrenderers
    //
    int frame;
    // Update is called once per frame
    void Update()
    {
        print(m[frame].gameObject.name + " " + m[frame].mesh.triangles.Length + " frame " + frame);
        frame++;
    }
}
