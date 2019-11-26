using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplayMulti : MonoBehaviour
{
    public GameObject timer;
    private Text timerText;
    public GameObject annaObject;
    private AnnaControllerMulti anna;
    int totalTime;
    // Start is called before the first frame update
    void Start()
    {
        anna = annaObject.GetComponent<AnnaControllerMulti>();
        totalTime = anna.totalTime;
        timerText = timer.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = anna.countDownDisplay.ToString();

    }
}
