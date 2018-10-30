using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_bus: MonoBehaviour
{
    public Text counterText;
    //public float seconds, minutes;
    public double timehour = 9.0f;

    // Use this for initialization
    void Start()
    {
        counterText = GetComponent<Text>() as Text;
        //Invoke("Update", 400);

    }

    // Update is called once per frame
    void Update()
    {
        //minutes = (int)(Time.time/0.2f);
        //seconds = (int)(Time.time % 0.2f);
        string[] tmp = counterText.text.Split(':');
        timehour = timehour + 0.002;
        //yield return new WaitForSeconds(3);
        counterText.text = tmp[0] + ":" + (int)timehour + ":00";//+minutes.ToString("00") + ":" + seconds.ToString("00");
        if (timehour > 12)
        {
            timehour = 12;
        }

    }
}
