using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer = 300f;
    private float minutes;
    private float seconds;
    private float speed = 2;

    [SerializeField]private TextMeshProUGUI timerDisplay;
    public bool stop = false;  
    
    public void StartTimer(float timeUsed)
    {
        stop = false;
        timer = timeUsed;
        Update();
        StartCoroutine(UpdateCoroutine());
    }

    void Update()
    {
        if(stop)return;
        timer -= Time.deltaTime;

        minutes = Mathf.Floor(timer/60);
        seconds = timer % 60; 
        if(seconds > 59) seconds = 59;
        if(minutes < 0 )
        {
            minutes = 0;
            seconds = 0;
        }
    }
    private IEnumerator UpdateCoroutine(){
        while(!stop)
        {
            timerDisplay.text = string.Format("{0:0}:{1:00}",minutes,seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
