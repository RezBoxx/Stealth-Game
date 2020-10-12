using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuff : MonoBehaviour
{
    int respawnTime = 3;
    public static int spyHealth = 3;
    public static int soldierHealth = 5;
    bool soldierisDead = false;
    bool spyisDead = false;

    GameObject soldier;
    GameObject spy;
    public Transform soldierSpawn;
    public Transform spySpawn;
        

    // Start is called before the first frame update
  

    // Update is called once per frame
     public void CheckHealth()
    {
        if (spyHealth<1)
        { SpyDeath(); }
        if (soldierHealth<1)
        { SoldierDeath(); }
        return;
    }


    private void SoldierDeath()
    {
        soldierisDead = true;
        Debug.Log("soldier died");
        soldier.SetActive(false);
        soldierSpawn.transform.position = soldierSpawn.position;
        soldierSpawn.transform.rotation = soldierSpawn.rotation;
        soldierHealth = 5;
        soldierisDead = false;
        soldier.SetActive(true);
        Debug.Log("soldier respawned");
    }
    private void SpyDeath()
    {
        spyisDead = true;
        Debug.Log("spy died");
        spy.SetActive(false);
        spySpawn.transform.position = spySpawn.position;
        spySpawn.transform.rotation = spySpawn.rotation;
        spyHealth = 3;
        spyisDead = false;
        spy.SetActive(true);
        Debug.Log("spy respawned");
    }
}
