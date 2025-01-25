using UnityEngine;
using TMPro;
using System;

public class TimerCountdown : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timerLength;

    public event EventHandler OnEndTime;

    float timer;

    void Start() {
        timer = timerLength;

    }
    

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timer > 0) {
            timer -= Time.deltaTime;   

        } else if (timer < 0) {
            timer = timerLength;
            // fire off event
            OnEndTime?.Invoke(this, EventArgs.Empty);
        }

        
        
    }
}
