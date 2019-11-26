using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    public GameObject timer;
    private Text timerText;
    public GameObject annaObject;
    private AnnaController anna;
    int totalTime;
    // Start is called before the first frame update
    void Start()
    {
        anna = annaObject.GetComponent<AnnaController>();
        totalTime = anna.totalTime;
        timerText = timer.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anna.countDownDisplay >= 0)
        {
            timerText.text = anna.countDownDisplay.ToString();
        }
        else
        {
            timerText.text = ("RUN");
        }
        
        
    }
}
