using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness : MonoBehaviour {

    public Text texthappiness;
    public float currentHappiness;
    //public Text textobject;
    //public float amount =1;
    public float startingHappiness = 100;
    public Slider barhappiness;
    // Use this for initialization
    void Start () {
        barhappiness = gameObject.GetComponent<Slider>();
        //valueText = gameObject.GetComponent<Text>();
        currentHappiness = startingHappiness;
       // currentHealth = Mathf.Clamp(energy.value, 0, startingHealth);
    }
	
	// Update is called once per frame
	void Update () {
        //stransform.rotation = Camera.main.transform.rotation;
        string[] tmp = texthappiness.text.Split(':');
        texthappiness.text = tmp[0] + ":" + currentHappiness;
        //currentHealth -= amount;
        barhappiness.maxValue = startingHappiness;
        barhappiness.value = currentHappiness;
        //energy.minValue = 0;
	}
}
