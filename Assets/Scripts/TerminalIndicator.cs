using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalIndicator : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("triggered indicator");
        if (other.gameObject.tag == "Spy")
        {
            Debug.Log("spy touching indicator");
            this.gameObject.SetActive(false);
            Debug.Log("indicator off");
        }
    }
}
