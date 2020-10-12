using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Slider healthBar;
    private Image healthBarBG;
    //private Text healthNumbers;
    //PLayerController pl;

    // Start is called before the first frame update
    void Start()
    {
        //healthBar = GameObject.Find("Healthbar").GetComponent<Slider>();
        //healthBarBG = GameObject.Find("HealthbarBG").GetComponent<Image>();
        //healthNumbers = GameObject.Find("Healthtext").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        //pl = GetComponent<PLayerController>();
        //int plyHealth = (int)pl.playerHealth;
        //healthBar.value = plyHealth;
        //healthBarBG.fillAmount = 1- (plyHealth / (float)100);
        //healthNumbers.text = "Health: "+ healthBar.value.ToString(); 

    }
}
