using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy3 : MonoBehaviour
{

    public Text textenergy;
    public float currentenergy;
    //public Text textobject;
    public float amount = 0.005f;
    public float startingenergy = 30;
    public Slider barenergy;
    // Use this for initialization
    void Start()
    {
        barenergy = gameObject.GetComponent<Slider>();
        //valueText = gameObject.GetComponent<Text>();
        currentenergy = startingenergy;
        // currentHealth = Mathf.Clamp(energy.value, 0, startingHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //stransform.rotation = Camera.main.transform.rotation;
        string[] tmp = textenergy.text.Split(':');
        textenergy.text = tmp[0] + ":" + (int)currentenergy;
        currentenergy =currentenergy+ amount;

        if (currentenergy > 100)
        {
            currentenergy = 100;
        }
        //barenergy.maxValue = 100;
        barenergy.value = currentenergy;
        Debug.Log(currentenergy);
        //energy.minValue = 0;
    }
}
