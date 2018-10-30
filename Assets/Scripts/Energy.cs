using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

    public Text textenergy;
    public float currentenergy;
    //public Text textobject;
    //public float amount =1;
    public float startingenergy = 100;
    public Slider barenergy;
    // Use this for initialization
    void Start () {
        barenergy = gameObject.GetComponent<Slider>();
        //valueText = gameObject.GetComponent<Text>();
        currentenergy = startingenergy;
       // currentHealth = Mathf.Clamp(energy.value, 0, startingHealth);
    }
    
    // Update is called once per frame
    void Update () {
        //stransform.rotation = Camera.main.transform.rotation;
        string[] tmp = textenergy.text.Split(':');
        textenergy.text = tmp[0] + ":" + currentenergy;
        //currentHealth -= amount;
        barenergy.maxValue = startingenergy;
        barenergy.value = currentenergy;
        //energy.minValue = 0;
    }
}
