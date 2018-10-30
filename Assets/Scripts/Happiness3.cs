using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness3 : MonoBehaviour
{

    public Text texthappiness;
    public float currentHappiness;
    //public Text textobject;
    public float amount = 0.002f;
    public float startingHappiness = 100;
    public Slider barhappiness;
    // Use this for initialization
    void Start()
    {
        barhappiness = gameObject.GetComponent<Slider>();
        //valueText = gameObject.GetComponent<Text>();
        currentHappiness = startingHappiness;
        // currentHealth = Mathf.Clamp(energy.value, 0, startingHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //stransform.rotation = Camera.main.transform.rotation;
        string[] tmp = texthappiness.text.Split(':');
        texthappiness.text = tmp[0] + ":" + (int)currentHappiness;
       //currentHappiness -= amount;

        barhappiness.maxValue = startingHappiness;
        barhappiness.value = currentHappiness;
        //energy.minValue = 0;
    }
}

