using UnityEngine;
using TMPro;
using System;

/// <summary>
///  Handles the time countdown system bc we want only one of them yes yes
/// </summary>
public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTextMaker;

    [SerializeField] TextMeshProUGUI timerTextInvestor;
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

        timerTextMaker.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerTextInvestor.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timer > 0) {
            timer -= Time.deltaTime;   

        } else if (timer < 0) {
            timer = timerLength;
            // fire off event ONCE YAYYYY
            OnEndTime?.Invoke(this, EventArgs.Empty);
        }

        
        
    }
}
