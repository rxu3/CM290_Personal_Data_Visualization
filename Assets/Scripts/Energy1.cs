using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy1 : MonoBehaviour
{

    public Text textenergy;
    public float currentenergy;
    //public float fSpawnDelay;
    
    //public Text textobject;
    public float amount =0.0001f;
    public float startingenergy = 100;
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
        //InvokeRepeating("SpawnStart", 0.0f, fSpawnDelay);
        currentenergy -= amount;
        if (currentenergy<80){
            currentenergy = 80;
        }
        else if (currentenergy>100){
            currentenergy = 100;
        }
        barenergy.maxValue = startingenergy;
        barenergy.value = currentenergy;
        //energy.minValue = 0;
    }





}
