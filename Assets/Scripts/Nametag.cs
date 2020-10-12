using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nametag : MonoBehaviour
{
    [SerializeField]private TMP_InputField nameTagField;
    [SerializeField]private TextMeshPro displayName;
    public string  nameTag;
    

    // Update is called once per frame
    void Update()
    {
        nameTag = nameTagField.text;
    }
}
